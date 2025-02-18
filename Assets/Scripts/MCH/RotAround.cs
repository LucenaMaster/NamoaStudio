using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RotAround : MonoBehaviour
{
    //Material
    //public Material WhirlpoolMat;
    Material WhirlpoolMat = GetComponent<Renderer>().M_Whirlpool;

    //boat
    public Transform BoatRef;
    private float RotAroundSpeed = 90;

    private float RotAroundTrigger = 0;


    //look at variables

    private Coroutine LookCoroutine;
    public float SpeedRot = 1f;
    public bool clockwise = true;

    // move boat variables
    public Move_Boat move_boat;

    // trigger rotaround
    private bool TriggerTurn = false;
    private bool isPaused = false;

    //transform
    public Transform pivotPoint;
    //public Vector3 pivotPoint = Vector3.zero;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move_boat.RotAroundOff == 0)
        {
            isPaused = true; // Alterna entre pausado e despausado
        }

        // Aplica a rotação apenas se não estiver pausado
        if (!isPaused)
        {
            RotBoatAround(RotAroundTrigger, TriggerTurn);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Boat"))
        {
            RotAroundTrigger = RotAroundSpeed * SpeedRot;
            TriggerTurn = true;
            isPaused = false;
            StartRotating();

        }

    }


    public void RotBoatAround(float Speed, bool Trigger)
    {
        if (clockwise == true && Trigger == true)
        {
            BoatRef.RotateAround(pivotPoint.position, Vector3.up, Speed * Time.deltaTime);
            //print("tocou_direita" + transform.name);
        }
        else if(clockwise == false && Trigger == true)
        {
            BoatRef.RotateAround(pivotPoint.position, Vector3.down, Speed * Time.deltaTime);
        }
               
    }

    public void StartRotating()
    {
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(this.transform.position - BoatRef.position);
        lookRotation = Quaternion.Euler(BoatRef.rotation.eulerAngles.x, lookRotation.eulerAngles.y, BoatRef.rotation.eulerAngles.z);

        float time = 0;

        Quaternion initialRotation = BoatRef.rotation;
        while (time < 1)
        {
            BoatRef.rotation = Quaternion.Slerp(initialRotation, lookRotation, time);

            time += Time.deltaTime * SpeedRot;

            yield return null;
        }
    }
}
