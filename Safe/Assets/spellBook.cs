
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class spellBook : MonoBehaviour
{
    public GameObject spellBookVisual; // Reference to the visual part of your spellbook
    public AnimationClip spellBookAnimation;     private Animator animator; // Animator component
    public AnimationClip spellBookExitAnimation;
   
    private bool hasAnimationPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component
        animator = spellBookVisual.GetComponent<Animator>();

        // Initially set the spellbook to inactive
        spellBookVisual.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the trigger is pressed
        if (Input.GetAxis("XRI_Left_Trigger") > 0.5f)
        {


            spellBookVisual.SetActive(true);

            if (!hasAnimationPlayed)
            {
                animator.SetBool("isActive", true);
                hasAnimationPlayed = true;
            }

        }
        else if (spellBookVisual.activeSelf) // Check if the spellbook is active
        {
            animator.SetBool("isActive", false);
            hasAnimationPlayed = false;


        }
    }



}


