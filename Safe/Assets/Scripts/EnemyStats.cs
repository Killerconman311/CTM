using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float EnemyHealth = 3;
    [SerializeField] FloatingHealthBar healthBar;
    private float maxHealth;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(EnemyHealth);

    }
    private void Start()
    {
        maxHealth = EnemyHealth;
        healthBar.UpdateHealthBar(EnemyHealth);
    }

   


    void Update()
    {
        if (EnemyHealth <= 0)
        {
            Died();
        }
    }

    public void TakeDamage(int damage)
{ 
    EnemyHealth -= damage;
    healthBar.UpdateHealthBar(EnemyHealth / maxHealth);
}

    private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("pSpell"))
    {
        TakeDamage(1);
        Debug.Log("enemy hit");
    }
}
    private void Died()
    {
        Destroy(gameObject);
    }

}