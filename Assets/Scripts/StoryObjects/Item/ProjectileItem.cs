using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileItem : Item
{
    [SerializeField] GameObject projectile;
    [SerializeField] Vector3 bulletOffset;
    [SerializeField] float bulletSpeedMultiplier;
    [SerializeField] float bulletDamageMultiplier;
    [SerializeField] float bulletRangeMultiplier;
    public override bool Use(int level)
    {
        if (base.Use(level))
        {
            Instantiate(projectile, transform.position + bulletOffset, Quaternion.identity).GetComponent<Projectile>().Init(1 + (level - 1) * bulletSpeedMultiplier,
            1f + (level - 1) * bulletDamageMultiplier, 1f + (level - 1) * bulletRangeMultiplier, false, gameObject);
            return true;
        }
        return false;
    }
}
