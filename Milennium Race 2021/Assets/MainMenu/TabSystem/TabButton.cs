using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public GameObject Tab;
    public Image bg;
    public Color Idle;
    public Color Hover;
    public Color Selected;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this); 
    }

    // Start is called before the first frame update
    void Start()
    {
        bg = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
