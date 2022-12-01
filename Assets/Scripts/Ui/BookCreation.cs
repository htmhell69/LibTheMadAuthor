using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BookCreation : MonoBehaviour
{
    [SerializeField] UiHandler ui;
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform coverObjectTransform;
    [SerializeField] GameObject coverObjectPrefab;
    [SerializeField] GameObject inputPrefab;
    [SerializeField] RectTransform inputPrefabTransform;
    [SerializeField] Color32 inputSelectionColor;
    [SerializeField] TMP_Text textMesh;
    [SerializeField] string transitionString;
    [SerializeField] float baseInputDistance;

    List<StoryFillIn> storyInputs = new List<StoryFillIn>();
    BookCreationData bookData;
    int sectionIndex = 0;
    int inputIndex = 0;
    Story story;
    public void Start()
    {
        story = gameManager.ChooseStory();
        bookData = new BookCreationData(story.StoryObjectCount());
        PlaceCoverObjects(ChooseBooks(story));
        LoadSection(story.GetSection(0), 0);
    }
    public void Forward()
    {
        if (inputIndex + 1 < storyInputs.Count)
        {
            storyInputs[inputIndex].UnSelect();
            inputIndex++;
            storyInputs[inputIndex].Select(inputSelectionColor);
        }
        else if (sectionIndex + 1 < story.SectionLength())
        {
            sectionIndex++;
            LoadSection(story.GetSection(sectionIndex), 0);
        }
        else
        {
            ui.SwitchMenu(UiHandler.Menu.StartGame);
        }
    }
    public void Back()
    {
        if (inputIndex - 1 >= 0)
        {
            storyInputs[inputIndex].UnSelect();
            inputIndex--;
            storyInputs[inputIndex].Select(inputSelectionColor);
        }
        else if (sectionIndex - 1 >= 0)
        {
            sectionIndex--;
            StorySection section = story.GetSection(sectionIndex);
            LoadSection(section, 0);
        }
    }
    CoverObject[] ChooseBooks(Story story)
    {
        return gameManager.GenerateCoverObjects(story.StoryObjectCount());
    }


    void LoadSection(StorySection section, int inputIndexValue)
    {
        textMesh.text = "";
        inputIndex = inputIndexValue;
        for (int i = 0; i < storyInputs.Count; i++)
        {
            Destroy(storyInputs[i].gameObject);
        }
        storyInputs.Clear();
        List<TMP_CharacterInfo> characterInfos = new List<TMP_CharacterInfo>();
        for (int i = 0; i < section.GetSectionCount(); i++)
        {
            characterInfos.AddRange(CreateTextSection(section, i));
        }
        characterInfos.AddRange(CreateTextSection(section, section.GetSectionCount()));
        for (int i = 0; i < characterInfos.Count / 2; i++)
        {
            CreateInputButton(characterInfos, i, section);
        }
        if (storyInputs.Count > 0)
        {
            storyInputs[inputIndexValue].Select(inputSelectionColor);
        }
        ReloadSection();
    }

    void CreateInputButton(List<TMP_CharacterInfo> characterInfo, int index, StorySection section)
    {
        TMP_CharacterInfo leftChar = characterInfo[index * 2];
        TMP_CharacterInfo rightChar = characterInfo[index * 2 + 1];
        float xOffset = 0;
        Vector3 leftCharBottomRight = textMesh.transform.TransformPoint(leftChar.bottomRight);
        Vector3 leftCharTopRight = textMesh.transform.TransformPoint(leftChar.topRight);
        Vector3 rightCharBottomLeft = textMesh.transform.TransformPoint(rightChar.bottomLeft);
        Vector3 rightCharTopLeft = textMesh.transform.TransformPoint(rightChar.topLeft);

        float leftCharYOffset = (leftCharTopRight.y - leftCharBottomRight.y) / 2;
        float rightCharYOffset = (rightCharTopLeft.y - rightCharBottomLeft.y) / 2;
        float yOffset = (leftCharYOffset + rightCharYOffset) / 2;
        if (leftChar.lineNumber == rightChar.lineNumber)
        {
            xOffset = (rightCharBottomLeft.x - leftCharBottomRight.x) / 2;
        }
        else
        {
            xOffset = (textMesh.transform.TransformPoint(leftChar.bottomRight).x - textMesh.transform.TransformPoint(leftChar.bottomLeft).x) * 2;
        }
        StoryFillIn storyInput = Instantiate(inputPrefab, Vector2.zero, Quaternion.identity).AddComponent<StoryFillIn>();
        storyInput.transform.SetParent(textMesh.transform, false);
        storyInput.transform.position = new Vector2(leftCharBottomRight.x + xOffset, leftCharBottomRight.y + yOffset);
        storyInput.SetReference(section.GetSectionContext(index).ReferenceStoryObject());
        storyInputs.Add(storyInput);
    }

    List<TMP_CharacterInfo> CreateTextSection(StorySection section, int i)
    {
        string sectionText = section.GetSection(i);
        int firstVisibleCharacter = textMesh.text.Length + section.GetFirstNonSpace(sectionText);
        textMesh.text += sectionText + transitionString;
        textMesh.ForceMeshUpdate(true);
        List<TMP_CharacterInfo> charInfo = new List<TMP_CharacterInfo>();
        if (i == section.GetSectionCount())
        {
            TMP_CharacterInfo character = textMesh.textInfo.characterInfo[firstVisibleCharacter];
            charInfo.Add(character);
        }
        else if (i == 0 && textMesh.textInfo.lineCount != 0)
        {
            TMP_CharacterInfo character = textMesh.textInfo.characterInfo[textMesh.textInfo.lineInfo[textMesh.textInfo.lineCount - 1].lastVisibleCharacterIndex];
            charInfo.Add(character);
        }
        else
        {
            TMP_CharacterInfo firstCharacter = textMesh.textInfo.characterInfo[firstVisibleCharacter];
            TMP_CharacterInfo lastCharacter = textMesh.textInfo.characterInfo[textMesh.textInfo.lineInfo[textMesh.textInfo.lineCount - 1].lastVisibleCharacterIndex];
            charInfo.Add(firstCharacter);
            charInfo.Add(lastCharacter);
        }
        return charInfo;
    }
    public void ReloadSection()
    {
        for (int i = 0; i < storyInputs.Count; i++)
        {
            storyInputs[i].UpdateCycle(this);
        }
    }
    public void PlaceCoverObjects(CoverObject[] coverObjects)
    {
        for (int i = 0; i < coverObjects.Length; i++)
        {
            CreateCoverObject(coverObjects[i]);
        }
    }
    public void CreateCoverObject(CoverObject coverObject)
    {
        GameObject coverObjectGo = Instantiate(coverObjectPrefab);
        BookCover bookCover = coverObjectGo.GetComponent<BookCover>();
        bookCover.CreateBook(coverObject, this);
        coverObjectGo.transform.SetParent(coverObjectTransform);
        bookCover.transform.localScale = coverObjectPrefab.transform.localScale;
    }

    public void AssignCoverObject(CoverObject coverObject)
    {
        storyInputs[inputIndex].SetCoverObject(coverObject, this, false);
        ReloadSection();
    }

    public BookCreationData BookData()
    {
        return bookData;
    }
    public void StartGame()
    {
        if (ValidateBookData())
        {
            gameManager.StartGame(bookData);
        }
        else
        {
            ui.DisplayText("Please fill in all the fill in the blanks", 3, new Color32(255, 0, 0, 255), 50);
        }
    }
    public bool ValidateBookData()
    {
        for (int i = 0; i < bookData.coverObjects.Length; i++)
        {
            if (bookData.coverObjects[i] == null)
            {
                return false;
            }
        }
        return true;
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