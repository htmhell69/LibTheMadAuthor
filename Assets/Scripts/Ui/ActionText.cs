using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ActionText : MonoBehaviour
{
    TMP_Text text;
    float duration;
    public void Display(string message, float duration, Color32 color, float textSize)
    {
        gameObject.SetActive(true);
        if (text == null)
        {
            text = GetComponent<TMP_Text>();
        }
        this.duration = duration;
        text.text = message;
        text.color = color;
        text.fontSize = textSize;
    }

    void Update()
    {
        if (duration <= 0 && text.text != "")
        {
            text.text = "";
            gameObject.SetActive(false);
        }

        if (duration >= 0)
        {
            duration -= 1 * Time.deltaTime;
        }
    }
}
