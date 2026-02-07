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
        //ShowCardList();
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
}
