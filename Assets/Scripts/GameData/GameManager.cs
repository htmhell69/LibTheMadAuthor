using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Story[] storys;

    Story story;

    public void Start()
    {
        story = storys[Random.Range(0, storys.Length)];
    }

    public Story CurrentStory()
    {
        return story;
    }

    public void UpdateStoryObject()
    {

    }
}

public class InGameStoryObject
{

}