using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DragCard : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public AutoPlacementOfObjectsInPlane boardmanager;
    public Vector2 initpos;
    private ARRaycastManager arRaycastManager;
    private Vector2 lasttouch;
    public Text debugtext;
    public CardsManager cardsmanager;

    // Start is called before the first frame update
    void Start()
    {
        cardsmanager = FindObjectOfType<CardsManager>();
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        boardmanager = FindObjectOfType<AutoPlacementOfObjectsInPlane>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        initpos = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var hits = new List<ARRaycastHit>();
        if (arRaycastManager.Raycast(eventData.position, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;
            if (Vector3.Distance(hitPose.position, boardmanager.placedObject.transform.position) < 1)
            {
                debugtext.text = "hit board";
            }
            else
            {
                transform.position = initpos;
            }
        }
        else
        {
            transform.position = initpos;
        }
        transform.localPosition = initpos;
        Debug.Log("drag ended");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pointer up");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        Debug.Log("pointer down");
    }
}
