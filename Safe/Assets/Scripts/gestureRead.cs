using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class gestureRead : MonoBehaviour
{

    public GameObject lightning;
    public GameObject waterWall;
    //getting info to spawn lightnting at space of controller
    public GameObject rightController;
    //wand stuff
    public GameObject wand;

    private Vector3 wandPos;
    public GameObject powerUp;
    Rigidbody wandBod;

    //Distance sets the positiion of the water wall when it is instantiated
    public float distance = 1.5f;

    //this sets the spawn position of lightning 
    public float lightningDistance = 1.5f;

    public Transform playerTransform;

    public Transform lightningSpawn;

    public Transform cam;

    Vector3 contPos;
    Rigidbody lightningRigidbody;



    //count to limit walls and lightning
    public int wallCount = 0;
    public int lightningCount = 0;
    //checks to see if player is holding wand
    public bool holdingWand = false;

    //lightning spell stuff 
    public GameObject lightningPrefab; // Add this line to define your lightning prefab
    private bool isTriggerPulled = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 wandpos = wand.transform.position;
        lightningRigidbody = lightning.GetComponent<Rigidbody>();
        contPos = rightController.transform.position;
        wandBod = wand.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("XRI_Right_Trigger") > 0.5f)
        {
            isTriggerPulled = true;
        }
        else
        {
            isTriggerPulled = false;
        }

        if (isTriggerPulled)
        {
            FireLightning();
        }
    }
    void FixedUpdate()
    {

        if (holdingWand == true)
        {

            wandBod.useGravity = true;
            Destroy(powerUp);
        }

    }



    public void wandHold()
    {
        holdingWand = true;
    }
    public void wandDrop()
    {
        holdingWand = false;
    }




    public void OnGestureCompleted(GestureCompletionData gestureCompletionData)
    {
        {
            //lightning is at position 0 in the .dat files index, water at 1, and so on...
            if (gestureCompletionData.gestureID == 0 && gestureCompletionData.similarity > .1 && holdingWand == true)
            {
                Debug.Log("lightning recognized");
                FireLightning();


            }
            if (gestureCompletionData.gestureID == 1 && gestureCompletionData.similarity > .7 && holdingWand == true)
            {
                Debug.Log("water recognized");
                spawnWater();

            }
            if (gestureCompletionData.gestureID == 2 && gestureCompletionData.similarity > .7 && holdingWand == true)
            {
                Debug.Log("gesture not recognized");
            }
            else
            {
                Debug.Log("gesture not recognized");
            }
        }
        if (holdingWand == false)
        {
            Debug.Log("not holding wand");
        }
    }

    private void FireLightning()
    {
        // Get an inactive lightning bolt from the object pool
        GameObject lightningBolt = objPoolManager.SpawnObject(lightningPrefab, lightningSpawn.position, Quaternion.identity, objPoolManager.PoolType.Spells);

        // Check if the lightning bolt object is not null
        if (lightningBolt != null)
        {
            // Get the lightningDestroy component from the lightning bolt object
            lightningDestroy lightningScript = lightningBolt.GetComponent<lightningDestroy>();

            // Call the OnEnableLightning method of lightningDestroy if the component exists
            if (lightningScript != null)
            {
                lightningScript.OnEnableLightning();
            }
        }

        // // Get the lightningDestroy component from the lightning bolt object
        // lightningDestroy lightningScript = lightningBolt.GetComponent<lightningDestroy>();

        // // Call the OnEnable method of lightningDestroy if the component exists
        // if (lightningScript != null)
        // {
        //     lightningScript.OnEnableLightning();
        // }


    }

    private void spawnWater()
    {

        Vector3 camPosition = new Vector3(cam.position.x, cam.position.y, cam.position.z);
        Vector3 camForward = cam.forward;
        Quaternion cameraRot = cam.rotation;
        Vector3 instantiatePosition = camPosition + camForward * distance;

        Instantiate(waterWall, instantiatePosition, cameraRot);
    }

}
