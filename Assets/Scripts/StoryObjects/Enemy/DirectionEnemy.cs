using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionEnemy : Entity
{
    [SerializeField] float speed;
    Vector2 maxPos;
    [SerializeField] bool dynamicX;
    [SerializeField] Rigidbody2D physics;
    [SerializeField] bool canJump;
    [SerializeField] float xJumpOffset;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float xSizeRaycastMultiplier;
    [SerializeField] Vector3 raycastOffset;
    [SerializeField] LayerMask reboundLayers;
    bool directionMax = true;
    Vector3 basePosition;
    bool updateVelocity = true;
    Vector2 direction;
    bool instantiated = false;
    public void SetMaxPos(Vector3 maxPosX, Vector3 maxPosY)
    {
        basePosition = transform.position;
        instantiated = true;
        if (dynamicX)
        {
            maxPos = maxPosX;
        }
        else
        {
            maxPos = maxPosY;
        }
    }
    public virtual void Update()
    {
        if (instantiated)
        {
            if (directionMax)
            {

                if (updateVelocity)
                {
                    updateVelocity = false;
                    physics.velocity = maxPos.normalized * speed;
                }
                if ((dynamicX && (transform.position.x >= basePosition.x + maxPos.x)) || !dynamicX && (transform.position.y >= basePosition.y + maxPos.y))
                {
                    updateVelocity = true;
                    directionMax = false;
                    direction = -(maxPos.normalized);
                }
            }
            else
            {
                if (updateVelocity)
                {
                    updateVelocity = false;
                    physics.velocity = -(maxPos.normalized * speed);
                }

                if ((dynamicX && (transform.position.x <= basePosition.x)) || !dynamicX && (transform.position.y <= basePosition.y))
                {
                    updateVelocity = true;
                    directionMax = true;
                    direction = maxPos.normalized;
                }
            }
            if (canJump)
            {
                Debug.DrawLine(transform.position + raycastOffset, new Vector3(transform.position.x + ((GetRenderer().bounds.size.x * xSizeRaycastMultiplier)) * direction.x, transform.position.y, 0));
                if (Physics2D.Raycast(transform.position + raycastOffset, direction, (GetRenderer().bounds.size.x * xSizeRaycastMultiplier), groundMask).collider)
                {
                    transform.Translate(new Vector3(xJumpOffset * direction.x, 1, 0));
                    updateVelocity = true;
                }
            }
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & reboundLayers) != 0)
        {
            directionMax = !directionMax;
            updateVelocity = true;
        }
        else if (gameObject.layer != groundMask)
        {
            updateVelocity = true;
        }
        if (other.gameObject.layer == 9)
        {
            DealDamage(other.gameObject.GetComponent<Entity>());
        }
    }

    public override void StatAdjustments()
    {
        base.StatAdjustments();
        speed *= StatMultipliers()[3];
    }

    public void MultiplySpeed(float multiplier)
    {
        speed *= multiplier;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return speed;
    }
}


