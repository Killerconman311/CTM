using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public string endPointTag;

    public GameObject cloudsSpawns;
    

    private void Start()
    {
        cloudsSpawns = GameObject.Find("cloudsSpawns");
    }   

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("endPoint"))
        {
            objPoolManager.ReturnObjectToPool(this.gameObject);
            cloudsSpawns.GetComponent<cloudSpawner>().DecreaseActiveClouds();
        }
    }
}
