using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;

public class CardListUI : MonoBehaviour
{
    public List<Card> cards;
    public Transform[] cardCells;
    
    private void Start()
    {
        DisableCardList();
        
        //游戏开始使用默认植物,没有保存的数据
        ClearCards();
        LoadCards();
    }

    public void ShowCardList()
    {
        //游戏开始后让卡片列表显示到屏幕并禁止卡片状态
        
        //模拟第一关结束,保存了数据,第二关读取并生成植物卡片
        SaveCards();
        LoadCards();
        GenerateCards();
        
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
            Debug.Log("卡片保存成功！");
        }
        else
        {
            Debug.LogError("BinaryDataMgr 不在场景里！");
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
        
        Debug.Log("卡片读取成功！");
    }

    public void ClearCards()
    {
        BinaryDataMgr.Instance.ClearAllSaveData("Save.dat");
        GameDataHub.playerData.unlockedPlant.Clear();
    }

    
    //只是为了可视化,卡片应该在玩家选择之后在生成
    public void GenerateCards()
    {
        int index = 0;
        foreach (PlantType plantType in GameDataHub.playerData.unlockedPlant)
        {
            GameObject cardPrefab = Resources.Load<GameObject>($"PlantCards/{plantType}");

            if (cardPrefab == null)
            {
                Debug.LogError("找不到卡片：PlantCards/" + plantType);
                continue;
            }

         
            GameObject card = Instantiate(cardPrefab,transform);
            
            //TODO 玩家自己选择要使用的卡片,卡片槽不够用
            card.transform.position=cardCells[index].position;
            index++;
            //cards.Add(card.GetComponent<Card>());
        }
    }
}
