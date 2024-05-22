using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Skeleton : Enemy {
    private Seeker seeker;

    private Path path;
    private int CurrentWayPoint = 0;

    private Vector2 Dir;
    private float TimeToPath;

    public float MoveWaypoint = 0.2f;
    public float StopDistance = 1.5f;
    public int Damage = 1;

    public override void Start() {
        base.Start();

        seeker = GetComponent<Seeker>();
        seeker.StartPath(rb.position, Target.position, OnPath);
    }

    private void OnPath(Path p) {
        if (!p.error) {
            path = p;
            CurrentWayPoint = 0;
        }
    }

    private void Update() {
        if (Target == null) return;

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

        if (path == null) return;

        if (Vector2.Distance(rb.position, path.vectorPath[CurrentWayPoint]) < MoveWaypoint) {
            CurrentWayPoint += 1;
        }

        if (CurrentWayPoint >= path.vectorPath.Count) {
            seeker.StartPath(rb.position, Target.position, OnPath);
        }

        Dir = ((Vector2)path.vectorPath[CurrentWayPoint] - rb.position).normalized;
    }

    private void OnCollisionEnter2D(Collision2D HitInfo) {
        Player player = HitInfo.collider.GetComponent<Player>();

        if (player != null) {
            player.TakeDamage(Damage);
        }
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + (Dir * Speed * Time.fixedDeltaTime));
    }

    public override void Die() {
        Destroy(gameObject);
    }

    IEnumerator Attack() {
        Vector2 OriginalPosition = transform.position;

        bool Hit = false;

        float percent = 0f;
        while (percent <= 1) {
            if (Target != null) {
                percent += Time.deltaTime * AttackSpeed;
                float Interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;

                rb.position = Vector2.Lerp(OriginalPosition, Target.position, Interpolation);

                yield return null;
            } else {
                yield return null;
            }
        }
    }
}