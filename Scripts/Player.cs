using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float Speed = 5f;

    private Rigidbody2D rb;
    private Vector2 MoveInput;

    [SerializeField] private int Health;
    [SerializeField] private Weapon Gun;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Mouse0)) {
            Gun.Attack();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + (MoveInput.normalized * Speed * Time.fixedDeltaTime));
    }

    public void TakeDamage(int Damage) {
        Health -= Damage;

        if (Health <= 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}
