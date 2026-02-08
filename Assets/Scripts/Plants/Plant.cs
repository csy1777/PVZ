using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    enum PlantState
    {
        Disabled,
        Enabled,
    }

    public enum PlantType
    {
        Sunflower,
        PeaShooter,
        DoublePeaShooter
    }
public class Plant : MonoBehaviour
{
    PlantState plantState = PlantState.Disabled;
    public PlantType plantType=PlantType.Sunflower;
    protected Animator anim;
    protected BoxCollider2D collider;
    public int Hp = 100;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Start()
    {
        TransitionToDisable();
    }

    protected void Update()
    {
        switch (plantState)
        {
            case PlantState.Disabled:
                UpdateDisabled();
                break;
            case PlantState.Enabled:
                UpdateEnabled();
                break;
        }
    }

    protected virtual void UpdateDisabled()
    {
        
    } 
    protected virtual void UpdateEnabled()
    {
        
    }

    public void TransitionToDisable()
    {
        plantState = PlantState.Disabled;
        anim.enabled = false;
        collider.enabled = false;
    }
    public void TransitionToEnable()
    {
        plantState = PlantState.Enabled;
        anim.enabled = true;
        collider.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlantManager.Instance.RemovePlant(this);
        Destroy(gameObject);
    }
}
