using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : FreeFlight
{
    public override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        OnDeath();
    }
}
