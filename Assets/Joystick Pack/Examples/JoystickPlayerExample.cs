using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    //public VariableJoystick variableJoystick;
    public FloatingJoystick floatingJoystick;
    public Rigidbody rb;
    private Tween moveTween;
    private Tween lookAtTween;
    private PlayerAnimator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        floatingJoystick = GameController.Instance.FloatingJoystick();
    }
    public void Update()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        //if (direction != Vector3.zero)
        //{
        //    MoveCharacter(-direction);
        //    playerAnimator.PlayerRun(true);
        //}
        //rb.velocity = direction * speed;
        //playerAnimator.PlayerRun(true);
        //Vector3 targetPosition = transform.position - direction.normalized * speed;
        ////transform.DOLocalRotate(direction, 0.1f).SetEase(Ease.Linear);
        //transform.DOLookAt(targetPosition, 0.1f).SetEase(Ease.Linear);
        if(direction == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
        if (direction != Vector3.zero)
        {
            //rb.AddForce(-direction * speed*(1+transform.localScale.x/5) * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.velocity = -direction * speed* (1 + transform.localScale.x / 5);
            playerAnimator.PlayerRun(true);
            Vector3 targetPosition = transform.position - direction.normalized * speed;
            //transform.DOLocalRotate(direction, 0.1f).SetEase(Ease.Linear);
            transform.DOLookAt(targetPosition, 0.1f).SetEase(Ease.Linear);
        }

    }
    private void MoveCharacter(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction.normalized * speed;

        moveTween.Kill();
        lookAtTween.Kill();

        moveTween = transform.DOMove(targetPosition, 1f).SetEase(Ease.Linear);
        lookAtTween = transform.DOLookAt(targetPosition, 0.1f).SetEase(Ease.Linear);
    }
}