using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StorySection
{
    [SerializeField] string text;
    [SerializeField] GameObject level;
    [SerializeField] StoryObjectPositions[] positions;
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
            endIndex = contexts[index].GetStringIndex(text);
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

    public SectionContext GetSectionContext(int index)
    {
        return contexts[index];
    }

    public int GetFirstNonSpace(string section)
    {
        for (int i = 0; i < section.Length; i++)
        {
            if (section[i] != ' ')
            {
                return i;
            }
        }
        return -1;
    }

    public string GetFullText(StoryData storyData)
    {
        string[] stringContexts = new string[contexts.Length];
        string finalString = "";
        for (int i = 0; i < contexts.Length; i++)
        {
            stringContexts[i] = contexts[i].GetContext(storyData);
        }
        for (int i = 0; i < contexts.Length; i++)
        {
            finalString += GetSection(i) + stringContexts[i];
        }
        finalString += GetSection(contexts.Length);
        return finalString;
    }

    public GameObject LoadLevel(GameManager gameManager)
    {
        PlaceStoryObjects(gameManager);
        GameObject levelGo = MonoBehaviour.Instantiate(level, Vector3.zero, Quaternion.identity);
        for (int i = 0; i < contexts.Length; i++)
        {
            gameManager.UpdateStoryObject(contexts[i]);
        }
        levelGo.GetComponent<Level>().Init(gameManager);
        return levelGo;
    }

    void PlaceStoryObjects(GameManager gameManager)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            gameManager.PlaceStoryObject(positions[i].storyObjectIndex, positions[i].positions);
        }
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
        [Header("max pos x is for enemys that move in the x direction this indicates the maximum position they can move to same for maxPos y")]
        public Vector3[] maxPosX;
        public Vector3[] maxPosY;
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
    public int ReferenceStoryObject()
    {
        return baseParams.storyObjectIndex;
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
public struct StoryObjectPositions
{
    public int storyObjectIndex;
    public Vector3[] positions;
}
