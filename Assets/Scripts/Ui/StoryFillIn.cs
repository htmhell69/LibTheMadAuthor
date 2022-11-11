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
    public void SetCoverObject(CoverObject coverObject, BookCreationData bookData)
    {
        this.coverObject = coverObject;
        bookData.coverObjects[referenceCoverObject] = coverObject;
        coverObject.SetCoverImage(GetComponent<RawImage>());
    }
    public void SetReference(int referenceStoryObject)
    {
        this.referenceCoverObject = referenceStoryObject;
    }
    public void UpdateCycle(BookCreationData bookData)
    {
        if (bookData.coverObjects[referenceCoverObject] != coverObject)
        {
            SetCoverObject(bookData.coverObjects[referenceCoverObject], bookData);
        }
    }

}
