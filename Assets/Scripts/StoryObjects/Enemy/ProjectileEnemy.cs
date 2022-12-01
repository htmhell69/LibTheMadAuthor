using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : DirectionEnemy
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpawnChance;
    [SerializeField] Vector3 projectileOffset;
    float bulletSpeed = 1;
    float bulletDamage = 1;
    float bulletRange = 1;

    public override void StatAdjustments()
    {
        base.StatAdjustments();
        bulletSpeed *= StatMultipliers()[4];
        bulletDamage *= StatMultipliers()[5];
        bulletRange *= StatMultipliers()[6];
    }
    public override void Update()
    {
        base.Update();
        if (Random.Range(0f, 1f) < projectileSpawnChance)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position + projectileOffset, Quaternion.identity).GetComponent<Projectile>();
            projectile.Init(bulletSpeed, bulletDamage, bulletRange, true, gameObject);
        }
    }
}
