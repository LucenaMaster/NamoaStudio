using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] public Transform playertarget;
    //[SerializeField] private Transform whirlpooltarget;
    [SerializeField] private float smoothTime;
    private float ST;

    private Vector3 _currentvelocity = Vector3.zero;

    public bool whirlpool = false;

    private void Awake()
    {
        _offset = transform.position - playertarget.position;
    }
    private void Start()
    {
        ST = smoothTime;
    }
    private void LateUpdate()
    {
        FollowCam();

    }
    private void FollowCam()
    {

            Vector3 targetPosition = playertarget.position + _offset;
            transform.position = Vector3.SmoothDamp(current: transform.position, targetPosition, ref _currentvelocity, smoothTime);
    }

    public void CameraStopInterpolation(bool SwitchInterp)
    {
        if ( SwitchInterp == true)
        {
            while (ST < 10)
            {
                Debug.Log("mais");
                smoothTime ++;
                ST ++ ;
            }
        }else if ( SwitchInterp == false)
        {
            while (ST > 1)
            {
                Debug.Log("menos");
                smoothTime--;
                ST--;
            }
        }
       
    }

}
