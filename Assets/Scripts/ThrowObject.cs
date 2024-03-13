using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrowableObject : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    [System.Obsolete]
    void Start()
    {
        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Subscribe to the selectExited event to detect when the object is thrown
        grabInteractable.onSelectExited.AddListener(OnThrow);
    }

    void OnThrow(XRBaseInteractor interactor)
    {
        // Check if the object has a Rigidbody
        if (rb != null)
        {


            // Apply forces or perform other actions based on the throw
            rb.isKinematic = false;
            rb.AddForce(Vector3.forward, ForceMode.VelocityChange);

            // Uncomment the line below if you want to disable gravity while seeking the enemy
            rb.useGravity = false;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    // Enemy found, calculate the direction to the enemy
                    Vector3 directionToEnemy = (hit.point - transform.position).normalized;

                    // Add force in the direction of the enemy (you might need to adjust the force magnitude)
                    rb.AddForce(directionToEnemy * 5, ForceMode.Force);
                }
            }
        }
    }
}