using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe gérant la collision du joueur
public class PlayerCollision : MonoBehaviour {

    // Use this for initialization
    private Vector2 direction;
    private Vector2 positionRay;
    private RaycastHit2D hit;
    private bool onHitX;
    private bool onHitY;

    public bool OnHitX
    {
        get
        {
            return this.onHitX;
        }
        set
        {
            this.onHitX = value;
        }
    }

    public bool OnHitY
    {
        get
        {
            return this.onHitY;
        }
        set
        {
            this.onHitY = value;
        }
    }
	void Start () {
        onHitX = false;
        onHitY = false;
    }
	
    //Fonction qui crée les Ray2D autout du joueur pour les collisions
    void PlayerRaycast()
    {
        direction = new Vector2(GetComponent<PlayerController>().Speed.x.CompareTo(0), 0);
        if (GetComponent<JumpEngine>().IsWallJumping == true)
        {
            Debug.Log(GetComponent<PlayerController>().Speed.x);
            if (GetComponent<JumpEngine>().WasOnWallLeft == true && GetComponent<JumpEngine>().PositionX > 0.1)
            {
                direction.x = 1;
            }
            if (GetComponent<JumpEngine>().WasOnWallRight == true && GetComponent<JumpEngine>().PositionX < -0.1)
            {
                direction.x = -1;
            }
        }
        if (GetComponent<JumpEngine>().PositionY - GetComponent<JumpEngine>().PreviousPositionY > 0 && GetComponent<JumpEngine>().IsJumping == true)
        {
            direction.y = 1;
        }
        else
        {
            direction.y = -1;
        }
        
        if (direction.x != 0)
        {
            if (direction.x == 1)
            {
                positionRay.x = GetComponent<PlayerController>().Position.x + 0.5f;

            }
            else
            {
                positionRay.x = GetComponent<PlayerController>().Position.x - 0.5f;
            }
            for (float i = -1; i < 2; i++)
            {
                if (direction.x == 1)
                {
                    positionRay.x = GetComponent<PlayerController>().Position.x + 0.5f;

                }
                else {
                    positionRay.x = GetComponent<PlayerController>().Position.x - 0.5f;
                }
                positionRay.y = GetComponent<PlayerController>().Position.y + (0.4f* i);
                hit = Physics2D.Raycast(positionRay, new Vector2(direction.x, 0), 2.0f);
                if (hit.collider != null)
                {
                    onHitX = true;
                    float dist = hit.distance;
                    if (direction.x == 1 && GetComponent<PlayerController>().MaxPositionX > (GetComponent<PlayerController>().Position.x + dist))
                    {
                        GetComponent<PlayerController>().MaxPositionX = GetComponent<PlayerController>().Position.x + dist;
                    }
                    else if (direction.x != 1 && GetComponent<PlayerController>().MinPositionX < (GetComponent<PlayerController>().Position.x - dist))
                    {
                        GetComponent<PlayerController>().MinPositionX = GetComponent<PlayerController>().Position.x - dist;
                    }
                }
                else if (onHitX == false)
                {
                    onHitX = false;
                    GetComponent<PlayerController>().MinPositionX = -40;
                    GetComponent<PlayerController>().MaxPositionX = 40;
                }
                //ray = new Ray2D(positionRay, new Vector2(direction.x, 0));
                Debug.DrawRay(positionRay, new Vector2(direction.x*2, 0), Color.green);

            }
        }
        if (direction.y != 0)
        {
            for (float i = -1; i < 2; i++)
            {
                positionRay.x = GetComponent<PlayerController>().Position.x + (0.4f * i);
                if (direction.y == 1) {
                    positionRay.y = GetComponent<PlayerController>().Position.y + 0.5f;

                }
                else
                {
                    positionRay.y = GetComponent<PlayerController>().Position.y - 0.5f;

                }
                hit = Physics2D.Raycast(positionRay, new Vector2(0, direction.y), 2.0f);
                if (hit.collider != null)
                {
                    onHitY = true;
                    float dist = hit.distance;
                    if (direction.y == 1)
                    {
                        GetComponent<PlayerController>().MaxPositionY = GetComponent<PlayerController>().Position.y + dist;
                    }
                    else
                    {
                        GetComponent<PlayerController>().MinPositionY = GetComponent<PlayerController>().Position.y - dist;
                    }
                    //Debug.Log(dist);
                }
                else if (onHitY == false)
                {
                    onHitY = false;
                    GetComponent<PlayerController>().MaxPositionY = 20;
                    GetComponent<PlayerController>().MinPositionY = -20;
                }
                //ray = new Ray2D(positionRay, new Vector2(0, direction.y));
                Debug.DrawRay(positionRay, new Vector2(0, direction.y * 2), Color.green);
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
        PlayerRaycast();
        onHitX = false;
        onHitY = false;
    }
}
