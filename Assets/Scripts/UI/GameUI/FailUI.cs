using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FailUI : MonoBehaviour
{
    private Animator anim;
    
    
    public Image failMenuUI;
    public Button reloadBtn;
    public Button quitBtn;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        reloadBtn.onClick.AddListener(ReloadBtnOnClick);
        quitBtn.onClick.AddListener(QuitBtnOnClick);
        Hide();
    }

    public void Hide()
    {
        anim.enabled = false;
        failMenuUI.enabled = false;
    }

    public void Show()
    {
        anim.enabled = true;
    }

    public void ShowFailMenuUI()
    {
        failMenuUI.gameObject.SetActive(true);
    }
    public void ReloadBtnOnClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_Click,1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitBtnOnClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_Click,1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex-1);
    }
}
