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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("lightning"))
        {
            TakeDamage(-1);
        }
    }

    public void TakeDamage(int damage)
    { 
        EnemyHealth -= damage;
        healthBar.UpdateHealthBar(EnemyHealth);
    }
    private void Died()
    {
        Destroy(gameObject);
    }

}