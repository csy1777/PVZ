using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : SingleTon<MainMenuController>
{
    public GameObject setNameUI;
    public Button changeUserUI;
    public Text NameShowText;
    private Text NameInputText;
    private Button submitButton;

    protected virtual void Awake()
    {
        base.Awake();
        submitButton = setNameUI.transform.GetChild(1).GetComponent<Button>();
        NameInputText = setNameUI.transform.GetChild(0).GetChild(1).GetComponent<Text>();
        changeUserUI.onClick.AddListener(ShowSetNameUI);
        submitButton.onClick.AddListener(SubmitNameUI);
    }

    private void Update()
    {
        NameUpdate();
    }

    private void NameUpdate()
    {
        PlayerPrefs.SetString("Name", NameInputText.text);
    }

    private void ShowSetNameUI()
    {
        AudioManager.Instance.PlayClip(Config.btn_Click,1);
        setNameUI.SetActive(true);
    }

    private void SubmitNameUI()
    {
        AudioManager.Instance.PlayClip(Config.btn_Click,1);
        HideSetNameUI();
        NameShowText.text = PlayerPrefs.GetString("Name", string.Empty);
    }
    private void HideSetNameUI()
    {
        setNameUI.SetActive(false);
    }
}
