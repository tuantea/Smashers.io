using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject screenObject;
    [SerializeField] private GameObject tapToStart;

    [SerializeField] private GameObject Skin;
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
    }
    public void Test()
    {
        Debug.Log("Test");
    }
    public void SkinButton()
    {
        Skin.SetActive(true);
    }
}
