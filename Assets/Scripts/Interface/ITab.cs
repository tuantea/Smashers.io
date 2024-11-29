using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITab 
{
    void Active() { }
    void UnActive() { }

    int GetIndexTab();
}
