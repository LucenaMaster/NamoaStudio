using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_fit : MonoBehaviour
{
    public float baseAspectRatio = 9f / 16f; // Propor��o base (ex.: 16:9)
    public float baseFOV = 60f; // FOV base

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        AdjustFOV();
    }

    void AdjustFOV()
    {
        // Calcula o aspect ratio atual da tela
        float currentAspectRatio = (float)Screen.width / Screen.height;

        // Ajusta o FOV para manter a propor��o correta
        if (currentAspectRatio < baseAspectRatio)
        {
            float ratio = currentAspectRatio / baseAspectRatio;
            mainCamera.fieldOfView = baseFOV / ratio;
        }
        else
        {
            mainCamera.fieldOfView = baseFOV;
        }
    }
}
