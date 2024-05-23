using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float Speed = 5f;
    public float DashCooldown = 10f;
    public float DashInvincibilityTime = 0.25f;

    public float TimeBetweenSpawns = 0.2f;
    public GameObject SkeletonPrefab;

    private Rigidbody2D rb;
    private Vector2 MoveInput;

    [SerializeField] private int Health;
    [SerializeField] private Weapon Gun;

    private bool canDash = true;
    private bool isDashing = false;
    private bool canTakeDamage = true;

    private float TimeToSpawn;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        TimeToSpawn = Time.time;
    }

    private void Update() {
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Gun.Attack();
        }

        isDashing = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= TimeToSpawn) {
            for (int i = 0; i < 3; i++) {
                int Posx = (int)Random.Range(-1, 1);
                int Posy = (int)Random.Range(-1, 1);

                Instantiate(SkeletonPrefab, transform.position + new Vector3 (Posx, Posy), Quaternion.identity);
            }

            TimeToSpawn = Time.time + TimeBetweenSpawns;
        }
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + (MoveInput.normalized * Speed * Time.fixedDeltaTime));

        if (isDashing) {
            if (!canDash) return;

            Dash();
            isDashing = false;
        }
    }

    private void Dash() {
        Speed *= 2.5f;
        canTakeDamage = false;
        canDash = false;

        StartCoroutine(ResentCanTakeDamage());
        StartCoroutine(ResentCanDash());
    }

    private IEnumerator ResentCanTakeDamage() {
        yield return new WaitForSeconds (DashInvincibilityTime);
        canTakeDamage = true;
        Speed /= 2.5f;
    }

    private IEnumerator ResentCanDash() {
        yield return new WaitForSeconds (DashCooldown);
        canDash = true;
    }

    public void TakeDamage(int Damage) {
        if (!canTakeDamage) return;

        Health -= Damage;

        if (Health <= 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}
