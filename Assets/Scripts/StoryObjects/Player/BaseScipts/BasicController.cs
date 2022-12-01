using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : Player
{
    [SerializeField] float movementSpeed;
    [SerializeField] float airSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] KeyCode jumpButton;
    [SerializeField] Rigidbody2D playerPhysics;
    [SerializeField] Collider2D groundTrigger;
    [SerializeField] AudioClip jumpNoise;
    bool grounded = false;
    bool jumpInput = false;

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        if (!Validate())
        {
            Debug.LogError("This StoryObject does not meet standards");
        }
    }

    public override bool Validate()
    {
        return playerPhysics != null && groundTrigger != null;
    }
    public override void StatAdjustments()
    {
        base.StatAdjustments();
        jumpHeight *= StatMultipliers()[3];
        movementSpeed *= StatMultipliers()[4];
        airSpeed *= StatMultipliers()[5];
    }
    void FixedUpdate()
    {
        if (jumpInput)
        {
            jumpInput = false;
            Jump();
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (grounded)
            {
                Move(Input.GetAxis("Horizontal") * movementSpeed);
            }
            else
            {
                Move(Input.GetAxis("Horizontal") * airSpeed);
            }
        }
        else
        {
            if (grounded && playerPhysics.velocity.x != 0)
            {
                playerPhysics.velocity = new Vector2(0, playerPhysics.velocity.y);
            }
        }
    }

    public override void Update()
    {
        base.Update();
        if (!jumpInput)
        {
            jumpInput = Input.GetKeyDown(jumpButton);
        }
    }
    public virtual void Move(float speed)
    {
        playerPhysics.velocity = new Vector2(speed, playerPhysics.velocity.y);
    }
    void Jump()
    {
        if (grounded)
        {
            AudioSource.PlayClipAtPoint(jumpNoise, transform.position);
            playerPhysics.velocity = new Vector2(playerPhysics.velocity.x, jumpHeight);
        }
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

    public override float GetMovementSpeed()
    {
        return movementSpeed;
    }
    public override void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }


}
