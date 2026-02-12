using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Squash :Plant
{
   public float atkHalfRange;
   public LayerMask zombieLayer;
   private Vector3 startPos;
   private Zombie zombie;
   private bool isFirstFindZombie=true;
   protected override void Awake()
   {
      anim = transform.GetChild(0).GetComponent<Animator>();
      collider = GetComponent<Collider2D>();
   }

   protected override void UpdateEnabled()
   {
      startPos = transform.position;
      startPos.x-=atkHalfRange;
      if (FindZombie()&&isFirstFindZombie)
      { 
         AudioManager.Instance.PlayClip(Config.hmm,1);
         isFirstFindZombie = false;
         StartCoroutine(AttackZombie());
      }
   }

   private bool FindZombie()
   {
       RaycastHit2D hit2D= Physics2D.Raycast(startPos,transform.right,atkHalfRange*2,zombieLayer);
       if (hit2D.collider != null)
       {
          zombie=hit2D.collider.GetComponent<Zombie>();
          return true;
       }
       return false;
   }

   IEnumerator AttackZombie()
   { 
      yield return new WaitForSeconds(1f);
      anim.SetTrigger("Jump");
      Vector3 pos=zombie.transform.position;
      pos.y=transform.position.y; 
      yield return transform.DOMove(pos,.5f).SetEase(Ease.Linear).WaitForCompletion();
      zombie.TakeDamage(zombie.Hp);
      PlantManager.Instance.RemovePlant(this);
      Destroy(gameObject,1f);
   }
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawLine(startPos,startPos + transform.right * atkHalfRange*2);
   }
   
}
