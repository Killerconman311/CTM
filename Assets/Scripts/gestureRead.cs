using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class gestureRead : MonoBehaviour
{
    public GameObject lightning;
    public GameObject waterWall;


    //Distance sets the positiion of the water wall when it is instantiated
    public float distance = 1.5f;

    //this sets the spawn position of lightning 
    public float lightningDistance = 1.5f;



    public Transform playerTransform;

    public Transform lightningSpawn;

    public Transform cam;

    //getting info to spawn lightnting at space of controller
    public GameObject rightController;
    Vector3 contPos;

    

    Rigidbody lightningRigidbody;

    //count to limit walls and lightning

    public int wallCount = 0;

    public int lightningCount = 0;






    // Start is called before the first frame update
    void Start()
    {
        lightningRigidbody = lightning.GetComponent<Rigidbody>();
        contPos = rightController.transform.position;

    }
    

    // Update is called once per frame
    void Update()
    {
        



    }
    public void OnGestureCompleted(GestureCompletionData gestureCompletionData)
    {
        //lightning is at position 0 in the .dat files index, water at 1, and so on...
        if (gestureCompletionData.gestureID == 0 && gestureCompletionData.similarity > .2)
        {
            Debug.Log("lightning recognized");
            spawnLightning();
            
            
        }
        if (gestureCompletionData.gestureID == 1 && gestureCompletionData.similarity > .65)
        {
            
            {
                Debug.Log("water recognized");
                spawnWater();
                
            }
        }
    }

    private void spawnLightning()
    {

        Vector3 playerPosition = playerTransform.position;
        Vector3 playerForward = playerTransform.forward;

        // Vector3 lightningS = contPos;

        //Vector3 instantiatePositionL = playerPosition + playerForward * lightningDistance;
        Vector3 instantiatePositionL = lightningSpawn.position;
        Instantiate(lightning, instantiatePositionL, Quaternion.identity);

    }
    private void spawnWater()
    {

        Vector3 camPosition = new Vector3(cam.position.x, cam.position.y, cam.position.z);
        Vector3 camForward = cam.forward;
        Quaternion cameraRot = cam.rotation;
        Vector3 instantiatePosition = camPosition + camForward * distance;

        Instantiate(waterWall, instantiatePosition, cameraRot);
    }
    

     private GameObject FindNearestEnemy()
     {
         Collider[] colliders = Physics.OverlapSphere(lightningSpawn.position, distance, LayerMask.GetMask("enemy"));

        GameObject nearestEnemy = null;
         float nearestDistance = float.MaxValue;

         foreach (var collider in colliders)
         {
             float distanceToEnemy = Vector3.Distance(lightningSpawn.position, collider.transform.position);
             if (distanceToEnemy < nearestDistance)
             {
                 nearestDistance = distanceToEnemy;
                 nearestEnemy = collider.gameObject;
             }
         }

         return nearestEnemy;
     }
}
