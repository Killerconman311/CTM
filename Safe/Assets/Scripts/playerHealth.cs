using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public float health = 3;




    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            health--;
        }

        /*if (other.CompareTag("sword"))
        {
            health = health - .05f;   
        }*/
    }
}
