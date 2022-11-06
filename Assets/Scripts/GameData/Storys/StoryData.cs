using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StoryData
{
    [SerializeField] StoryObject[] storyObjects;
    public StoryObject GetStoryObject(int index)
    {
        return storyObjects[index];
    }
}