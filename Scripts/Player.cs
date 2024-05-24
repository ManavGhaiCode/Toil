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
    [SerializeField] private Weapon Needle;

    private float _Speed = 5f;
    private bool canDash = true;
    private bool isDashing = false;
    private bool canTakeDamage = true;

    public bool isSmall = false;

    private float TimeToSpawn;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        TimeToSpawn = Time.time;
    }

    private void Update() {
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Needle.Attack();
        }

        isDashing = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= TimeToSpawn) {
            int Effect = Random.Range(-1, 6);

            switch (Effect) {
                case 0: {
                    transform.localScale = new Vector3 (2f, 2f, 0);
                    Needle.transform.localScale = new Vector3 (0.5f, 0.5f, 0);

                    _Speed = Speed / 1.1f;
                    isSmall = false;
                } break;

                case 1: {
                    transform.localScale = new Vector3 (0.5f, 0.5f, 0);
                    Needle.transform.localScale = new Vector3 (2f, 2f, 0);

                    _Speed = Speed * 1.25f;

                    isSmall = true;
                } break;

                case 2: {
                    Enemy[] Enemies = GameObject.FindObjectsOfType<Enemy>();

                    for (int i = 0; i < Enemies.Length; i++) {
                        Enemies[i].Speed *= 1.25f;
                    }
                } break;
            }

            for (int i = 0; i < 3; i++) {
                int Posx = (int)Random.Range(-1, 1);
                int Posy = (int)Random.Range(-1, 1);

                Instantiate(SkeletonPrefab, transform.position + new Vector3 (Posx, Posy), Quaternion.identity);
            }

            TimeToSpawn = Time.time + TimeBetweenSpawns;
        }
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + (MoveInput.normalized * _Speed * Time.fixedDeltaTime));

        if (isDashing) {
            if (!canDash) return;

            Dash();
            isDashing = false;
        }
    }

    private void Dash() {
        _Speed *= 2.5f;
        canTakeDamage = false;
        canDash = false;

        StartCoroutine(ResentCanTakeDamage());
        StartCoroutine(ResentCanDash());
    }

    private IEnumerator ResentCanTakeDamage() {
        yield return new WaitForSeconds (DashInvincibilityTime);
        canTakeDamage = true;
        _Speed /= 2.5f;
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
