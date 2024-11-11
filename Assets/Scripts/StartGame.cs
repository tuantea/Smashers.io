using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private bool isStartGame;

    void Start()
    {
        
    }

    private void SetIsStartGame()
    {
        isStartGame = true;
    }
    public bool IsStartGame()
    {
        return isStartGame;
    }
    void Update()
    {
        
    }
}
