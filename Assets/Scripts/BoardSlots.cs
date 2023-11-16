using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlots : MonoBehaviour
{
    //public AutoPlacementOfObjectsInPlane boardmanager;
    public CardsManager cardsManager;
    public GameObject cardonboardPrefab;
    private GameObject cardonboard;
    // Start is called before the first frame update
    void Start()
    {
        //boardmanager = FindObjectOfType<AutoPlacementOfObjectsInPlane>();
        cardsManager = FindObjectOfType<CardsManager>();
    }
    private void OnMouseDown()
    {
        if(cardonboard)
        {
        }
        else
        {
            placehighcard();
        }
        
    }
    public void placehighcard()
    {
        if(cardsManager.highlightedcard.activeSelf )
        {
            cardsManager.highlightedcard.SetActive(false);
            cardonboard=Instantiate(cardonboardPrefab, transform.position,transform.rotation,transform.parent);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
