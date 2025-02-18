using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.GraphicsBuffer;

public class Move_Boat : MonoBehaviour
{
    //hold variables
    public float holdTime = 1f; // Tempo necessário para considerar um "hold touch"
    private float touchStartTime;
    private bool isHolding = false;

    //mov variables
    public float BoatSpeed;
    private float BS;

    //tap variables
    private Vector2 starTouchPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;
    public float tapRange;

    //rotaround
    public float RotAroundOff = 1;
    public float DragSpeed;
    private float drag; // atual
    public float dragForce = 0; 
    private bool d;

    public float minXloc = -0.5f;   // mínima
    public float maxXloc = 0.5f; // máxima


    //switch script
    public RotAround rotAround;

    //camera
    public SmoothCameraFollow smoothCameraFollow;
    public GameObject CameraSC;

    private void Start()
    {
        BS = BoatSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        MoveBoat(Mathf.Clamp(drag, minXloc, maxXloc), dragForce * -1);
        tap();
        //hold();
        //CameraSC.GetComponent<SmoothCameraFollow>().CameraStopInterpolation(smoothCameraFollow.whirlpool);

    }

    private void MoveBoat(float WhirlpoolDrag, float dra)
    { 
        transform.Translate(new Vector3(WhirlpoolDrag, BoatSpeed, 0) * Time.deltaTime, Space.Self);
        //transform.Translate(new Vector3(dra, 0, 0) * Time.deltaTime, Space.Self);
        //transform.position += new Vector3(0, 0, BoatSpeed) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Whirlpool_Trigger"))
        {
            BoatSpeed = 0;
            RotAroundOff = 1;
            smoothCameraFollow.whirlpool = !smoothCameraFollow.whirlpool;
            smoothCameraFollow.playertarget = other.transform;
            d = other.GetComponent<RotAround>().clockwise;
            /*if (d)
            {
                drag = DragSpeed;
                minXloc = 0;
                maxXloc = 0.5f;
            }
            else if (!d)
            {
                drag = -DragSpeed;
                maxXloc = 0;
                minXloc = -0.5f;
            }*/

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Whirlpool_Trigger"))
        {
            dragForce = 0;
            //drag = 0;
        }
    }

    public void tap()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            starTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPosition - starTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                Debug.Log("Tap");
                BoatSpeed = BS;
                RotAroundOff = 0;
                smoothCameraFollow.whirlpool = !smoothCameraFollow.whirlpool;
                smoothCameraFollow.playertarget = this.transform;

            }
        }

    }

    public void hold()
    {
        // Verifica se há pelo menos um toque na tela
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Pega o primeiro toque

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Registra o tempo inicial do toque
                    touchStartTime = Time.time;
                    isHolding = false;
                    break;

                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    // Verifica se o toque está sendo mantido por mais tempo que o necessário
                    if (Time.time - touchStartTime > holdTime && !isHolding)
                    {
                        isHolding = true;
                        dragForce = drag + drag;
                        Debug.Log("Hold touch detectado!");
                        // Aqui você pode chamar a função que deseja executar durante o hold
                    }
                    break;

                case TouchPhase.Ended:
                    // Reseta o estado quando o toque termina
                    isHolding = false;
                    dragForce = 0;
                    break;
            }
        }
    }
}
