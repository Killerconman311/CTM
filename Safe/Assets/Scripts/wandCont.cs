using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wandCont : MonoBehaviour
{


    private Animator anim;

    //How to access gestureRead script
    public GameObject gestRec;
    //access to player health
    public GameObject player;
    //ring that changes color based on player health
    Transform healthRing;
    Renderer ringColor;
    //materils to change color of ring
    public Material[] ringMat;

    Rigidbody wandBod;
    public Transform controllerTransform;
    bool isSwitching = false;
    public float flicker = 0.3f;

    public GameObject[] spellList;
    public ParticleSystem[] spellFX;
    public Transform FXspawn;



    void Start()
    {
        // gets the first child on this object
        healthRing = transform.GetChild(0);
        ringColor = healthRing.GetComponent<MeshRenderer>();


        wandBod = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetBool("grabbed", false);
        anim.Play("wandIdle");
    }

    void Update()
    {
        if (gestRec.GetComponent<gestureRead>().holdingWand == true)
        {
            anim.SetBool("grabbed", true);
            anim.enabled = false; // Disable the Animator
            //wandBod.useGravity = true;
            //wandBod.transform.rotation = controllerTransform.rotation;
        }
        if (gestRec.GetComponent<gestureRead>().holdingWand == false)
        {
            wandBod.transform.rotation = Quaternion.Euler(new Vector3(279, 180, 180));
            anim.enabled = true;
        }
    }

    void FixedUpdate()
    {
        if (player.GetComponent<playerHealth>().health == 3)
        {
            healthRing.GetComponent<MeshRenderer>().material = ringMat[0];
            isSwitching = false;
        }
        if (player.GetComponent<playerHealth>().health == 2)
        {
            healthRing.GetComponent<MeshRenderer>().material = ringMat[1];
            isSwitching = false;
        }
        if (player.GetComponent<playerHealth>().health <= 1 && !isSwitching)
        {
            StartCoroutine("SwitchMaterial", healthRing.GetComponent<MeshRenderer>());
            isSwitching = true;
        }
    }
    IEnumerator SwitchMaterial(Renderer renderer)
    {
        while (player.GetComponent<playerHealth>().health <= 1) // Only continue the loop while the player's health is 1
        {
            renderer.material = ringMat[2];
            yield return new WaitForSeconds(flicker);
            renderer.material = ringMat[3];
            yield return new WaitForSeconds(flicker);
        }

        if (player.GetComponent<playerHealth>().health > 1)
        {
            isSwitching = false;
        }

    }

}

