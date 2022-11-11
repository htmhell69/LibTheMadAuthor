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
    List<StoryFillIn> storyInputs = new List<StoryFillIn>();
    BookCreationData bookData;
    int sectionIndex = 0;
    int inputIndex = 0;
    public void Start()
    {
        Story story = gameManager.ChooseStory();
        bookData = new BookCreationData(story.StoryObjectCount());
        PlaceCoverObjects(ChooseBooks(story));
        LoadSection(story.GetSection(0));
    }
    public void Forward()
    {
        inputIndex++;
    }
    public void Back()
    {
        inputIndex--;
    }
    CoverObject[] ChooseBooks(Story story)
    {
        return gameManager.GenerateCoverObjects(story.StoryObjectCount());
    }


    void LoadSection(StorySection section)
    {
        inputIndex = 0;
        storyInputs.Clear();
        for (int i = 0; i < section.GetSectionCount(); i++)
        {
            CreateTextSection(section, i);
        }
        CreateTextSection(section, section.GetSectionCount());
    }

    void CreateTextSection(StorySection section, int i)
    {
        textMesh.text += section.GetSection(i);
        textMesh.ForceMeshUpdate(true);
        if (i != section.GetSectionCount() && textMesh.textInfo.lineCount != 0)
        {
            Debug.Log("wassup");
            TMP_CharacterInfo character = textMesh.textInfo.characterInfo[textMesh.textInfo.lineInfo[textMesh.textInfo.lineCount - 1].lastCharacterIndex];
            Vector2 position = textMesh.transform.TransformPoint(character.topRight);
            position.y -= (textMesh.transform.TransformPoint(character.topRight).y - textMesh.transform.TransformPoint(character.bottomRight).y) / 2;

            char nextChar = section.GetText()[character.index + 2 - ((transitionString.Length) * storyInputs.Count)];
            Debug.Log(nextChar);
            if (nextChar == ' ')
            {
                position.x += inputPrefabTransform.sizeDelta.x * xInputPrefabOffset;

            }
            StoryFillIn storyInput = Instantiate(inputPrefab, position, Quaternion.identity).AddComponent<StoryFillIn>();
            storyInput.transform.SetParent(textMesh.transform);
            storyInput.SetReference(section.GetSectionContext(i).ReferenceStoryObject());
            storyInputs.Add(storyInput);
            textMesh.text += transitionString;
        }
    }
    public void SectionUpdate(int index)
    {

    }
    public void ReloadSection()
    {
        for (int i = 0; i < storyInputs.Count; i++)
        {
            storyInputs[i].UpdateCycle(bookData);
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
        storyInputs[inputIndex].SetCoverObject(coverObject, bookData);
        ReloadSection();
    }
}

public struct BookCreationData
{
    public CoverObject[] coverObjects;
    public BookCreationData(int size)
    {
        coverObjects = new CoverObject[size];
    }
}