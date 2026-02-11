using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JalapenoAttack : MonoBehaviour
{
    public  LayerMask layer;
    public  float distance;
    private Vector3 startPoint;
    private RaycastHit2D[] collider;

    private void Awake()
    {
        startPoint = transform.position-new Vector3(6.18f,0,0);
    }

    public void BurnZombie()
    {
        collider=Physics2D.RaycastAll(startPoint, transform.right, distance, layer);
        foreach (RaycastHit2D hit in collider)
        {
            if (hit.collider.tag == "Zombie")
            {
                Zombie zombie = hit.collider.gameObject.GetComponent<Zombie>();
                AudioManager.Instance.PlayClip(Config.jalapeno,1);
                zombie.gameObject.GetComponent<Animator>().SetTrigger("isBoom");
                zombie.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                zombie.GetComponent<Rigidbody2D>().simulated = false;
                zombie.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint,startPoint+transform.right*distance);
    }


    //播放完动画后Destroy
    public void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
