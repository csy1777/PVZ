using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut :Plant
{
    private float startHp;

    protected override void Start()
    {
        base.Start();
        startHp = Hp;
    }
    protected override void UpdateEnabled()
    {
        float  currentHp = Hp*1f/startHp; 
        anim.SetFloat("Hp", currentHp);
    }
}
