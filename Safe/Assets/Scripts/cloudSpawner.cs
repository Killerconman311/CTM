using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class cloudSpawner : MonoBehaviour
{

    public List<GameObject> cloudSpawns = new List<GameObject>();
    public List<GameObject> cloudPrefabs;
    public int activeClouds = 0;



    public float spawnTimer = 4f;
    public int cloudMax = 30;

    private GameObject lastSpawnPoint = null;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SpawnCloudsRoutine());
    }

    IEnumerator SpawnCloudsRoutine()
    {
        while (true) // Infinite loop
        {
            if (activeClouds < cloudMax) // Only spawn clouds if activeClouds is less than cloudMax
            {
                SpawnClouds();
                activeClouds++;
            }
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    void SpawnClouds()
    {
        // Select a random spawn point
        GameObject spawnPoint;
        do
        {
            spawnPoint = cloudSpawns[UnityEngine.Random.Range(0, cloudSpawns.Count)];
        } while (spawnPoint == lastSpawnPoint);
        Debug.Log("Spawn Point: " + spawnPoint.name);

        lastSpawnPoint = spawnPoint;

        // Select a random cloud prefab
        GameObject cloudPrefab = cloudPrefabs[UnityEngine.Random.Range(0, cloudPrefabs.Count)];

        // Spawn the cloud at the selected spawn point
        cloudPrefab = objPoolManager.SpawnObject(cloudPrefab, spawnPoint.transform.position, Quaternion.identity, objPoolManager.PoolType.Clouds);
        Rigidbody cloudRb = cloudPrefab.GetComponent<Rigidbody>();

        cloudRb.velocity = Vector3.forward * UnityEngine.Random.Range(-1, -3);

        Cloud cloudScript = cloudPrefab.AddComponent<Cloud>();
        cloudScript.endPointTag = "endPoint";
    }
    public void DecreaseActiveClouds()
    {
        activeClouds--;
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("endPoint"))
    //     {
    //         objPoolManager.ReturnObjectToPool(this.gameObject);
    //     }
    // }


}