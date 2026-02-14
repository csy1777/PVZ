using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShovelManager : SingleTon<ShovelManager>
{
    public Button shovelButton;
    public Texture2D shovelTexture;
    public LayerMask targetLayer;
    public GameObject growSoil;
    private bool hasShovel=false;
    private Vector2 shovelHotspot;
    private Vector2 mousePos;
    private Collider2D currentPlantCollider;

    protected override void Awake()
    {
        base.Awake();
        shovelButton.onClick.AddListener(ShovelButtonClick);
        shovelHotspot=new Vector2(35,37);
    }

    private void Update()
    {
        if (hasShovel)
        {
            if (FindPlant())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    AudioManager.Instance.PlayClip(Config.plant, 1);
                    GameObject go=Instantiate(growSoil, mousePos, Quaternion.identity);
                    Destroy(go,.5f);
                    currentPlantCollider.gameObject.GetComponent<Plant>()
                        .TakeDamage(currentPlantCollider.GetComponent<Plant>().Hp);
                    hasShovel = false;
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    hasShovel = false;
                }
            }
        }
    }

    public void ShovelButtonClick()
    {
        AudioManager.Instance.PlayClip(Config.shovel,1);
        Cursor.SetCursor(shovelTexture,shovelHotspot,CursorMode.Auto);
        hasShovel = true;
    }

    private bool FindPlant()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPlantCollider = Physics2D.OverlapPoint(mousePos, targetLayer);

        if (currentPlantCollider != null)
        {
            return true;
        }
        return false;
    }
    
}
