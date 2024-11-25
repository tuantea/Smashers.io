using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private PlayerAnimator enemyAnimator;
    [SerializeField] private Enemy parent;
    public EnemyMove enemyMove;
    public event EventHandler<EventArgs> OnAttack;
    public event EventHandler<EventArgs> FinishOnAttack;

    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerAttack playerAttack;
    private List<Vector3> decorList;
    bool onHitBool = false;
    bool Test=false;
    bool isStart;

    private void Start()
    {
        playerAttack.OnHit += PlayerAttack_OnHit;
        playerAttack.OnFinishAttack += PlayerAttack_OnFinishAttack;
        isStart = StartGame.Instance.IsStartGame();
    }

    private void PlayerAttack_OnFinishAttack(object sender, EventArgs e)
    {
        onHitBool = false;
        FinishOnAttack?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerAttack_OnHit(object sender, EventArgs e)
    {
        onHitBool = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (StartGame.Instance.IsStartGame())
        {
            if (!Test && other.CompareTag("Character"))
            {
                OnAttack?.Invoke(this, EventArgs.Empty);
                Test = true;
                StartCoroutine(ResetTestAfterDelay(2f));
            }
            if (onHitBool && other.CompareTag("Character"))
            {

                Player player = other.transform.GetComponent<Player>();
                if (player == null)
                {
                    //Test = false;
                }
                if (player != null)
                {
                    player.PlayerDie();
                }
            }

            if (!Test && other.CompareTag("Enemy") && other.gameObject != parent.gameObject)
            {
                OnAttack?.Invoke(this, EventArgs.Empty);
                Test = true;
                StartCoroutine(ResetTestAfterDelay(2f));
            }
            if (onHitBool && other.CompareTag("Enemy") && other.gameObject != parent.gameObject)
            {

                Enemy enemy = other.transform.GetComponent<Enemy>();
                if (enemy == null)
                {
                    //Test = false;
                }
                if (enemy != null)
                {
                    enemy.EnemyDie();
                }
            }
        }
    }
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Enemy") && other.gameObject == parent.gameObject)
    //    {
    //        Test = false;
    //    }
    //}
    IEnumerator ResetTestAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Test = false;
    }
    IEnumerator DelayPlayerDie(Player player)
    {
        yield return new WaitForSeconds(0.4f);
        player.PlayerDie();
    }
    IEnumerator DelayEnemyDie(Enemy enemy)
    {
        yield return new WaitForSeconds(0.4f);
        if (enemy != null)
        {
            enemy.EnemyDie();
        }
        
    }
}