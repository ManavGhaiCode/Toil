using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    protected Transform Target;
    protected Rigidbody2D rb;

    public float Speed;
    public float AttackSpeed;

    public int MaxHealth;
    protected int Health;

    private void Start() {
        Target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Die() {}
    public void TakeDamage(int Damage) {
        Health -= Damage;

        if (Health <= 0) {
            Die();
        }
    }
}