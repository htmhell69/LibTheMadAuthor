using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D physicsBody;
    [SerializeField] float baseSpeed;
    [SerializeField] float baseDamage;
    [SerializeField] float baseRange;
    bool initialized = false;
    float speed;
    float damage;
    float range;
    GameObject source;
    public void Init(float speedMultiplier, float damageMultiplier, float rangeMultiplier, bool left, GameObject source)
    {
        initialized = true;
        this.speed = baseSpeed * speedMultiplier;
        this.damage = baseDamage * damageMultiplier;
        this.range = baseRange * rangeMultiplier;
        this.source = source;
        if (!left)
        {
            physicsBody.velocity = new Vector2(speed, 0);
        }
        else
        {
            physicsBody.velocity = new Vector2(-speed, 0);
        }
    }

    public virtual void Update()
    {
        if (initialized)
        {
            range -= Time.deltaTime;
            if (range <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject != source)
        {
            if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
            {
                other.gameObject.GetComponent<Entity>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public bool IsSource(GameObject gameObject)
    {
        return gameObject == source;
    }

    public float GetDamage()
    {
        return damage;
    }
}
