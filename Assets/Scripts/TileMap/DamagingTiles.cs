using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingTiles : MonoBehaviour
{
    [SerializeField] float damage;

    void OnCollisionStay2D(Collision2D other)
    {
        Entity entity = other.gameObject.GetComponent<Entity>();
        if (entity != null)
        {
            entity.TakeDamage(damage);
        }
    }
}
