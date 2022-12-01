using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFlight : Player
{
    [SerializeField] float movementSpeed;
    [SerializeField] float airSpeed;
    [SerializeField] Rigidbody2D playerPhysics;
    [SerializeField] Collider2D groundTrigger;
    [SerializeField] float maxY;
    bool grounded = false;
    bool canStop = false;

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
    }
    public override void Update()
    {
        base.Update();
        if (transform.position.y >= maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, 0);
            if (playerPhysics.velocity.y > 0)
            {
                playerPhysics.velocity = new Vector3(transform.position.x, maxY, 0);
            }
        }
    }
    public override bool Validate()
    {
        return playerPhysics != null && groundTrigger != null;
    }
    public override void StatAdjustments()
    {
        base.StatAdjustments();
        movementSpeed *= StatMultipliers()[3];
        airSpeed *= StatMultipliers()[4];
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            canStop = true;
            if (grounded)
            {
                Move(new Vector2(Input.GetAxis("Horizontal") * movementSpeed, Input.GetAxis("Vertical") * movementSpeed));
            }
            else
            {
                Move(new Vector2(Input.GetAxis("Horizontal") * airSpeed, Input.GetAxis("Vertical") * airSpeed));
            }
        }
        else
        {
            if (canStop)
            {
                canStop = false;
                playerPhysics.velocity = Vector2.zero;
            }
        }
    }
    public virtual void Move(Vector2 translation)
    {
        playerPhysics.velocity = translation;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            grounded = false;
        }
    }
    public bool IsGrounded()
    {
        return grounded;
    }
    public Rigidbody2D ActiveRigidbody()
    {
        return playerPhysics;
    }
    public override float GetMovementSpeed()
    {
        return movementSpeed;
    }
    public override void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }
}
