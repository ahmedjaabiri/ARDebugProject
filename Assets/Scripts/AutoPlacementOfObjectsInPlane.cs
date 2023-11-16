using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class AutoPlacementOfObjectsInPlane : MonoBehaviour
{
    private ARRaycastManager arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;


    [SerializeField]
    private GameObject placedPrefab;

    public GameObject placedObject;
    public Text debugT;
    public GameObject firstcard;
    public GameObject secondcard;



    [SerializeField]
    private ARPlaneManager arPlaneManager;


    void Awake()
    {
        arOrigin = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
        //arPlaneManager.planesChanged += PlaneChanged;
    }

    //private void PlaneChanged(ARPlanesChangedEventArgs args)
    //{
    //    if (args.added != null && placedObject == null)
    //    {
    //        ARPlane arPlane = args.added[0];
    //        placedObject = Instantiate(placedPrefab, arPlane.center, Quaternion.identity);
    //        placedObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    //        StartCoroutine(showcards());
    //    }
    //}
    void placeboard()
    {
        placedObject = Instantiate(placedPrefab, placementPose.position, placementPose.rotation);
        placedObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        DisablePlaneDetection();
        StartCoroutine(showcards());
    }
    void DisablePlaneDetection()
    {
        arPlaneManager.enabled = false;

        //foreach (ARPlane plane in arPlaneManager.trackables)
        //{
        //    plane.gameObject.SetActive(arPlaneManager.enabled);
        //}
    }
    IEnumerator showcards()
    {
        yield return new WaitForSeconds(2);
        firstcard.SetActive(true);
        secondcard.SetActive(true);
        debugT.text = "please select card";
        

    }

    public void resetboard()
    {
        foreach (Transform child in placedObject.transform)
        {
            child.gameObject.tag = "Untagged";
        }
        Destroy(placedObject);
        firstcard.SetActive(false);
        secondcard.SetActive(false);
        arPlaneManager.enabled = true;
    }

    private void Update()
    {
        if (placedObject != null)
            return;
        UpdatePlacementPose();
        if (placementPoseIsValid )
        {
            placeboard();
        }
        else
        {
            debugT.text = "face a plane then move phone slightly backwards";
        }

    }
    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }

    }
}
