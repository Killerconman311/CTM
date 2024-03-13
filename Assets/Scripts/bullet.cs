using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float delay = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

    }
}
