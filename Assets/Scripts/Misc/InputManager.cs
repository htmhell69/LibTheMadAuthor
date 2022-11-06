using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode[] keyCodes;
    public static event EventHandler<KeyEvent> OnKeyPressed;
    public static event EventHandler<KeyEvent> OnKeyHeld;
    public void Update()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]) && OnKeyPressed != null)
                {
                    OnKeyPressed(this, new KeyEvent(keyCodes[i]));
                }
            }
        }
        if (Input.anyKey)
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKey(keyCodes[i]) && OnKeyHeld != null)
                {
                    OnKeyHeld(this, new KeyEvent(keyCodes[i]));
                }
            }
        }
    }
}
public class KeyEvent : EventArgs
{
    public KeyCode keyPressed;
    public KeyEvent(KeyCode keyPressed)
    {
        this.keyPressed = keyPressed;
    }
}