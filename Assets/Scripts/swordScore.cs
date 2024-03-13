using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordScore : MonoBehaviour
{
 public scoreKeep scoreKeep; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            scoreKeep.IncreaseScore(1); 
        }
    }
}
