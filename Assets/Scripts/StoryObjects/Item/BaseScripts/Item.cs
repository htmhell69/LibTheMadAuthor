using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Vector3 heldOffset;
    [SerializeField] Vector3 heldRotation;
    [SerializeField] ItemAnimation attackAnimation;
    [SerializeField] bool hasAnimation;
    [SerializeField] Collider2D mainCollider;
    [SerializeField] Rigidbody2D physicsBody;
    [SerializeField] bool waitTillAnimationFinished;
    [SerializeField] float cooldown;
    Player player;
    float lastActivated = 0;
    bool isAnimating = false;
    public virtual bool Use(int level)
    {
        if (((waitTillAnimationFinished && !isAnimating) && lastActivated + cooldown < Time.realtimeSinceStartup) || !waitTillAnimationFinished && (lastActivated + cooldown < Time.realtimeSinceStartup))
        {
            lastActivated = Time.realtimeSinceStartup;
            if (hasAnimation)
            {
                if (!isAnimating)
                {
                    StartCoroutine("RunAnimation");
                }
            }
            return true;
        }
        return false;
    }

    public void Equip(Vector3 position, Player player)
    {
        this.player = player;
        mainCollider.isTrigger = true;
        if (physicsBody != null)
        {
            physicsBody.isKinematic = true;
        }
        physicsBody.velocity = Vector2.zero;
        transform.localPosition = position + heldOffset;
        transform.eulerAngles = heldRotation;
        gameObject.layer = 10;
        transform.SetParent(player.transform, false);
    }
    public virtual void Drop()
    {
        mainCollider.isTrigger = false;
        if (physicsBody != null)
        {
            physicsBody.isKinematic = false;
        }
        gameObject.layer = 7;
        transform.SetParent(null, true);
        StopCoroutine("RunAnimation");
        isAnimating = false;
        player = null;
    }

    IEnumerator RunAnimation()
    {
        isAnimating = true;
        Vector3 endPosition = Vector3.zero;
        Vector3 endRotation = Vector3.zero;
        Vector3 endScale = Vector3.zero;
        for (int i = 0; i < attackAnimation.animationClips.Length; i++)
        {
            ItemAnimationClip animationClip = attackAnimation.animationClips[i];
            endPosition = transform.localPosition + animationClip.position;
            endRotation = transform.rotation.eulerAngles + animationClip.rotation;
            endScale = transform.localScale + animationClip.scale;
            float currentTime = 0;
            while (currentTime < animationClip.duration)
            {
                currentTime += Time.deltaTime;
                transform.localPosition += (animationClip.position / animationClip.duration) * Time.deltaTime;
                transform.Rotate((animationClip.rotation / animationClip.duration) * Time.deltaTime);
                transform.localScale += ((animationClip.scale / animationClip.duration) * Time.deltaTime);
                yield return null;
            }
            transform.eulerAngles = endRotation;
            transform.localPosition = endPosition;
        }
        AnimationEnd();
        isAnimating = false;
    }
    public virtual void AnimationEnd()
    {
        if (waitTillAnimationFinished)
        {
            lastActivated = Time.realtimeSinceStartup;
        }
    }
    public void SetTrigger(bool isTrigger)
    {
        mainCollider.isTrigger = isTrigger;
    }

    public Player GetPlayer()
    {
        return player;
    }
}

[System.Serializable]
public struct ItemAnimation
{
    public ItemAnimationClip[] animationClips;
}
[System.Serializable]
public struct ItemAnimationClip
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public float duration;
}