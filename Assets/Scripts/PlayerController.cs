using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe gérant les contrôles du joueur excepté les sauts
public class PlayerController : MonoBehaviour {

    private Vector2 speed;
    [SerializeField]
    private Vector2 maxSpeed;
    private Vector2 jumpingPosition;
    private Vector2 previousPosition;
    private float minPositionX;
    private float minPositionY;
    private float maxPositionX;
    private float maxPositionY;
    private bool onGround;
    private bool onWallLeft;
    private bool onWallRight;
    private bool jump;
    private int jumpCount;
    private int maxJumpCount;

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

    public Vector2 MaxSpeed
    {
        get
        {
            return this.maxSpeed;
        }
        set
        {
            this.maxSpeed = value;
        }
    }

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

    public Vector2 PreviousPosition
    {
        get
        {
            return previousPosition;
        }
        set
        {
            this.previousPosition = value;
        }
    }
    
    public Vector2 JumpingPosition
    {
        get
        {
            return this.jumpingPosition;
        }
        set
        {
            this.jumpingPosition = value;
        }
    }
    public float MinPositionX
    {
        get
        {
            return this.minPositionX;
        }
        set
        {
            this.minPositionX = value;
        }
    }

    public float MinPositionY
    {
        get
        {
            return this.minPositionY;
        }
        set
        {
            this.minPositionY = value;
        }
    }

    public float MaxPositionX
    {
        get
        {
            return this.maxPositionX;
        }
        set
        {
            this.maxPositionX = value;
        }
    }

    public float MaxPositionY
    {
        get
        {
            return this.maxPositionY;
        }
        set
        {
            this.maxPositionY = value;
        }
    }

    public bool OnGround
    {
        get
        {
            return this.onGround;
        }
        set
        {
            this.onGround = value;
        }
    }

    public bool OnWallLeft
    {
        get
        {
            return this.onWallLeft;
        }
        set
        {
            this.onWallLeft = value;
        }
    }

    public bool OnWallRight
    {
        get
        {
            return this.onWallRight;
        }
        set
        {
            this.onWallRight = value;
        }
    }

    public bool Jump
    {
        get
        {
            return this.jump;
        }
        set
        {
            this.jump = value;
        }
    }

    public int JumpCount
    {
        get
        {
            return this.jumpCount;
        }
        set
        {
            this.jumpCount = value;
        }
    }
    void Start()
    {
        MinPositionX = -40;
        MinPositionY = -20;
        MaxPositionX = 40;
        MaxPositionY = 20;
        maxJumpCount = 2;
        jumpCount = maxJumpCount;
        previousPosition = Position;
        onWallLeft = false;
        onWallRight = false;
    }

    //Fonction qui permet de savoir si le joueur touche le sol
    void IsOnGround()
    {
        if (Position.y == MinPositionY && jump == false)
        {
            onGround = true;
            GetComponent<JumpEngine>().TimeJump = 0;
            jumpCount = maxJumpCount;
        }
        else
        {
            onGround = false;
        }
    }

    //Fonction qui permet de savoir si le joueur touche un mur
    void IsOnWall()
    {
        if (Position.x == MaxPositionX )
        {
            if (onWallRight == false)
            {
                jumpCount = 1;
            }
            onWallLeft = false;
            onWallRight = true;
        }
        else if (Position.x == MinPositionX)
        {
            if (onWallLeft == false)
            {
                jumpCount = 1;
            }
            onWallLeft = true;
            onWallRight = false;
        }
        else
        {
            onWallLeft = false;
            onWallRight = false;
        }
    }
    void Update()
    {
        if (jump == true)
        {
            jumpCount--;
            jumpingPosition = Position;
            speed.y = 1;
            GetComponent<JumpEngine>().PositionY = Position.y;
            GetComponent<JumpEngine>().IsJumping = true;
            jump = false;
        }
        if (GetComponent<JumpEngine>().IsJumping == false)
        {
            if (onWallLeft == true || onWallRight == true)
            {
                Position += new Vector2(0, (GetComponent<JumpEngine>().Gravity *0.5f) * Time.deltaTime);

            }
            else
            {
                Position += new Vector2(0, GetComponent<JumpEngine>().Gravity * Time.deltaTime);
            }

        }
        Position += new Vector2(Speed.x * MaxSpeed.x * Time.deltaTime, 0);
        if (Position.x < MinPositionX)
        {
            Position = new Vector2(MinPositionX, Position.y);
        }
        if (Position.x > MaxPositionX)
        {
            Position = new Vector2(MaxPositionX, Position.y);

        }
        if (Position.y < MinPositionY)
        {
            GetComponent<JumpEngine>().IsJumping = false;
            GetComponent<JumpEngine>().IsWallJumping = false;
            GetComponent<JumpEngine>().WasOnWallLeft = false;
            GetComponent<JumpEngine>().WasOnWallRight = false;
            Position = new Vector2(Position.x, MinPositionY);
            speed.y = 0;
        }
        if (Position.y > MaxPositionY)
        {
            GetComponent<JumpEngine>().IsJumping = false;
            GetComponent<JumpEngine>().IsWallJumping = false;
            GetComponent<JumpEngine>().WasOnWallLeft = false;
            GetComponent<JumpEngine>().WasOnWallRight = false;
            Position = new Vector2(Position.x, MaxPositionY);
            speed.y = 0;
        }
        IsOnWall();
        IsOnGround();
    }
}