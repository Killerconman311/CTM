using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public float timer = 7f;

    private void OnTriggerEnter(Collider other)
    {
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        timer = timer - Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }
    
}
