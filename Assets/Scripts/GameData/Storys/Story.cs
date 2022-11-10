using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new story", menuName = "GameData/Story")]
public class Story : ScriptableObject
{
    [SerializeField] StorySection[] sections;
    [SerializeField] int storyObjectCount;
    public int sectionLength()
    {
        return sections.Length;
    }
    public StorySection GetSection(int index)
    {
        return sections[index];
    }
    public int StoryObjectCount()
    {
        return storyObjectCount;
    }
}
