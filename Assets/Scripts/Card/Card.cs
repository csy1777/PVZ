using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

enum CardState
{
    //禁用状态
    Disable,
    //冷却状态
    Cooling,
    //等待阳光收集完成状态
    WaitingSun,
    //准备完毕状态
    Ready
}
public class Card : MonoBehaviour
{
    private GameObject cardLight;
    private GameObject cardGray;
    private Image cardMask;
    public float cdTime=2;
    private float cdTimer;
    public int  needSunPoint=50;

    private CardState cardState=CardState.Disable;
    public PlantType plantType=PlantType.Sunflower;
    private void Awake()
    {
       cardLight=transform.GetChild(0).gameObject;
       cardGray=transform.GetChild(1).gameObject;
       cardMask=transform.GetChild(2).gameObject.GetComponent<Image>();
       cardLight.GetComponent<Button>().onClick.AddListener(OnCardLightClick);
    }
    

    private void Update()
    {
        switch (cardState)
        {
            case CardState.Cooling:
                CoolingUpdate();
                break;
            case CardState.WaitingSun:
                WaitingSunUpdate();
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
        }
    }

    private void CoolingUpdate()
    {
        //等待CD加载,加载完成后转换到等待阳光状态
        cdTimer += Time.deltaTime;
        cardMask.fillAmount = (cdTime-cdTimer) / cdTime;
        if (cdTimer > cdTime)
        {
            TransitionToWaitingSun();
        }
    }

    private void WaitingSunUpdate()
    {
        if (SunManager.Instance.SunPoint >= needSunPoint)
        {
            TransitionToReady();
        }
    }

    private void ReadyUpdate()
    {
        if (SunManager.Instance.SunPoint < needSunPoint)
        {
            TransitionToWaitingSun();
        }
    }

    private void TransitionToWaitingSun()
    {
        cardState = CardState.WaitingSun;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(false); 
    }

    private void TransitionToReady()
    {
        cardState = CardState.Ready;
        cardLight.SetActive(true);
        cardGray.SetActive(false);
        cardMask.gameObject.SetActive(false); 
    } 
    private void TransitionToCooling()
    {
        cdTimer = 0;
        cardState = CardState.Cooling;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(true); 
    }

    public void DisableCard()
    {
        cardState = CardState.Disable;
    }

    public void EnableCard()
    {
        //可用状态时先转换到冷却状态
        TransitionToCooling();
    }

    public void OnCardLightClick()
    {
        //卡片准备好被点击后触发音效并为HandManager提供植物并转换到冷却状态
        if (cardState == CardState.Disable)
            return;
        if (SunManager.Instance.SunPoint < needSunPoint)
            return;
        AudioManager.Instance.PlayClip(Config.btn_Click,1);
        bool isSucess=HandManager.Instance.AddPlant(plantType);
        if (isSucess)
        {
            SunManager.Instance.SubSunPoint(needSunPoint);
            TransitionToCooling();
        }
    }
}
