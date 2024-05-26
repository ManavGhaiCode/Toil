using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float Speed = 5f;
    public float DashCooldown = 10f;
    public float DashInvincibilityTime = 1f;

    public float TimeBetweenSpawns = 0.2f;
    public GameObject SkeletonPrefab;

    private Rigidbody2D rb;
    private Vector2 MoveInput;

    private HealthBar healthBar;
    private CameraController cam;

    [SerializeField] private int Health;
    [SerializeField] private Weapon Needle;

    private float _Speed;
    private bool canDash = true;
    private bool isDashing = false;
    private bool canTakeDamage = true;

    public bool isSmall = false;

    private float TimeToSpawn;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GameObject.FindObjectOfType<HealthBar>().GetComponent<HealthBar>();
        cam = Camera.main.GetComponent<CameraController>();

        TimeToSpawn = Time.time;

        _Speed = Speed;
    }

    private void Update() {
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Needle.Attack();
        }

        isDashing = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= TimeToSpawn) {
            int Effect = Random.Range(0, 4);

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
        StartCoroutine(ResentDashSpeed());
        StartCoroutine(ResentCanDash());
    }

    private IEnumerator ResentCanTakeDamage() {
        yield return new WaitForSeconds (DashInvincibilityTime);
        canTakeDamage = true;
    }

    private IEnumerator ResentCanDash() {
        yield return new WaitForSeconds (DashCooldown);
        canDash = true;
    }

    private IEnumerator ResentDashSpeed() {
        yield return new WaitForSeconds (0.25f);
        _Speed = Speed;
    }

    public void TakeDamage(int Damage) {
        if (!canTakeDamage) return;

        Health -= Damage;

        canTakeDamage = false;
        StartCoroutine(ResentCanTakeDamage());

        healthBar.UpdateBar(Health);

        cam.Shake(2);

        if (Health <= 0) {
            Die();
        }
    }

    public void TakeHealth(int ExtraHealth) {
        Health += ExtraHealth;

        if (Health >= 10) {
            Health = 10;
        }

        healthBar.UpdateBar(Health);
    }

    public void ResetEffects() {
        transform.localScale = new Vector3 (1f, 1f, 0);
        Needle.transform.localScale = new Vector3 (1f, 1f, 0);

        _Speed = Speed;
        isSmall = false;

        Health = 10;

        healthBar.UpdateBar(Health);
    }

    private void Die() {
        Destroy(gameObject);
    }
}
