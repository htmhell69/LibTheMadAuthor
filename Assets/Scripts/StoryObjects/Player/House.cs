using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : BasicController
{
    [SerializeField] float bigDuration;
    [SerializeField] Vector3 bigSize;
    [SerializeField] Vector3 normalSize;
    [SerializeField] float grownYOffset;
    bool isBig = false;
    // Start is called before the first frame update
    public override void SpecialAbility()
    {
        base.SpecialAbility();
        StartIFrames(bigDuration);
        isBig = true;
        transform.localScale = bigSize;
        transform.Translate(new Vector3(0, grownYOffset, 0));
    }

    public override void IFramesEnd()
    {
        if (isBig)
        {
            isBig = false;
            transform.localScale = normalSize;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if (isBig)
            {
                DealDamage(other.gameObject.GetComponent<Entity>());
            }
        }
    }
}
