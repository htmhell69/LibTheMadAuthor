using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttack : Item
{
    [SerializeField] float damageMultiplier;
    bool isActive;
    public override bool Use(int level)
    {
        if (base.Use(level))
        {
            SetTrigger(false);
            isActive = true;
            return true;
        }
        return false;
    }


    public override void AnimationEnd()
    {
        isActive = false;
        SetTrigger(true);
        base.AnimationEnd();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (isActive)
        {
            Entity collidedEntity = other.gameObject.GetComponent<Entity>();
            if (GetPlayer() && collidedEntity)
            {
                GetPlayer().DealDamage(collidedEntity, damageMultiplier);
            }
        }
    }

}
