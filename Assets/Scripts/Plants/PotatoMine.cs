using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PotatoMine : Plant
{
    public float watchDistance;
    public LayerMask targetLayer;
    public float attackRidus;
    public List<Collider2D> allZombiesCollider;

    protected override void UpdateEnabled()
    {
        
        if (FindEnemy())
        {
            anim.SetBool("isReady", true);
        }
        else
        {
            anim.SetBool("isReady", false);
        }

    }

    /*public override void TransitionToDisable()
    {
        //重写TransitionToDisable,让土豆地雷的Boom动画播放完整
        plantState = PlantState.Disabled;
        collider.enabled = false;
    }*/

    private bool FindEnemy()
    {
        if (Physics2D.Raycast(transform.position, transform.right, watchDistance, targetLayer))
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * watchDistance);
        Gizmos.DrawWireSphere(transform.position,attackRidus);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            anim.SetTrigger("Boom");
            AudioManager.Instance.PlayClip(Config.potatoMine,1);
            //DestroyZombies();
        }
    }

    private void HideZombies()
    {
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(transform.position, attackRidus, targetLayer);
        foreach (Collider2D collider in allColliders)
        {
            if (collider != null && collider.tag == "Zombie")
            {
                allZombiesCollider.Add(collider);
                collider.gameObject.SetActive(false);
            }
        }
    }

    //帧事件
    public void SelfBoom()
    {
        foreach (Collider2D zombieCollider in allZombiesCollider)
        {
            Zombie zombie = zombieCollider.gameObject.GetComponent<Zombie>();
            ZombieManager.Instance.RemoveZombie(zombie);
            Destroy(zombieCollider.gameObject);
        }
        TakeDamage(this.Hp);
    }

    public void Boom()
    {
        HideZombies();
    }
}
