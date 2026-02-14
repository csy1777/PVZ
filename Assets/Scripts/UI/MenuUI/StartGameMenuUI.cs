using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameMenuUI : MonoBehaviour
{
    public Button Btn_ShowGameMenu;
    public Image GameMenu;
    public Button Btn_Back;
    public Button Btn_Exit;
    public Slider SoundSlider;
    public AudioSource MenuSound;

    protected virtual void Awake()
    {
        Btn_ShowGameMenu.onClick.AddListener(ShowGameMenu);
        Btn_Back.onClick.AddListener(Back);
        Btn_Exit.onClick.AddListener(Exit);
        SoundSlider.value = 1f;
    }

    protected virtual void Update()
    {
        MenuSound.volume = SoundSlider.value;
    }

    public virtual void ShowGameMenu()
    {
        AudioManager.Instance.PlayClip(Config.btn_Click,1);
        Btn_ShowGameMenu.gameObject.SetActive(false);
        GameMenu.gameObject.SetActive(true);
    }

    public virtual void Back()
    {
        AudioManager.Instance.PlayClip(Config.btn_Click,1);
        Btn_ShowGameMenu.gameObject.SetActive(true);
        GameMenu.gameObject.SetActive(false);
    }

    public virtual void Exit()
    {
        AudioManager.Instance.PlayClip(Config.btn_Click,1);
        Application.Quit();
    }
}
