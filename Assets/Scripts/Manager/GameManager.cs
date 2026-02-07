using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : SingleTon<GameManager>
{
    //摄像机最初的位置
    private Vector3 currentPosition;
    
    public PrepareUI prepareUI;
    public CardListUI cardListUI;
    public FailUI failUI;
    private bool isGameOver = false;

    private void Start()
    {
        currentPosition = Camera.main.transform.position;
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        //摄像机移动后显示游戏准备UI
        Camera.main.transform.DOMove(new Vector3(5,0,-10), 3f);
        yield return new WaitForSeconds(5f);
        yield return Camera.main.transform.DOMove(currentPosition, 3f).WaitForCompletion();
        ShowStartUI();
    }

    public void EndGameFail()
    {
        //游戏失败后显示失败UI和音乐和游戏结束状态 
        if (isGameOver)
            return;
        isGameOver = true;
        failUI.Show();
        AudioManager.Instance.PlayClip(Config.lose_Music,1);
        SetGameOver();
    }

    public void EndGameSuccess()
    {
        if (isGameOver)
            return;
        isGameOver = true;
        //TODO:游戏胜利的制作
        AudioManager.Instance.PlayClip(Config.win_Music, 1);
        SetGameOver();
    }

    public void SetGameOver()
    {
        ZombieManager.Instance.Pause();
        SunManager.Instance.StopProduceSun();
        PlantManager.Instance.DisablePlants();
        cardListUI.DisableCardList();
    }
    private void ShowStartUI()
    {
        prepareUI.ShowStartUI(OnPrepareUIComplete);
    }

    private void OnPrepareUIComplete()
    {
        SunManager.Instance.StartProduceSun();
        ZombieManager.Instance.StartSpawning();
        cardListUI.ShowCardList();
        AudioManager.Instance.PlayBGM(Config.bgm1);
    }
   
}
