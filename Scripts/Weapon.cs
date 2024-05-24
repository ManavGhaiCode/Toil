using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public int Damage;
    public float TimeBetweenAttacks;

    private float TimeToAttack;
    private Transform player;
    private Player PlayerScript;
    private BoxCollider2D collider;
    private bool isAttacking = false;
 
    private void Start() {
        collider = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player").transform;

        PlayerScript = player.GetComponent<Player>();
    }

    private void Update() {
        if (isAttacking) return;
        if (player == null) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 LookDir = mousePos - (Vector2)transform.position;

        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3 (0, 0, angle));

        Vector2 PosDir = ((Vector2)player.position - mousePos).normalized;
        PosDir = -PosDir;

        if (PlayerScript.isSmall) {
            PosDir /= 2;
        }

        transform.position = player.position + (Vector3)PosDir;
    }

    public void Attack() {
        if (Time.time <= TimeToAttack) return;

        transform.position += transform.right;
        isAttacking = true;

        collider.size = new Vector2 (1f, 0.3f);

        if (PlayerScript.isSmall) {
            collider.size /= 2;
        }

        StartCoroutine(ResetAttacking());

        TimeToAttack = Time.time + TimeBetweenAttacks;
    }

    private IEnumerator ResetAttacking() {
        yield return new WaitForSeconds (.1f);
        isAttacking = false;

        collider.size = new Vector2 (0.0001f, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D HitInfo) {
        Enemy enemy = HitInfo.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.TakeDamage(Damage);
        }
    }
}