using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StoryUI : MonoBehaviour
{
    [SerializeField] UiHandler ui;
    [SerializeField] string[] dialogue;
    [SerializeField] TMP_Text text;
    int dialogueIndex = 0;
    void Start()
    {
        InputManager.OnMouseDown += SwapDialogue;
        text.text = dialogue[0];
    }

    void SwapDialogue(object sender, MouseEvent mouseEvent)
    {
        if (gameObject.activeSelf)
        {
            if (mouseEvent.mouseButton == 0)
            {
                dialogueIndex++;
                if (dialogueIndex != dialogue.Length)
                {
                    text.text = dialogue[dialogueIndex];
                }
                else
                {
                    ui.SwitchMenu(UiHandler.Menu.BookCreation);
                }
            }
        }

    }
}
