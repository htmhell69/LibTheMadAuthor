using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSwapper : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] UiHandler ui;
    [SerializeField] UiHandler.Menu menu;
    void Start()
    {
        button.onClick.AddListener(delegate { ui.SwitchMenu(menu); });
    }

}
