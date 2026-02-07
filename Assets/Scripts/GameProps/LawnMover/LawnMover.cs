using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum LawnMoverState
{
    Idle,Run
}
public class LawnMover : MonoBehaviour
{
    LawnMoverState state = LawnMoverState.Idle;
    public float speed = 3f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (state)
        {
            case LawnMoverState.Idle:
                IdleUpdate();
                break;
            case LawnMoverState.Run:
                RunUpdate();
                break;
        }
        
    }

    private void IdleUpdate()
    {
        rb.velocity = Vector2.zero;
    }

    private void RunUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        Destroy(gameObject,6f);
    }

    public void TransitionToRun()
    {
        state = LawnMoverState.Run;
    }
}
