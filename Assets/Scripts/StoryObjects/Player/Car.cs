using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : BasicController
{
    [SerializeField] float speedMultiplier;
    [SerializeField] float speedDuration;
    bool isFast = false;
    float originalSpeed;
    // Start is called before the first frame update
    public override void SpecialAbility()
    {
        base.SpecialAbility();
        StartIFrames(speedDuration);
        originalSpeed = GetMovementSpeed();
        SetMovementSpeed(GetMovementSpeed() * speedMultiplier);
        isFast = true;
    }

    public override void IFramesEnd()
    {
        if (isFast)
        {
            isFast = false;
            SetMovementSpeed(originalSpeed);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if (isFast)
            {
                DealDamage(other.gameObject.GetComponent<Entity>());
            }
        }
    }


}
