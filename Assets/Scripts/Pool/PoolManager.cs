using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager :SingleTon<PoolManager>
{
   public GameObject sun;
   public GameObject peaBullet;
   public GameObject peaBulletHit;
   public static Dictionary<string, Pool> pools = new Dictionary<string, Pool>();
   protected override void Awake()
   {
      base.Awake();
      pools.Add("PeaBullet", new Pool(peaBullet));
      
      pools.Add("PeaBulletHit", new Pool(peaBulletHit));
      
      pools.Add("Sun", new Pool(sun));
   }
}
