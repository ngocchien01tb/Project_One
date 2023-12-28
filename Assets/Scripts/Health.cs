using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private const int MaxHealth = 100;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have nagetive Damage");
            
        }
        this.health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have nagetive Healing");
            
        }

        var wouldBeOverMaxHealth = health + amount > MaxHealth;

        if (wouldBeOverMaxHealth)
        {
            this.health = MaxHealth;
        }
        else
        {
            this.health += amount;
        }
        
    }

    private void Die()
    {
        Debug.Log("I am Dead!");
        Destroy(gameObject);
    }
}
