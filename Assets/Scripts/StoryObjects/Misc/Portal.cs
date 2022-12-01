using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    GameManager gameManager;
    bool used = false;
    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9 && !used)
        {
            if (gameManager.LastSection())
            {
                gameManager.GetUiHandler().SwitchMenu(UiHandler.Menu.Win);
            }
            else
            {
                used = true;
                gameManager.LoadSection();
                Destroy(gameObject);
            }

        }

    }
}
