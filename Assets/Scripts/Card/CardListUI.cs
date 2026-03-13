using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardListUI : MonoBehaviour
{
    public List<Card> cards;

    private void Start()
    {
        DisableCardList();
    }

    public void ShowCardList()
    {
        //游戏开始后让卡片列表显示到屏幕并禁止卡片状态
        GetComponent<RectTransform>().DOAnchorPosY(-65f, 1f);
        EableCardList();
    }
    public void DisableCardList()
    {
        foreach (Card card in cards)
        {
            card.DisableCard();
        }
    }
    public void EableCardList()
    {
        foreach (Card card in cards)
        {
            card.EnableCard();
        }
    }

    public void SaveCards()
    {
        
        if (cards == null || cards.Count <= 0)
        {
            Debug.LogWarning("卡片列表为空！");
            return;
        }

        // 添加卡片
        foreach (Card card in cards)
        {
            if (!GameDataHub.playerData.unlockedPlant.Contains(card.plantType))
            {
                GameDataHub.playerData.unlockedPlant.Add(card.plantType);
            }
        }

       
        if (BinaryDataMgr.Instance != null)
        {
            BinaryDataMgr.Instance.SaveData(GameDataHub.playerData, "Save.dat");
            Debug.Log("✅ 卡片保存成功！");
        }
        else
        {
            Debug.LogError("❌ BinaryDataMgr 不在场景里！");
        }
    }
    public void LoadCards()
    {
        // 1. 安全读取存档（没有就返回）
        PlayerData loadData = BinaryDataMgr.Instance.LoadData<PlayerData>("Save.dat");
        if (loadData == null)
        {
            Debug.Log("无存档，使用默认卡片");
            return;
        }

        // 2. 把读取到的数据赋值给全局数据
        GameDataHub.playerData = loadData;
        
        Debug.Log("✅ 卡片读取成功！");
    }
}
