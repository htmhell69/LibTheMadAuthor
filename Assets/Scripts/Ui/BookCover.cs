using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BookCover : MonoBehaviour
{
    [SerializeField] RawImage bindingImage;
    [SerializeField] RawImage coverImage;
    [SerializeField] Button button;
    public void CreateBook(CoverObject coverObject, BookCreation bookCreation)
    {
        coverObject.SetColor(bindingImage);
        coverObject.SetCoverImage(coverImage);
        button.onClick.AddListener(delegate { bookCreation.AssignCoverObject(coverObject); });
    }
}
