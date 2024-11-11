using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private GameObject coins;
    [SerializeField] private Transform target;
    [SerializeField] private Transform transformParent;

    public void SpawnCoin()
    {
        GameObject gameObject;
        for (int i = 0; i < amount; i++)
        {
            gameObject = Instantiate(coins, target.position, Quaternion.identity, transformParent);
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = new Vector3(0f, 1f, 0f).normalized;
                rb.AddForce(randomDirection * 15f, ForceMode.Impulse);
            }
        }
    }
}
