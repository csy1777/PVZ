using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

enum SpawnState
{
   NotStart,
   Spawning,
   End
}


public class ZombieManager : SingleTon<ZombieManager>
{
   SpawnState state = SpawnState.NotStart;
   public GameObject ZombiePrefab;
   public Transform[] SpawnPoints;
   [Header("ZombieCount in everyRound")]
   public int[] EachSpawnCount;
   [Header("Time betweeen everyRound")]
   public float[] BetweenEachRoundSpawnTime;
   [Header("Time of EveryZombieSpawn")]
   public float EachSpawnTime=2f;
   private List<Zombie> ZombieList= new List<Zombie>();
   
   private void Update()
   {
      if (state == SpawnState.End && ZombieList.Count == 0)
      {
         //游戏胜利
         GameManager.Instance.EndGameSuccess();
      }
   }

   public void StartSpawning()
   {
      //开始生成僵尸
      state = SpawnState.Spawning;
      StartCoroutine(SpawnZombie());
   }
   IEnumerator SpawnZombie()
   {
       yield return new WaitForSeconds(5f);
      //僵尸生成完成后状态转换为End
         for (int i = 0; i < EachSpawnCount[0]; i++)
         {
            SpawnRandomZombie();
            yield return new WaitForSeconds(EachSpawnTime);
         }

         yield return new WaitForSeconds(BetweenEachRoundSpawnTime[0]);
         for (int i = 0; i < EachSpawnCount[1]; i++)
         {
            SpawnRandomZombie();
            yield return new WaitForSeconds(EachSpawnTime);
         }

         yield return new WaitForSeconds(BetweenEachRoundSpawnTime[1]);
         AudioManager.Instance.PlayClip(Config.finalwave,1);
         for (int i = 0; i < EachSpawnCount[2]; i++)
         {
            SpawnRandomZombie();
            yield return new WaitForSeconds(EachSpawnTime);
         }
         state = SpawnState.End;
   }

   //在每一行随机生成僵尸,如果游戏结束暂停生成僵尸
   private void SpawnRandomZombie()
   {
      if (state == SpawnState.End)
      {
         StopAllCoroutines();
         return;
      }

      if (state == SpawnState.Spawning)
      {
         int index = Random.Range(0, SpawnPoints.Length);
         GameObject go = Instantiate(ZombiePrefab, SpawnPoints[index].position, Quaternion.identity);
         go.GetComponent<SpriteRenderer>().sortingOrder = 10 * (index+1);
         ZombieList.Add(go.GetComponent<Zombie>());
      }
   }

   public void Pause()
   {
      state = SpawnState.End;
      foreach (Zombie zombie in ZombieList)
      {
         zombie.TransitionToPause();
      }
   }
   public void RemoveZombie(Zombie zombie)
   {
      ZombieList.Remove(zombie);
   }
}
