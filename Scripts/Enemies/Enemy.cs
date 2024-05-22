using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    protected Transform Target;
    protected Rigidbody2D rb;

    public float Speed;
    public float AttackSpeed;

    public float TimeBetweenAttacks = 0.2f;
    public float TimeToAttack;

    [SerializeField] protected int Health;

    public virtual void Start() {
        Target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        TimeToAttack = Time.time + TimeBetweenAttacks;
    }

    public virtual void Die() {}
    public void TakeDamage(int Damage) {
        Health -= Damage;

        if (Health <= 0) {
            Die();
        }
    }
}