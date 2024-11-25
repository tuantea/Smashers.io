using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public static Player Instance { get; private set; }
    [SerializeField] private Transform visualTransform;

    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform hammer;
    PlayerAnimator playerAnimator;
    private bool isPlayGame=true;
    bool test = false;
    [SerializeField] private GameObject CharacterExplosion;
    public static event EventHandler<EventArgs> OnPlayerInitialized;
    public event EventHandler<EventArgs> OnLost;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        OnPlayerInitialized?.Invoke(this, EventArgs.Empty);
    }
    void Start()
    {
        hammer.transform.SetParent(rightHand, true);
        hammer.transform.localRotation = Quaternion.Euler(0, 0, 90);
        EnemyDetector.Instance.OnScale += Player_OnScale;
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Player_OnScale(object sender, EnemyDetector.ScaleArgs e)
    {
        transform.DOScale(transform.localScale+e.scale/2, 1f);
    }


    // Update is called once per frame
    void Update()
    {
        if (StartGame.Instance.IsStartGame()&& isPlayGame)
        {

            if (Input.GetMouseButtonUp(0))
            {
                test = true;
                transform.DOKill();
                playerAnimator.PlayerRun(false);
                playerAnimator.PlayerAttack();
            }
            if (Input.GetKey(KeyCode.K))
            {
                //playerAnimator.PlayerDie();
                //visualTransform.localScale = new Vector3(1, 0.2f, 1);
                //StartCoroutine(ScalePlayer());
                PlayerDie();
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerAnimator.PlayerVictory(true);
                CameraController.Instance.Win(gameObject);
                isPlayGame = false;
                //visualTransform.localScale = new Vector3(1, 0.2f, 1);
                //StartCoroutine(ScalePlayer());
            }
        }
    }
    public void PlayerDie()
    {
        PlayerAnimator playerAnimator = GetComponent<PlayerAnimator>();
        playerAnimator.PlayerDie();
        CharacterExplosion.SetActive(true);
        visualTransform.localScale = new Vector3(1, 0.2f, 1);
        StartCoroutine(ScalePlayer());
        OnLost?.Invoke(this, EventArgs.Empty);
    }
    IEnumerator ScalePlayer()
    {
        yield return new WaitForSeconds(0.5f);
        visualTransform.DOScale(Vector3.zero,1f);
        Destroy(gameObject);

    }
}
