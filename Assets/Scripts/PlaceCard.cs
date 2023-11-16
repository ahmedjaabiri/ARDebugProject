using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCard : MonoBehaviour
{
    public GameObject CardOnBoardPrefab;
    public AutoPlacementOfObjectsInPlane boardmanager;


    public GameObject[] slots;
    public GameObject placedcard;
    // Start is called before the first frame update

    public void putcardonboard()
    {
        foreach (GameObject slot in slots)
        {
            if (!slot.CompareTag("Taken"))
            {
                //Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
                placedcard = Instantiate(CardOnBoardPrefab, slot.transform.position, slot.transform.rotation,boardmanager.placedObject.transform);
                slot.tag = "Taken";
                break;
            }
        }
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {   
        //board in scene
        if (boardmanager.placedObject)
        {
            for (int i = 0; i < 2; i++)
            {
                slots[i] = boardmanager.placedObject.transform.GetChild(i).gameObject;
            }
        }
    }



}
