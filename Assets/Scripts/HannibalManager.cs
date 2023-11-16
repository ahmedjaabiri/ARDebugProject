using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class HannibalManager : MonoBehaviour
{
    private ARAnchorManager anchorManager;
    private ARRaycastManager arRaycastManager;
    private bool placementPoseIsValid = false;
    private Pose placementPose;
    [SerializeField]
    private GameObject hannibalPrefab;
    private GameObject hannibal;

    private ARPlaneManager arPlaneManager;
    [SerializeField]
    private Text debug;
    private int planeUpdatedCount = 0;
    //plane hit with raycast
    private ARPlane arPlaneHit;

    // Start is called before the first frame update
    void Start()
    {
        anchorManager = GetComponent<ARAnchorManager>();
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }
    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (args.updated != null)
        {
            planeUpdatedCount += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hannibal != null)
        {
            if(hannibal.GetComponent<ARAnchor>().trackingState == TrackingState.Tracking)
            {
                if(Vector3.Distance(Camera.main.transform.position, hannibal.transform.position)>7)
                {
                    debug.text = "Get Back here Please";
                    if(Vector3.Distance(Camera.main.transform.position, hannibal.transform.position) > 10)
                    {                  
                        Application.Quit();
                    }
                }
                else
                {
                    debug.text = "click on me";
                }
            }          
        }
        else
        {
            UpdatePlacementPose();
            if (placementPoseIsValid && (arPlaneHit.alignment == PlaneAlignment.HorizontalDown || arPlaneHit.alignment == PlaneAlignment.HorizontalUp))
            {
                if (planeUpdatedCount > 30)
                {
                    placehannibal();
                }
                else
                {
                    debug.text = "initialization";
                }
            }
            else
            {
                debug.text = "please face a ground";
            }
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits,TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            TrackableId planeHit_ID = hits[0].trackableId;
            arPlaneHit = arPlaneManager.GetPlane(planeHit_ID);    
            placementPose = hits[0].pose;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void placehannibal()
    {
        hannibal = Instantiate(hannibalPrefab, placementPose.position, placementPose.rotation);
        hannibal.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        hannibal.transform.Rotate(Vector3.up, 180);
        hannibal.AddComponent<ARAnchor>();
        arPlaneManager.requestedDetectionMode = PlaneDetectionMode.Vertical;
        debug.text = "";
    }

    public void resethannibal()
    {
        arPlaneManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;
        Destroy(hannibal);
        planeUpdatedCount = 0;
    }
}