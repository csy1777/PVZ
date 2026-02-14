using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuUI : MainGameMenuUI
{
    public override void ShowGameMenu()
    {
        base.ShowGameMenu();
        Time.timeScale = 0f;
    }

    public override void Back()
    {
        Time.timeScale = 1f;
        base.Back();
    }

    public override void Exit()
    {
        Time.timeScale = 1f;
        base.Exit();
    }
}
