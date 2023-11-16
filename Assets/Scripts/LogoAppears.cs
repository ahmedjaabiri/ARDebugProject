using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAppears : MonoBehaviour
{
    public Vector3 targetPosition;
    public float smoothFactor=1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothFactor);
    }
}
