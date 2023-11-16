using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArCard : MonoBehaviour, IPointerDownHandler
{
    public CardsManager cardsmanager;

    public void OnPointerDown(PointerEventData eventData)
    {
        cardsmanager.highlightcard(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        cardsmanager = FindObjectOfType<CardsManager>();
    }


}
