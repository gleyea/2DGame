using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe gérant tous les sauts dans le jeu
public class JumpEngine : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    private float gravity;
    private float acceleration;
    private bool isJumping;
    private float positionX;
    private float positionY;
    private float previousPositionY;
    private float timeJump;
    [SerializeField]
    private float initialSpeedX;
    [SerializeField]
    private float initialSpeedY;
    private bool initialSpeedXBool;
    [SerializeField]
    private float thresholdPositionX;
    private bool isWallJumping;
    private bool wasOnWallLeft;
    private bool wasOnWallRight;

    public float Gravity
    {
        get
        {
            return this.gravity;
        }
        set
        {
            this.gravity = value;
        }
    }
    public bool IsJumping
    {
        get
        {
            return this.isJumping;
        }
        set
        {
            this.isJumping = value;
        }
    }

    public bool IsWallJumping
    {
        get
        {
            return this.isWallJumping;
        }
        set
        {
            this.isWallJumping = value;
        }
    }

    public bool WasOnWallLeft
    {
        get
        {
            return this.wasOnWallLeft;
        }
        set
        {
            this.wasOnWallLeft = value;
        }
    }
    public bool WasOnWallRight
    {
        get
        {
            return this.wasOnWallRight;
        }
        set
        {
            this.wasOnWallRight = value;
        }
    }
    public float PositionY
    {
        get
        {
            return this.positionY;
        }
        set
        {
            this.positionY = value;
        }
    }

    public float PositionX
    {
        get
        {
            return this.positionX;
        }
        set
        {
            this.positionX = value;
        }
    }
    public float PreviousPositionY
    {
        get
        {
            return this.previousPositionY;
        }
        set
        {
            this.previousPositionY = value;
        }
    }
    public float TimeJump
    {
        get
        {
            return this.timeJump;
        }
        set
        {
            this.timeJump = value;
        }
    }
    void Start()
    {
        isJumping = false;
        timeJump = 0;
        previousPositionY = positionY;
        isWallJumping = false;
        wasOnWallLeft = false;
        wasOnWallRight = false;
        positionX = 0;
    }

    //Fonction gérant les sauts (saut normal et wall jump)
    private void Jump()
    {
        previousPositionY = positionY;
        timeJump += Time.deltaTime;
        positionY = (0.5f * gravity * Mathf.Pow(timeJump, 2)) + (initialSpeedY * timeJump);
        if (isWallJumping == true)
        {
            WallJump();
        }
        else 
        {
            if (GetComponent<PlayerController>().OnWallLeft || GetComponent<PlayerController>().OnWallRight)
            {
                GetComponent<PlayerController>().Position = new Vector2(GetComponent<PlayerController>().Position.x, GetComponent<PlayerController>().JumpingPosition.y + positionY);

            }
            else
            {
                GetComponent<PlayerController>().Position = new Vector2(GetComponent<PlayerController>().Position.x, GetComponent<PlayerController>().JumpingPosition.y + positionY);
            }
        }
        if (GetComponent<PlayerController>().Jump == true)
        {
            timeJump = 0;
            if (GetComponent<PlayerController>().OnWallRight == true || GetComponent<PlayerController>().OnWallLeft == true)
            {
                isWallJumping = true;
                positionX = 0;
            }
        }
    }

    //Fonction gérant les walljump
    private void WallJump()
    {
        if ((GetComponent<PlayerController>().OnWallRight == true || wasOnWallRight == true) && wasOnWallLeft == false)
        {
            if (GetComponent<PlayerController>().Speed.x > 0 || GetComponent<PlayerController>().OnWallLeft == true)
            {
                wasOnWallRight = false;
                isWallJumping = false;
                positionX = 0;
            }
            else
            {
                wasOnWallRight = true;
                positionX = -(initialSpeedX * Time.deltaTime);
            }
        }
        else if (GetComponent<PlayerController>().OnWallLeft == true || wasOnWallLeft == true)
        {
            if ((GetComponent<PlayerController>().Speed.x < 0 && GetComponent<PlayerController>().OnWallLeft == false) || GetComponent<PlayerController>().OnWallRight == true)
            {
                wasOnWallLeft = false;
                isWallJumping = false;
                positionX = 0;
            }
            else
            {
                wasOnWallLeft = true;
                positionX = (initialSpeedX * Time.deltaTime);
            }
        }
        GetComponent<PlayerController>().Position = new Vector2(GetComponent<PlayerController>().Position.x + positionX, GetComponent<PlayerController>().JumpingPosition.y + positionY);
    }
    // Update is called once per frame
    void Update()
    {
        if (IsJumping == true)
        {
            Jump();
        }
    }
}
