using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SunPool : Pool<SunPool>
{
   public override void InitPrefab(GameObject go)
   {
      go.transform.DOKill();
   }
}
