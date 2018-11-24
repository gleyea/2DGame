using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform {

    // Use this for initialization
    [SerializeField]
    private Vector2 speed;
    [SerializeField]
    private float maxPosition;
    private float initialPosition;

    public Vector2 Speed
    {
        get
        {
            return this.speed;
        }
        set
        {
            this.speed = value;
        }
    }

    public float MaxPosition
    {
        get
        {
            return this.maxPosition;
        }
        set
        {
            this.maxPosition = value;
        }
    }
    protected override void Init()
    {
        initialPosition = Position.x;
    }

    private void UpdatePosition()
    {
        Position = Position + Speed * Time.deltaTime;
        if ( Mathf.Abs(initialPosition - Position.x) > maxPosition)
        {
            Speed = -Speed;
        }
    }
    void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePosition();
	}
}
