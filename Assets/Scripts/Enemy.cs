using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform visualTransform;

    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform hammer;

    [SerializeField] private GameObject CharacterExplosion;

    public event EventHandler<EventArgs> OnDie;

    bool isDestroying = false;
    void Start()
    {
        hammer.transform.SetParent(rightHand, true);
        hammer.transform.localRotation = Quaternion.Euler(0, 0, 90);
        //EnemyDetector.Instance.OnScale += Player_OnScale;
    }

    private void Player_OnScale(object sender, EnemyDetector.ScaleArgs e)
    {
        transform.DOScale(transform.localScale + e.scale, 1f);
    }

    public void EnemyDie()
    {
        if (gameObject != null)
        {
            PlayerAnimator enemyAnimator = GetComponent<PlayerAnimator>();
            enemyAnimator.PlayerDie();
            isDestroying = true;
            CharacterExplosion.SetActive(true);
            visualTransform.localScale = new Vector3(1, 0.2f, 1);
            StartCoroutine(ScalePlayer());
        }   
    }
    public bool IsDestroying()
    {
        return isDestroying;
    }
    IEnumerator ScalePlayer()
    {
        yield return new WaitForSeconds(0.5f);
        visualTransform.DOScale(Vector3.zero, 2f);
        OnDie?.Invoke(this,EventArgs.Empty);
        Destroy(gameObject);

    }
}
