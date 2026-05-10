using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Dialogue
{
    public string name;
    public Image character;
    public string content;
}


public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get { return instance; }
    }

    public Text NameText;
    public Text ContentText;
    public Image CharacterImage;
    public List<Dialogue> GameStartDialogues;
    
    public List<Dialogue> showDialogues;
    private Dictionary<string, List<Dialogue>> Dialogues = new Dictionary<string, List<Dialogue>>();
    
    public int index = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Dialogues.Add("GameStart", GameStartDialogues);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Show();
        }
    }

    public void StartDialogue(string dialogueName)
    {
        if (!Dialogues.ContainsKey(dialogueName))
        {
            Debug.Log("不存在这个对话");
            return;
        }

        gameObject.SetActive(true);
        this.showDialogues = Dialogues[dialogueName];
        index = 0;
        Show();
    }

    public void Show()
    {
        if (index >= showDialogues.Count)
        {
            gameObject.SetActive(false);
            showDialogues = null;
            index = 0;
            return;
        }
        Dialogue dialogue = showDialogues[index++];
        NameText.text = dialogue.name;
        ContentText.text = "";
        CharacterImage.sprite = dialogue.character.sprite;
        
        float charSpeed = 0.1f; // 每个字符出现的时间间隔（秒）
        float totalTime = dialogue.content.Length * charSpeed;
        ContentText.DOText(dialogue.content, totalTime).SetEase(Ease.Linear);
    }

}
