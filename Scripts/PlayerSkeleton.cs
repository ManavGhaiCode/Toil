using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; 

public class PlayerSkeleton : MonoBehaviour {
    private Transform Target;
    private Rigidbody2D rb;

    public float Speed;
    public float AttackSpeed;

    public float TimeBetweenAttacks = 0.2f;
    private float TimeToAttack;

    [SerializeField] private int Health;

    private Seeker seeker;

    private Path path;
    private int CurrentWayPoint = 0;

    private Vector2 Dir;
    private float TimeToPath;

    public float MoveWaypoint = 0.2f;
    public float StopDistance = 1.5f;
    public int Damage = 1;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        TimeToAttack = Time.time + TimeBetweenAttacks;

        seeker = GetComponent<Seeker>();
        SearchTarget();
    }

    private void Update() {
        if (Target == null) {
            SearchTarget();
            return;
        }

        if (path == null) return;

        if (Time.time > TimeToPath) {
            seeker.StartPath(rb.position, Target.position, OnPath);
            TimeToPath = Time.time + .5f;
        }

        if (Vector2.Distance(rb.position, Target.position) < StopDistance) {
            if (Time.time >= TimeToAttack) {
                StartCoroutine(Attack());
                TimeToAttack = Time.time + TimeBetweenAttacks;
            }

            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Vector2.Distance(rb.position, path.vectorPath[CurrentWayPoint]) < MoveWaypoint) {
            CurrentWayPoint += 1;
        }

        if (CurrentWayPoint >= path.vectorPath.Count) {
            SearchTarget();
        }

        Dir = ((Vector2)path.vectorPath[CurrentWayPoint] - rb.position).normalized;
    }

    private void FixedUpdate() {
        rb.MovePosition((Vector2)rb.position + (Dir * Speed * Time.fixedDeltaTime));
    }

    private void OnPath(Path p) {
        if (!p.error) {
            path = p;
            CurrentWayPoint = 0;
        }
    }

    private void SearchTarget() {
        Enemy[] Enemies = GameObject.FindObjectsOfType<Enemy>();
        Transform[] Transforms = new Transform [Enemies.Length];

        for (int i = 0; i < Enemies.Length; i++) {
            Transforms[i] = Enemies[i].transform;
        }

        for (int i = 0; i < Transforms.Length; i++) {
            if (Target == null) {
                Target = Transforms[i];
                continue;
            }

            float TargetDis = Vector2.Distance(Target.position, transform.position);
            float TransformDis = Vector2.Distance(Transforms[i].position, transform.position);

            if (TransformDis < TargetDis) {
                Target = Transforms[i];
            }
        }

        seeker.StartPath(rb.position, Target.position, OnPath);
    }

    IEnumerator Attack() {
        Enemy enemy = Target.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.TakeDamage(Damage);
        }

        Vector2 OriginalPosition = transform.position;
        Vector2 TargetPosition = Target.position;

        float percent = 0f;
        while (percent <= 1) {
            if (Target != null) {
                percent += Time.deltaTime * AttackSpeed;
                float Interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;

                rb.position = Vector2.Lerp(OriginalPosition, TargetPosition, Interpolation);

                yield return null;
            } else {
                yield return null;
            }
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
    
    public void TakeDamage(int Damage) {
        Health -= Damage;

        if (Health <= 0) {
            Die();
        }
    }
}