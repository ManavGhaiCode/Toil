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

    public float DropChange = 0.3f;
    public GameObject[] Drops;

    [SerializeField] protected int Health;

    public virtual void Start() {
        Target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        TimeToAttack = Time.time + TimeBetweenAttacks;
    }

    public virtual void Die() {
        System.Random rand = new System.Random();

        float change = (float)rand.NextDouble();

        if (change <= DropChange) {
            int i = Random.Range(0, Drops.Length);

            Instantiate(Drops[i], transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int Damage) {
        Health -= Damage;

        if (Health <= 0) {
            Die();
        }
    }
}