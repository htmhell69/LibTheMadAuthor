using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : DirectionEnemy
{
    [SerializeField] float speedUpDuration;
    [SerializeField] float speedMultiplier;
    [SerializeField] float speedUpChance;
    float normalSpeed;
    float timeLeft = 0;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (timeLeft <= 0 && Random.Range(0f, 1f) < speedUpChance)
        {
            timeLeft = speedUpDuration;
            normalSpeed = GetSpeed();
            MultiplySpeed(speedMultiplier);
        }
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                SetSpeed(normalSpeed);
            }
        }
    }
}
