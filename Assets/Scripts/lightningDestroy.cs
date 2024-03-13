using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningDestroy : MonoBehaviour

{
    public float timer = 7f;
    private void Update()
    {
        timer = timer - Time.deltaTime;

        if (timer <= 0)
        {

            Destroy(this.gameObject);
        }

    }
   
    private void OnCollisionEnter(Collision collision)
    {
     if(collision.gameObject.CompareTag("enemy"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }


    }
}