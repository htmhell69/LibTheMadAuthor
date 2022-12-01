using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoryFillIn : MonoBehaviour
{
    CoverObject coverObject;
    int referenceCoverObject;
    public bool Taken()
    {
        return coverObject != null;
    }
    public void SetCoverObject(CoverObject coverObject, BookCreation bookCreation, bool update)
    {
        if (this.coverObject != null && !update)
        {
            bookCreation.CreateCoverObject(this.coverObject);
        }
        this.coverObject = coverObject;

        bookCreation.BookData().coverObjects[referenceCoverObject] = coverObject;
        coverObject.SetCoverImage(GetComponent<RawImage>());
    }
    public void SetReference(int referenceStoryObject)
    {
        this.referenceCoverObject = referenceStoryObject;
    }
    public void UpdateCycle(BookCreation bookCreation)
    {
        if (bookCreation.BookData().coverObjects[referenceCoverObject] != coverObject)
        {
            SetCoverObject(bookCreation.BookData().coverObjects[referenceCoverObject], bookCreation, true);
        }
    }

    public void Select(Color32 highlightColor)
    {
        GetComponent<Outline>().effectColor = highlightColor;
    }

    public void UnSelect()
    {
        GetComponent<Outline>().effectColor = new Color32(0, 0, 0, 255);
    }
}
