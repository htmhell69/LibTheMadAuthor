using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DifficultySelector : MonoBehaviour
{
    [SerializeField] Difficulty[] difficulties;
    [SerializeField] TMP_Text text;
    [SerializeField] GameManager gameManager;
    int currentIndex = 0;
    public void Start()
    {
        SetDifficulty(0);
    }
    public void Forward()
    {
        if (currentIndex != difficulties.Length - 1)
        {
            currentIndex++;
            SetDifficulty(currentIndex);
        }
    }
    public void Back()
    {
        if (currentIndex != 0)
        {
            currentIndex--;
            SetDifficulty(currentIndex);
        }
    }

    void SetDifficulty(int index)
    {
        gameManager.SetDifficulty(difficulties[index].difficultyLevel);
        text.text = difficulties[index].name;
    }
}

[System.Serializable]
public struct Difficulty
{
    public string name;
    public int difficultyLevel;
}