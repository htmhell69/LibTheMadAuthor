using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class UiHandler : MonoBehaviour
{
    public enum Menu
    {
        None = -1,
        BookCreation = 0
    }
    [SerializeField] GameObject[] menus;
    [SerializeField] Menu currentMenu;
    Menu previousMenu;
    Func<int, bool> callback;

    public void SwitchMenu(Menu menu)
    {
        if (currentMenu != Menu.None)
        {
            menus[(int)menu].SetActive(false);
        }
        if (menu != Menu.None)
        {
            menus[(int)menu].SetActive(true);
        }
        previousMenu = currentMenu;
        currentMenu = menu;
    }

    public void SwitchMenu(Menu menu, Func<int, bool> callback)
    {
        SwitchMenu(menu);
        Debug.Log(callback);
        this.callback = callback;
    }

    public void Back()
    {
        SwitchMenu(previousMenu);
    }
    public void SetCallBack(Func<int, bool> callback)
    {
        this.callback = callback;
    }

    public void RunCallBack(int buttonIndex)
    {
        callback(buttonIndex);
    }
    void Start()
    {
        if (currentMenu != Menu.None)
        {
            SwitchMenu(currentMenu);
        }
    }
}

