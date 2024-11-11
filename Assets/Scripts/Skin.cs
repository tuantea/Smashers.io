using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private  RuntimeAnimatorController animatorController;
    public RuntimeAnimatorController GetController()
    {
        return animatorController;
    }
}
