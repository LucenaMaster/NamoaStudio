using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collect_Float_Person : MonoBehaviour
{
    public GameManagerScript gameManagerScript;

    // collcte variables
    private int CollectPerson;

    public TextMeshProUGUI RescuePersonText;

    public int CollectPersonCount;

    public bool Winner = false;

    private void Start()
    {
        RescuePersonText.text = "Rescue: " + CollectPerson.ToString() + " / " + CollectPersonCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Person"))
        {
            CollectPerson++;
            RescuePersonText.text =  "Rescue: " + CollectPerson.ToString() + " / " + CollectPersonCount.ToString();
            Debug.Log(CollectPerson);
            Destroy(other.gameObject);

            if (CollectPerson == CollectPersonCount)
            {
                Winner = true;
                Debug.Log("Winner");

            }
        }

        if (other.gameObject.CompareTag("Pier"))
        {
            gameManagerScript.WinLevelUI.SetActive(true);
            Time.timeScale = 0;
           
        }
    }


}
