using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] float maxHp;
    [SerializeField] float defense;
    [SerializeField] float attack;
    [SerializeField] int statLevel;
    [SerializeField] float iFrameTime;
    [SerializeField] byte iFrameTransparency;
    [SerializeField] float[] statMultipliers = new float[3];
    [SerializeField] AudioClip hurtNoise;
    [SerializeField] SpriteRenderer spriteRenderer;
    float currentHp;
    float iFramesLeft;
    float minDamage = 0.25f;
    public virtual float TakeDamage(float attack)
    {
        if (iFramesLeft <= 0)
        {
            AudioSource.PlayClipAtPoint(hurtNoise, transform.position);
            float damage = CalculateDamage(attack);
            if (currentHp - damage <= 0)
            {
                OnDeath();
            }
            StartIFrames(iFrameTime);
            return currentHp -= damage;
        }
        return currentHp;
    }

    public virtual float Heal(float amount)
    {
        currentHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        return currentHp;
    }
    public float DealDamage(Entity entity)
    {
        return entity.TakeDamage(attack);
    }

    public float DealDamage(Entity entity, float damageMultiplier)
    {
        return entity.TakeDamage(attack * damageMultiplier);
    }

    public void AdjustStats(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            StatAdjustments();
        }
    }

    public virtual void StatAdjustments()
    {
        statLevel++;
        float oldMaxHp = maxHp;
        maxHp *= statMultipliers[0];
        defense *= statMultipliers[1];
        attack *= statMultipliers[2];
        Heal(maxHp - oldMaxHp);
    }
    public void AdjustStats()
    {
        AdjustStats(statLevel);
    }
    public virtual void Init(GameManager gameManager)
    {
        if (statLevel != 1)
        {
            AdjustStats(statLevel - 1);
        }
        currentHp = maxHp;
    }
    public float[] StatMultipliers()
    {
        return statMultipliers;
    }

    public int Level()
    {
        return statLevel;
    }
    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }

    IEnumerator IFrames()
    {
        Color32 color = spriteRenderer.color;
        spriteRenderer.color = new Color32(color.r, color.g, color.b, iFrameTransparency);
        while (iFramesLeft >= 0)
        {
            iFramesLeft -= Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = new Color32(color.r, color.g, color.b, 255);
        IFramesEnd();
    }

    public virtual void IFramesEnd()
    {

    }

    public void StartIFrames(float duration)
    {
        if (iFramesLeft <= 0)
        {
            iFramesLeft = duration;
            StartCoroutine("IFrames");
        }
        else
        {
            iFramesLeft = duration;
        }
    }
    public SpriteRenderer GetRenderer()
    {
        return spriteRenderer;
    }

    public float CalculateDamage(float attack)
    {
        float damage = attack - defense;
        if (damage < minDamage)
        {
            damage = minDamage;
        }
        return damage;
    }

    public float GetMaxHp()
    {
        return maxHp;
    }
    public float GetCurrentHp()
    {
        return currentHp;
    }
}
