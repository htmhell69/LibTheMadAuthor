using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode[] keyCodes;
    [SerializeField] int[] mouseButtons;
    List<KeyCode> keysPressed = new List<KeyCode>();
    public static event EventHandler<KeyEvent> OnKeyPressed;
    public static event EventHandler<KeyEvent> OnKeyHeld;
    public static event EventHandler<KeyEvent> OnKeyUp;
    public static event EventHandler<MouseEvent> OnMouseDown;
    public static event EventHandler<MouseEvent> OnMouseHeld;
    public void Update()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    if (OnKeyPressed != null)
                    {
                        OnKeyPressed(this, new KeyEvent(keyCodes[i]));
                    }
                    keysPressed.Add(keyCodes[i]);
                }
            }
        }
        if (Input.anyKey)
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKey(keyCodes[i]))
                {
                    if (OnKeyHeld != null)
                    {
                        OnKeyHeld(this, new KeyEvent(keyCodes[i]));
                    }
                }
            }
        }
        if (keysPressed.Count != 0)
        {
            for (int i = 0; i < keysPressed.Count; i++)
            {
                if (!Input.GetKey(keyCodes[i]))
                {
                    if (OnKeyUp != null)
                    {
                        OnKeyUp(this, new KeyEvent(keyCodes[i]));
                    }
                    keysPressed.RemoveAt(i);
                }
            }
        }
        for (int i = 0; i < mouseButtons.Length; i++)
        {
            if (Input.GetMouseButtonDown(mouseButtons[i]))
            {
                if (OnMouseDown != null)
                {
                    OnMouseDown(this, new MouseEvent(mouseButtons[i]));
                }
            }
            if (Input.GetMouseButtonUp(mouseButtons[i]))
            {
                if (OnMouseHeld != null)
                {
                    OnMouseHeld(this, new MouseEvent(mouseButtons[i]));
                }
            }
            if (Input.GetMouseButton(mouseButtons[i]))
            {
                if (OnMouseHeld != null)
                {
                    OnMouseHeld(this, new MouseEvent(mouseButtons[i]));
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

public class MouseEvent : EventArgs
{
    public int mouseButton;
    public MouseEvent(int mouseButton)
    {
        this.mouseButton = mouseButton;
    }
}