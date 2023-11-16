using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Ar.Quest
{
    public class Hannibal : MonoBehaviour
    {

        public GameObject panel;

        private void Start()
        {
            GameObject parentGobject = FindObjectOfType<Canvas>().gameObject;
            panel = parentGobject.transform.GetChild(0).gameObject;

        }

        private void OnMouseDown()
        {
            panel.SetActive(true);
        }
        private void OnDestroy()
        {
            panel.SetActive(false);
        }
    }
}

