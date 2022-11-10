using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "new CoverObject", menuName = "GameData/CoverObject")]
public class CoverObject : ScriptableObject
{
    [SerializeField] Texture cover;
    [SerializeField] StoryObject[] storyObjects;
    [SerializeField] Color32 bookColor;
    public StoryObject GetStoryObject()
    {
        return storyObjects[Random.Range(0, storyObjects.Length)];
    }
    public void SetColor(RawImage image)
    {
        image.color = bookColor;
    }

    public void SetCoverImage(RawImage image)
    {
        image.texture = cover;
    }
}
