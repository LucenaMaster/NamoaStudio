using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Borders : MonoBehaviour
{
    public GameManagerScript gameManagerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat"))
        {
            gameManagerScript.gameOver();
            Time.timeScale = 0;
        }
        
    }
}
