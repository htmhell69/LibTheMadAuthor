using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skunk : BasicController
{
    [SerializeField] GameObject projectile;
    [SerializeField] Vector3 projectileOffset;
    float bulletDamage = 1;
    float bulletRange = 1;

    // Start is called before the first frame update
    public override void SpecialAbility()
    {
        base.SpecialAbility();
        Instantiate(projectile, transform.position + projectileOffset, Quaternion.identity).GetComponent<Projectile>().Init(0, bulletDamage, bulletRange, false, gameObject);
    }

    public override void StatAdjustments()
    {
        base.StatAdjustments();
        bulletDamage *= StatMultipliers()[6];
        bulletRange *= StatMultipliers()[7];
    }



}
