using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private CinemachineVirtualCamera camera2;
    private CinemachineFramingTransposer cameraFraming;
    private CinemachineFramingTransposer cameraFraming2;
    [SerializeField] CinemachineImpulseSource impulseSource;
    Tween changeCamera;
    int count = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        EnemyDetector.Instance.OnScale += Camera_OnScale;
        cameraFraming =camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        cameraFraming2 = camera2.GetCinemachineComponent<CinemachineFramingTransposer>();
        Debug.Log(111);
        Transform player=Player.Instance.transform;
        impulseSource = player.GetComponent<CinemachineImpulseSource>();
        camera.Follow = player;
        camera2.Follow = player;
        //impulseSource = FindObjectOfType<CinemachineImpulseSource>();
        Debug.Log(222);
        //impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Camera_OnScale(object sender, EnemyDetector.ScaleArgs e)
    {
        count++;
        Debug.Log("CountScale :" + count);
        float targetDistance = cameraFraming.m_CameraDistance + 2f * e.scale.x;

        changeCamera = DOTween.To(() => cameraFraming.m_CameraDistance, x => cameraFraming.m_CameraDistance = x, targetDistance, 1f)
            .OnComplete(() => changeCamera.Kill());
        float targetDistance2 = cameraFraming2.m_CameraDistance + 2f;
        cameraFraming2.m_CameraDistance = targetDistance2;
        //distanceCoroutines.Enqueue(HandleCameraDistanceIncrease(e.scale.x));
        //Debug.Log("CountQueue :"+distanceCoroutines.Count);
        //if (!isProcessing)
        //{
        //    StartCoroutine(RunDistanceCoroutines());
        //}
    }
    public void ImpulseCamera()
    {
        impulseSource.GenerateImpulse();
    }
    // Update is called once per frame
    public void Win(GameObject gameObject)
    {
        gameObject.transform.DOLocalRotate(Vector3.zero,1f);
        camera.Priority = 5;
        camera2.Priority = 10;
    }
    //private Queue<IEnumerator> distanceCoroutines = new Queue<IEnumerator>();
    //private bool isProcessing = false;
    //private IEnumerator RunDistanceCoroutines()
    //{
    //    isProcessing = true;
    //    while (distanceCoroutines.Count > 0)
    //    {
    //        yield return StartCoroutine(distanceCoroutines.Dequeue());
    //    }
    //    isProcessing = false;
    //}

    //private IEnumerator HandleCameraDistanceIncrease(float x)
    //{
    //    float targetDistance = cameraFraming.m_CameraDistance + 4f*x;

    //    yield return DOTween.To(() => cameraFraming.m_CameraDistance, x => cameraFraming.m_CameraDistance = x, targetDistance, 0.5f).WaitForCompletion();

    //    //float targetDistance2 = cameraFraming2.m_CameraDistance + 4f*x;
    //    //yield return DOTween.To(() => cameraFraming2.m_CameraDistance, x => cameraFraming2.m_CameraDistance = x, targetDistance2, 1f).WaitForCompletion();
    //}
}
