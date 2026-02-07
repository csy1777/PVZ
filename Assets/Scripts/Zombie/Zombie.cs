using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ZombieState
{
   Move,Eat,Die,Pause
}
public class Zombie : MonoBehaviour
{
   private Rigidbody2D rb;
   private Animator anim;
   public float moveSpeed;
   public int atkValue=30;
   public float atkDuration = 2f;
   private float atkTimer;
   public int Hp = 100;
   private int currentHp;
   private Plant currentPlant;
   public GameObject ZombieHeadPrefab;
   private bool haveHead=true;
   ZombieState zombieState=ZombieState.Move;

   private void Awake()
   {
      rb=GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      currentHp = Hp;
   }

   private void Update()
   {
      switch (zombieState)
      {
         case ZombieState.Move:
            MoveUpdate();
            break;
         case ZombieState.Eat:
            EatUpdate();
            break;
         case ZombieState.Die:
            DieUpdate();
            break;
      }
   }

   private void MoveUpdate()
   {
      rb.velocity = new Vector2(-moveSpeed,rb.velocity.y);
   }

   private void EatUpdate()
   {
      if (currentPlant != null)
      {
         rb.velocity = Vector2.zero;
         atkTimer += Time.deltaTime;
         if (atkTimer >= atkDuration)
         {
            currentPlant.TakeDamage(atkValue);
            AudioManager.Instance.PlayClip(Config.eat,1);
            atkTimer = 0;
         }
      }
   }
   private void DieUpdate()
   {
      rb.velocity = Vector2.zero;
   }
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Plant")
      {
         anim.SetBool("isAttacking", true);
         currentPlant = other.GetComponent<Plant>();
         TransitionToEat();
      }
      else if (other.tag == "House")
      {
         GameManager.Instance.EndGameFail();
      }
      else if (other.tag == "LawnMover")
      {
         LawnMover lm = other.GetComponent<LawnMover>();
         lm.TransitionToRun();
         AudioManager.Instance.PlayClip(Config.lawnmover,1);
         TakeDamage(this.Hp);
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.tag == "Plant")
      {
         anim.SetBool("isAttacking", false);
         zombieState = ZombieState.Move;
         currentPlant = null;
      }
   }

   private void TransitionToEat()
   {
         zombieState = ZombieState.Eat;
         atkTimer = 0;
   }

   public void TransitionToPause()
   {
      zombieState = ZombieState.Pause;
      anim.enabled = false;
      rb.velocity = Vector2.zero;
      rb.simulated = false;
   }

   public void TakeDamage(int damage)
   {
      if (currentHp <= 0)
      {
         return;
      }
      currentHp -= damage;
      if (currentHp <= 0)
      {
         currentHp = -1;
         Dead();
      }
      float hpPercent = currentHp*1f / Hp;
      anim.SetFloat("HpPercent",hpPercent);
      if (haveHead && hpPercent <= .5f)
      {
         haveHead = false;
         GameObject newHead = Instantiate(ZombieHeadPrefab, transform.position, Quaternion.identity);
         Destroy(newHead, 2f);
      }
   }

   private void Dead()
   {
      if(zombieState == ZombieState.Die)
         return;
      zombieState = ZombieState.Die;
      GetComponent<BoxCollider2D>().enabled = false;
      rb.velocity = Vector2.zero;
      rb.simulated = false;
      ZombieManager.Instance.RemoveZombie(this);
      Destroy(gameObject, 2f);
   }
}
