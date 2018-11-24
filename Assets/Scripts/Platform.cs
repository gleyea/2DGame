using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour {

    // Use this for initialization
    private Vector2 position;
    private Vector2 scale;

    private int type;

    public Vector2 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    public Vector2 Scale
    {
        get
        {
            return transform.localScale;
        }
        set
        {
            transform.localScale = value;
        }
    }

    public int Type
    {
        get
        {
            return this.type;
        }
        set
        {
            this.type = value;
        }
    }

    protected virtual void Init()
    {

    }
}
