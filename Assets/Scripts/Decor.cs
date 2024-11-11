using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decor : MonoBehaviour
{
    [SerializeField] private GameObject shatterObject;
    [SerializeField] private GameObject childObject;

    void Update()
    {
        
    }
    public void ActiveShatter()
    {
        shatterObject.SetActive(true);
        childObject.SetActive(false);
        CameraController.Instance.ImpulseCamera();
    }
}
