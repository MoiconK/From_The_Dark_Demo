using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    public event Action OnHealthZero;
    public event Action OnHealthChange;
    public event Action OnAwakeningChange;
    

    [SerializeField] public float maxHealth;
    public float maxAwakening;
    public float currentAwakening;
    public float currentHealth;

    protected override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;
        currentAwakening = 0;
        
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        OnHealthChange?.Invoke();
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            OnHealthZero?.Invoke();
            Debug.Log("Health is zero");
        }
    }

    public void IncreaseAwakening(float recharge)
    {       
        currentAwakening = Mathf.Clamp(currentAwakening + recharge, 0, maxAwakening);
        OnAwakeningChange?.Invoke();
    }

    public void DecreaseAwakening()
    {
        currentAwakening = 0;
        OnAwakeningChange.Invoke();
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnHealthChange?.Invoke();
    }
}
