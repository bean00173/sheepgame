using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeColourOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private MeshRenderer model;

    public Color normalColor;
    public Color hoverColor;

    private void Start()
    {
        model = GetComponentInChildren<MeshRenderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        model.material.color = hoverColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        model.material.color = normalColor;
    }
}

