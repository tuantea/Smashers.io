using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter 
{

    public void Die()
    {
    }
    IEnumerator ScaleCharacter()
    {
        yield return new WaitForSeconds(0.5f);

    }
}
