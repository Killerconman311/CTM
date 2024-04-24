using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endAnim : MonoBehaviour
{
    public GameObject spellBookObject; // Reference to your spellbook GameObject

    private Animator animator; // Animator component
    // Start is called before the first frame update
    void Start()
    {
         animator = spellBookObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndAnimi(){
        
        // Get the Animator component
       

        // Initially set the spellbook to inactive
        
        animator.SetBool("isActive", false);
        spellBookObject.SetActive(false);
    }
}
