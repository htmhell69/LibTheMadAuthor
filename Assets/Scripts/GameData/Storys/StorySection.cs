using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StorySection
{
    [SerializeField] string text;
    [SerializeField] SectionContext[] contexts;
    public string GetText()
    {
        return text;
    }
    public string AddContext(StoryData storyData)
    {
        int stringIndex;
        string addition;
        for (int i = 0; i < contexts.Length; i++)
        {
            addition = contexts[i].GetContext(storyData);
            stringIndex = contexts[i].GetStringIndex(text);
            text = text.Substring(0, stringIndex) + addition + text.Substring(stringIndex + 1);
        }
        return text;
    }
}

[System.Serializable]
public class SectionContext
{
    [SerializeField] char replacementCharacter;
    [SerializeField] int storyObjectIndex = -1;
    [SerializeField] StoryObjectAction action;
    [SerializeField] int modifier;
    [SerializeField] int amountTargeted;
    [Header("Extra params for object creation.")]
    [SerializeField] CreationParams creationParams;



    public string GetContext(StoryData storyData)
    {
        return storyData.GetStoryObject(storyObjectIndex).GetName(); ;
    }

    public int GetStringIndex(string text)
    {
        return text.IndexOf(replacementCharacter);
    }
}

public enum StoryObjectAction
{
    Create,
    Destroy,
    Damage,
    LevelUp,
    Heal,
    Nothing
}
[System.Serializable]
public struct CreationParams
{
    [SerializeField] StoryObject.Types type;
    [SerializeField] int count;
    [SerializeField] Vector2[] position;
}