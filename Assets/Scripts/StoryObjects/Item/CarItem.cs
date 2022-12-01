using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarItem : Item
{
    [SerializeField] float speedMultiplier;
    float baseSpeed;
    public override bool Use(int level)
    {
        if (base.Use(level))
        {
            baseSpeed = GetPlayer().GetMovementSpeed();
            GetPlayer().SetMovementSpeed(baseSpeed * speedMultiplier);
            return true;
        }
        return false;
    }
    public override void AnimationEnd()
    {
        GetPlayer().SetMovementSpeed(baseSpeed);
    }
}
