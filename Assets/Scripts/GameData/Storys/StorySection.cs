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

    public string GetSection(int index)
    {
        int startingIndex = 0;
        if (index != 0)
        {
            startingIndex = contexts[index - 1].GetStringIndex(text) + 1;
        }
        int endIndex = text.Length;
        if (index != contexts.Length)
        {
            endIndex = contexts[index].GetStringIndex(text) - 1;
        }

        if (endIndex < 0)
        {
            return "";
        }
        return text.Substring(startingIndex, endIndex - startingIndex);
    }
    public int GetSectionCount()
    {
        return contexts.Length;
    }
}

[System.Serializable]
public class SectionContext
{
    [System.Serializable]
    public class BaseParams
    {
        public char replacementCharacter;
        public int storyObjectIndex = -1;
        public StoryObjectAction action;
        public int modifier;
        public int amountTargeted;
    }
    [System.Serializable]
    public struct ExtraParams
    {
        public StoryObject.Types type;
        public Vector2[] positions;
    }
    [SerializeField] BaseParams baseParams;
    [SerializeField] ExtraParams extraParams;

    public string GetContext(StoryData storyData)
    {
        return storyData.GetStoryObject(baseParams.storyObjectIndex).GetName(); ;
    }
    public BaseParams GetBaseParams()
    {
        return baseParams;
    }
    public ExtraParams GetExtraParams()
    {
        return extraParams;
    }

    public int GetStringIndex(string text)
    {
        return text.IndexOf(baseParams.replacementCharacter);
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

