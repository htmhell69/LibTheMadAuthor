using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BookCreation : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform coverObjectTransform;
    [SerializeField] GameObject coverObjectPrefab;
    [SerializeField] GameObject inputPrefab;
    [SerializeField] RectTransform inputPrefabTransform;
    [SerializeField] TMP_Text textMesh;
    [SerializeField] string transitionString;
    [SerializeField] float xInputPrefabOffset;
    GameObject[] inputs;
    StoryData storyData;
    int sectionIndex;
    int inputIndex;
    public void Start()
    {
        Story story = gameManager.ChooseStory();
        PlaceCoverObjects(ChooseBooks(story));
        LoadSection(story.GetSection(0));
    }
    public void Forward()
    {
        Debug.Log("forward i think");
    }
    CoverObject[] ChooseBooks(Story story)
    {
        return gameManager.GenerateCoverObjects(story.StoryObjectCount());
    }
    public void Back()
    {
        Debug.Log("back i think");
    }

    void LoadSection(StorySection section)
    {
        inputIndex = 0;
        for (int i = 0; i < section.GetSectionCount(); i++)
        {
            CreateTextSextion(section, i);
        }
        CreateTextSextion(section, section.GetSectionCount());
    }

    void CreateTextSextion(StorySection section, int i)
    {
        textMesh.text += section.GetSection(i) + transitionString;
        textMesh.ForceMeshUpdate(true);
        if (i != section.GetSectionCount() && textMesh.textInfo.lineCount != 0)
        {
            TMP_CharacterInfo character = textMesh.textInfo.characterInfo[textMesh.textInfo.lineInfo[textMesh.textInfo.lineCount - 1].lastVisibleCharacterIndex];
            Vector2 position = textMesh.transform.TransformPoint(character.topRight);
            position.y -= (textMesh.transform.TransformPoint(character.topRight).y - textMesh.transform.TransformPoint(character.bottomRight).y) / 2;
            position.x += inputPrefabTransform.sizeDelta.x * xInputPrefabOffset;
            Instantiate(inputPrefab, position, Quaternion.identity).transform.SetParent(textMesh.transform);
        }
    }
    public void PlaceCoverObjects(CoverObject[] coverObjects)
    {
        for (int i = 0; i < coverObjects.Length; i++)
        {
            GameObject coverObject = Instantiate(coverObjectPrefab);
            BookCover bookCover = coverObject.GetComponent<BookCover>();
            bookCover.CreateBook(coverObjects[i], this);
            coverObject.transform.SetParent(coverObjectTransform);
            bookCover.transform.localScale = coverObjectPrefab.transform.localScale;
        }
    }

    public void AssignCoverObject(CoverObject coverObject)
    {

    }
}
