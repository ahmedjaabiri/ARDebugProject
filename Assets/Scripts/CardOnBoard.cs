using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardOnBoard : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMouseDrag()
    { 
        removecard();
    }
    public void removecard()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
