using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class DistanceTest : MonoBehaviour
{
    private float distance;
    public GameObject cube;
    public Text debugtext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, cube.transform.position);
        debugtext.text = "distance is "+distance;
        Debug.Log(distance);
  
        
    }
}
