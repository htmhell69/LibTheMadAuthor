using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new story", menuName = "GameData/Story")]
public class Story : ScriptableObject
{
    [SerializeField] StorySection start;
    [SerializeField] StorySection[] sections;
}
