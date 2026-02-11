using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jalapeno : Plant
{
    public GameObject JalapenoAttack;
    protected override void UpdateEnabled()
    {
        anim.SetTrigger("Burn");
    }
    public void Burn()
    {
        TakeDamage(Hp);
        Vector3  pos = transform.position;
        pos.x = 0.94f;
        Instantiate(JalapenoAttack,pos,Quaternion.identity);
    }
}
