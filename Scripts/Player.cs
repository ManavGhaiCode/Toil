using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float Speed = 5f;

    private Rigidbody2D rb;
    private Vector2 MoveInput;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + (MoveInput.normalized * Speed * Time.fixedDeltaTime));
    }
}
