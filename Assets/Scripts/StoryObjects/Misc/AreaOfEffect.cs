using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : Projectile
{
    // Start is called before the first frame update

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void OnCollisionEnter2D(Collision2D other)
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsSource(other.gameObject))
        {
            if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
            {
                other.gameObject.GetComponent<Entity>().TakeDamage(GetDamage() / 2);
            }
        }
    }
}
