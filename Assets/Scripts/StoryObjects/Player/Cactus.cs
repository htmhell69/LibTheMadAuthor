using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : BasicController
{
    [SerializeField] float spikyDuration;
    [SerializeField] Sprite spikyImage;
    [SerializeField] Sprite normalImage;
    bool isSpiky = false;
    // Start is called before the first frame update

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        StartIFrames(spikyDuration);
        isSpiky = true;
        GetRenderer().sprite = spikyImage;
    }

    public override void IFramesEnd()
    {
        if (isSpiky)
        {
            isSpiky = false;
            GetRenderer().sprite = normalImage;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if (isSpiky)
            {
                DealDamage(other.gameObject.GetComponent<Entity>());
            }
        }
    }
}
