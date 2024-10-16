﻿using System;

using UnityEngine;

public class VitalitySystem : MonoBehaviour
{
    public int MaxHealth { get => MaxHealth = 100; private set { } }
    public int CurrentHealth { get; private set; }

    public event Action OnTakeHit;
    public event Action OnDie;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            OnDie?.Invoke();
            Destroy(gameObject, 3);
        }
        else
        {
            OnTakeHit?.Invoke();
        }
    }
}