using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new StoryObject", menuName = "GameData/StoryObject")]
public class StoryObject : ScriptableObject
{
    public enum Types
    {
        Player,
        Item,
        Enemy,
        Boss,
        Decoration
    }
    [SerializeField] StoryObjectInstance[] instances;
    [SerializeField] string objectName;
    public StoryObjectInstance GetInstance(Types type)
    {
        for (int i = 0; i < instances.Length; i++)
        {
            if (instances[i].type == type)
            {
                return instances[i];
            }
        }
        Debug.LogError("StoryObject instance not implimented");
        return null;
    }

    public string GetName()
    {
        return objectName;
    }
}
