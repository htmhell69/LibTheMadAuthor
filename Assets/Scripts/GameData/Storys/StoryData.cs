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
    public StoryData(BookCreationData bookData)
    {
        storyObjects = new StoryObject[bookData.coverObjects.Length];
        for (int i = 0; i < storyObjects.Length; i++)
        {
            storyObjects[i] = bookData.coverObjects[i].GetStoryObject();
        }
    }
}