using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
   Button startButton;

   private void Awake()
   {
      startButton = GetComponent<Button>();
      startButton.onClick.AddListener(LoadGameSecene);
   }

   private void LoadGameSecene()
   {
      AudioManager.Instance.PlayClip(Config.btn_Click,1);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
}
