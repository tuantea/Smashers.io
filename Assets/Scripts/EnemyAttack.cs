using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private PlayerAnimator enemyAnimator;
    public EnemyMove enemyMove;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Debug.Log("NameOther "+other.name);
            
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                enemyAnimator.PlayerAttack();
                enemyMove.ResetPath();
                StartCoroutine(DelayPlayerDie(player));
            }
        }
    }
    IEnumerator DelayPlayerDie(Player player)
    {
        yield return new WaitForSeconds(0.4f);
        player.PlayerDie();
    }
}