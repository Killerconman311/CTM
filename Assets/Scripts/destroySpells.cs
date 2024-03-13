using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySpells : MonoBehaviour
    
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
}
