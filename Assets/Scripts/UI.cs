using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject screenObject;
    [SerializeField] private GameObject tapToStart;
    [SerializeField] private Canvas joyStick;
    void Start()
    {
       // TapToStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TapToStart()
    {
        tapToStart.SetActive(false);
        screenObject.SetActive(false);
        joyStick.gameObject.SetActive(true);
    }
    public void Test()
    {
        Debug.Log("Test");
    }
}
