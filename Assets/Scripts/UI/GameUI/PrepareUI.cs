using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareUI : MonoBehaviour
{
    private Animator anim;
    private Action onComplete;
    private void Awake()
    {
        anim=GetComponent<Animator>();
        anim.enabled = false;
    }

    public void ShowStartUI(Action OnComplete)
    {
        this.onComplete=OnComplete;
        anim.enabled = true;
    }

    public void OnShowComplete()
    {
        onComplete?.Invoke();
    }
}
