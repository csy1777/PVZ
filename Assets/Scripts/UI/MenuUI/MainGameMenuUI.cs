using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainGameMenuUI : StartGameMenuUI
{
    public override void Exit()
    {
        AudioManager.Instance.PlayClip(Config.btn_Click,1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex-1);
    }
}
