using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class User_Interface_WindowDraging : MonoBehaviour, IDragHandler , IBeginDragHandler , IEndDragHandler, IPointerDownHandler
{

    [SerializeField] private RectTransform rectTransform = null;
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private Image backgroundImage = null;
    private Color backgroundColor;

    private void Awake()
    {
        backgroundColor = backgroundImage.color;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        backgroundColor.a = 0.4f;
        backgroundImage.color = backgroundColor;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        backgroundColor.a = 1f;
        backgroundImage.color = backgroundColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.SetAsLastSibling();
    }
}
