using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{   
    public static EnemyDetector Instance { get; private set; }

    public event EventHandler<ScaleArgs> OnScale;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerAttack playerAttack;
    private List<Vector3> decorList= new List<Vector3>();
    bool onHitBool=false;

    public class ScaleArgs : EventArgs
    {
        public Vector3 scale;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        playerAttack.OnHit += Player_OnHit;
        playerAttack.OnFinishAttack += Player_OnFinishAttack;
    }

    private void Player_OnFinishAttack(object sender, EventArgs e)
    {
        onHitBool = false;
    }

    private void Player_OnHit(object sender, System.EventArgs e)
    {
        onHitBool = true; 
    }
    private void OnTriggerStay(Collider other)
    {
        if (playerAnimator.IsCancelAttack())
        {
            onHitBool=false;
        }
        if (onHitBool&&other.CompareTag("Decor"))
        {
           
            Decor decor = other.transform.parent.GetComponent<Decor>();
            if (decor == null)
            {
                Debug.Log("null");
            }
            if (decor != null)
            {
                decor.ActiveShatter();
                SpawnCoins spawnCoins = other.transform.parent.GetComponent<SpawnCoins>();
                if (spawnCoins != null)
                {
                    spawnCoins.SpawnCoin();
                }
                else
                {
                    Debug.Log("nullSpawnCoins");
                }
            }
            decorList.Add(other.transform.localScale);
            //OnScale?.Invoke(this,new ScaleArgs
            //{
            //    scale = other.transform.localScale,
            //});
            StartCoroutine(DelayDestroy(other.transform.parent.gameObject));
            //Destroy(other.transform.parent);
        }
        if (!onHitBool && decorList.Count > 0)
        {
            Vector3 scale = SumVector(decorList);
            Debug.Log("DecorListCount " + decorList.Count);
            decorList.Clear();
            OnScale?.Invoke(this, new ScaleArgs
            {
                scale = scale,
            });

        }
        if (onHitBool && other.CompareTag("Enemy"))
        {

            Debug.Log("attackEnemy");
            Enemy enemy = other.transform.GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.Log("null");
            }
            if (enemy != null)
            {
                enemy.EnemyDie();
            }
            Debug.Log("Scale "+other.transform.localScale);
            //other.GetComponent<Decor>().ActiveShatter();
            OnScale?.Invoke(this, new ScaleArgs
            {
                scale = other.transform.localScale,
            });
            onHitBool=false;
        }
    }
    private Vector3 SumVector(List<Vector3> listVector)
    {
        Vector3 sum = Vector3.zero;
        foreach (Vector3 t in listVector)
        {
            sum += t;
        }
        return sum;
    }
    IEnumerator DelayDestroy(GameObject gameObject)
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
