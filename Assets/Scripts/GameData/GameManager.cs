using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Story[] storys;
    [SerializeField] CoverObject[] coverObjects;
    StoryData storyData;
    InGameStoryObject[] storyObjects;
    Story story;

    public void Start()
    {
        story = storys[Random.Range(0, storys.Length)];
    }
    public void StartGame(StoryData storyData)
    {
        this.storyData = storyData;
    }

    public Story CurrentStory()
    {
        return story;
    }

    public Story ChooseStory()
    {
        story = storys[Random.Range(0, storys.Length)];
        return story;
    }
    public void UpdateStoryObject(SectionContext context)
    {
        switch (context.GetBaseParams().action)
        {
            case StoryObjectAction.Create:
                context.GetExtraParams();
                break;
        }
    }

    public CoverObject[] GenerateCoverObjects(int amount)
    {
        CoverObject[] covers = new CoverObject[amount];
        for (int i = 0; i < amount; i++)
        {
            covers[i] = coverObjects[Random.Range(0, coverObjects.Length)];
        }
        return covers;
    }
}

public class InGameStoryObject
{
    GameObject[] gameObjects;
}

public struct StoryObjectActionParams
{

}