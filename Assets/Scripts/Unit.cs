using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int health = 100;
    public bool IsAlive { get => health > 0; }

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Taking Damage: " + damage);
        health -= damage;
    }
}
