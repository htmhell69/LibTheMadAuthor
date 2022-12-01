using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] UiHandler ui;
    [SerializeField] Story[] storys;
    [SerializeField] CoverObject[] coverObjects;
    [SerializeField] TMP_Text displayText;
    StoryData storyData;
    List<InGameStoryObject> storyObjects = new List<InGameStoryObject>();
    Story story;
    int currentSection = 0;
    GameObject activeLevel;
    int currentDifficulty;

    public void Start()
    {
        story = storys[Random.Range(0, storys.Length)];
        InputManager.OnKeyPressed += Quit;
    }
    public void StartGame(BookCreationData bookData)
    {
        ui.SwitchMenu(UiHandler.Menu.None);
        storyData = new StoryData(bookData);
        LoadSection(0);
    }
    void Quit(object sender, KeyEvent keyEvent)
    {
        if (keyEvent.keyPressed == KeyCode.Space)
        {
            Application.Quit();
        }
    }

    public Story CurrentStory()
    {
        return story;
    }

    public Story ChooseStory()
    {
        story = storys[Random.Range(0, storys.Length)];
        return story;
    }
    public void UpdateStoryObject(SectionContext context)
    {
        switch (context.GetBaseParams().action)
        {
            case StoryObjectAction.Create:
                CreateStoryObject(context);
                break;
        }
    }

    void CreateStoryObject(SectionContext context)
    {
        SectionContext.BaseParams baseParams = context.GetBaseParams();
        SectionContext.ExtraParams extraParams = context.GetExtraParams();
        List<GameObject> gameObjects = new List<GameObject>();
        for (int i = 0; i < baseParams.amountTargeted; i++)
        {
            StoryObjectInstance storyObject = storyData.GetStoryObject(context.GetBaseParams().storyObjectIndex).GetInstance(extraParams.type);
            Vector3 position = extraParams.positions[i];
            GameObject storyObjectGo = Instantiate(storyObject.gameObject, position, storyObject.gameObject.transform.rotation);
            if (extraParams.type == StoryObject.Types.Player)
            {
                Camera.main.transform.SetParent(storyObjectGo.transform, false);
            }
            if (storyObjectGo.GetComponent<Entity>() != null)
            {
                Entity entity = storyObjectGo.GetComponent<Entity>();
                entity.Init(this);
                if (baseParams.modifier > 1 || currentDifficulty != 1)
                {
                    int level = baseParams.modifier;
                    if (extraParams.type != StoryObject.Types.Player)
                    {
                        level *= currentDifficulty;
                    }
                    entity.AdjustStats(level - 1);
                }
            }
            if (storyObjectGo.GetComponent<DirectionEnemy>())
            {
                storyObjectGo.GetComponent<DirectionEnemy>().SetMaxPos(extraParams.maxPosX[i], extraParams.maxPosY[i]);
            }
            gameObjects.Add(storyObjectGo);
        }

        storyObjects.Add(new InGameStoryObject(gameObjects, baseParams.storyObjectIndex, extraParams.type));
    }

    public CoverObject[] GenerateCoverObjects(int amount)
    {
        CoverObject[] covers = new CoverObject[amount];
        for (int i = 0; i < amount; i++)
        {
            covers[i] = coverObjects[Random.Range(0, coverObjects.Length)];
        }
        return covers;
    }
    public void LoadSection(int index)
    {
        if (activeLevel != null)
        {
            Destroy(activeLevel);
        }
        currentSection = index;
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            Destroy(enemys[i]);
        }
        displayText.gameObject.SetActive(true);
        StorySection section = story.GetSection(index);
        displayText.text = section.GetFullText(storyData);
        activeLevel = section.LoadLevel(this);
    }

    InGameStoryObject GetStoryObject(int index)
    {
        for (int i = 0; i < storyObjects.Count; i++)
        {
            if (storyObjects[i].index == index)
            {
                return storyObjects[i];
            }
        }
        return null;
    }

    public void PlaceStoryObject(int index, Vector3[] positions)
    {
        InGameStoryObject storyObject = GetStoryObject(index);
        if (storyObject != null)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (storyObject.gameObjects[i] != null)
                {
                    storyObject.gameObjects[i].transform.position = positions[i];
                }
            }
        }
    }
    public void LoadSection()
    {
        LoadSection(currentSection + 1);
    }
    public Slider GetStatusBar(int index)
    {
        return ui.GetStatusBar(index);
    }
    public UiHandler GetUiHandler()
    {
        return ui;
    }

    public void SetDifficulty(int difficulty)
    {
        currentDifficulty = difficulty;
    }

    public bool LastSection()
    {
        if (currentSection == story.SectionLength() - 1)
        {
            return true;
        }
        return false;
    }
}

public class InGameStoryObject
{
    public StoryObject.Types type;
    public int index;
    public List<GameObject> gameObjects;
    public InGameStoryObject(List<GameObject> gameObjects, int index, StoryObject.Types type)
    {
        this.index = index;
        this.gameObjects = gameObjects;
        this.type = type;
    }
}
