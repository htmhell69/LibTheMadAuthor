using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Portal portal;

    public void Init(GameManager gameManager)
    {
        portal.SetGameManager(gameManager);
    }



}
