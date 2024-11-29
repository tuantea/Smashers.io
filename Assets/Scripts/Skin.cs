using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private  RuntimeAnimatorController animatorController;
    public enum TypeSkin
    {
        Hero_A,
        Hero_B,
        AmongUs
    }
    public TypeSkin typeSkin;
    public RuntimeAnimatorController GetController()
    {
        return animatorController;
    }
    public bool IsHeroA()
    {
        return typeSkin == TypeSkin.Hero_A;
    }
}
