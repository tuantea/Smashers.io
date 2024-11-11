using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayTime());
    }
    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(1f);
        transform.DOScale(Vector3.zero,1f).
            OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
