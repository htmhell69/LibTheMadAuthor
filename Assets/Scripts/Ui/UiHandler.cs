using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
public class UiHandler : MonoBehaviour
{
    public enum Menu
    {
        None = -1,
        MainMenu = 0,
        BookCreation = 1,
        StartGame = 2,
        GameOver = 3,
        Win = 4,
        Story = 5
    }
    [SerializeField] GameObject[] menus;
    [SerializeField] Menu currentMenu;
    [SerializeField] ActionText actionText;
    [SerializeField] Slider[] statusBars;
    [SerializeField] Transform canvas;
    Menu previousMenu;
    Func<int, bool> callback;

    public void SwitchMenu(Menu menu)
    {
        if (currentMenu != Menu.None)
        {
            menus[(int)currentMenu].SetActive(false);
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
    public void DisplayText(string message, float duration, Color32 color, float textSize)
    {
        actionText.Display(message, duration, color, textSize);
    }
    public Slider GetStatusBar(int index)
    {
        return statusBars[index];
    }

    public Vector3 CanvasScale()
    {
        return canvas.localScale;
    }

    public Menu CurrentMenu()
    {
        return currentMenu;
    }
}

