using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject highlightedcard;
    //last highlighted card
    private GameObject cardhighlighted;
    public void highlightcard(GameObject card)
    {
        if (!highlightedcard.activeSelf)
        {
            highlightedcard.transform.position = card.transform.position;
            highlightedcard.transform.localRotation = card.transform.localRotation;
            cardhighlighted = card;
            card.SetActive(false);
            highlightedcard.SetActive(true);
        }
        else
        {
            highlightedcard.transform.position = card.transform.position;
            highlightedcard.transform.localRotation = card.transform.localRotation;           
            //set last highlightedcard active
            cardhighlighted.SetActive(true);
            //new last card highlighted
            cardhighlighted = card;
            card.SetActive(false);
        }


    }
    public void unhighlightcard()
    {
        cardhighlighted.SetActive(true);
        highlightedcard.SetActive(false);

    }
   
    public void placehighcard()
    {
        if (highlightedcard.activeSelf)
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
     
    }
}
