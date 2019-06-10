using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    public Collider currentTile;

    CanvasGroup canvasGroup;

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        currentTile = null;
    }

    public void Show(Collider col)
    {
        currentTile = col;

        Vector3 tilePosOnScreen = Camera.main.WorldToScreenPoint(col.transform.position);
        float lowerHeightOverlap = tilePosOnScreen.y - this.GetComponent<RectTransform>().rect.height / 2;
        float upperHeightOverlap = tilePosOnScreen.y + this.GetComponent<RectTransform>().rect.height / 2;
        if (lowerHeightOverlap < 0)
            tilePosOnScreen.y -= lowerHeightOverlap;

        if (upperHeightOverlap > Camera.main.pixelHeight)
            tilePosOnScreen.y -= upperHeightOverlap - Camera.main.pixelHeight;

        this.GetComponent<RectTransform>().position = tilePosOnScreen;

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void HideTile()
    {

    }
}