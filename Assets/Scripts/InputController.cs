using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    private Vector2 axis;

    public Vector2 Axis
    {
        get
        {
            return this.axis;
        }
        set
        {
            this.axis = value;
        }
    }
    void Start () {
        axis.y = 0;
	}
	
	// Update is called once per frame
	void Update () {
        axis.x = Input.GetAxis("Horizontal");
        //axis.y = Input.GetAxis("Vertical");
        axis.y = 0;
        if (Input.GetButtonDown("Jump") && GetComponent<PlayerController>().JumpCount != 0)
        {
            axis.y = 1;
            GetComponent<PlayerController>().Jump = true;
        }
        GetComponent<PlayerController>().Speed = axis;

    }
}
