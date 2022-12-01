using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ufo : FreeFlight
{
    [SerializeField] float maxFuel;
    [SerializeField] float refuelSpeed;
    [SerializeField] float fallSpeed;
    [SerializeField] GameObject bullet;
    float bulletSpeed = 1;
    float bulletDamage = 1;
    float bulletRange = 1;
    [SerializeField] float bulletCost = 1;
    [SerializeField] Vector3 projectileOffset;
    Slider fuelSlider;
    public override void Move(Vector2 translation)
    {
        if (fuelSlider.value > 0 && (!IsGrounded() || (translation.x == 0 && translation.y > 0)))
        {
            base.Move(translation);
            fuelSlider.value -= Time.deltaTime;
        }
    }
    public override void Update()
    {
        base.Update();
        if (IsGrounded() && ActiveRigidbody().velocity.y <= 0)
        {
            if (fuelSlider.value != maxFuel)
            {
                fuelSlider.value += Time.deltaTime * refuelSpeed;
                if (fuelSlider.value < 0)
                {
                    fuelSlider.value = 0;
                }
                else if (fuelSlider.value > maxFuel)
                {
                    fuelSlider.value = maxFuel;
                }
            }
            if (ActiveRigidbody().velocity.x != 0)
            {
                ActiveRigidbody().velocity = new Vector2(0, ActiveRigidbody().velocity.y);
            }
        }
        else if (fuelSlider.value <= 0)
        {
            if (ActiveRigidbody().gravityScale == 0)
            {
                ActiveRigidbody().gravityScale = fallSpeed;
            }
        }
        else
        {
            if (ActiveRigidbody().gravityScale != 0)
            {
                ActiveRigidbody().gravityScale = 0;
            }
        }
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        fuelSlider = gameManager.GetStatusBar(2);
        fuelSlider.gameObject.SetActive(true);
        fuelSlider.maxValue = maxFuel;
        fuelSlider.value = maxFuel;
    }
    public override void StatAdjustments()
    {
        base.StatAdjustments();
        maxFuel *= StatMultipliers()[5];
        refuelSpeed *= StatMultipliers()[6];
        bulletSpeed *= StatMultipliers()[7];
        bulletDamage *= StatMultipliers()[8];
        bulletRange *= StatMultipliers()[9];
    }
    public override void SpecialAbility()
    {
        if (!IsGrounded() && fuelSlider.value > bulletCost)
        {
            base.SpecialAbility();
            Instantiate(bullet, transform.position + projectileOffset, Quaternion.identity).GetComponent<Projectile>().Init(bulletSpeed, bulletDamage, bulletRange, false, gameObject);
            fuelSlider.value -= bulletCost;
        }
    }
}
