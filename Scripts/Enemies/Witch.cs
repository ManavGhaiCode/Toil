using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Witch : Enemy {
    public float WaitTime = 2f;

    private Seeker seeker;

    private Path path;
    private int CurrentWayPoint = 0;

    private Vector2 Dir;
    private Vector2 Target;

    private float TimeToMove;

    private float MoveWaypoint = 0.25f;

    public override void Start() {
        base.Start();

        seeker = GetComponent<Seeker>();

        int Posx = (int)Random.Range(-8, 8);
        int Posy = (int)Random.Range(-5, 5);

        Target = new Vector3 (Posx, Posy, 0);
        seeker.StartPath(rb.position, Target, OnPath);
    }

    private void Update() {
        if (Target == null) return;
        if (path == null) return;

        if (Vector2.Distance(rb.position, path.vectorPath[CurrentWayPoint]) < MoveWaypoint) {
            CurrentWayPoint += 1;
        } 

        if (CurrentWayPoint >= path.vectorPath.Count) {
            Dir = Vector2.zero;
            StartCoroutine(WaitPath());
        } else {
            Dir = ((Vector2)path.vectorPath[CurrentWayPoint] - rb.position).normalized;
        }

    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + (Dir * Speed * Time.fixedDeltaTime));
    }

    private void OnPath(Path p) {
        path = p;
        CurrentWayPoint = 0;
    }

    private IEnumerator WaitPath() {
        yield return new WaitForSeconds (WaitTime);

        int Posx = (int)Random.Range(-8, 8);
        int Posy = (int)Random.Range(-5, 5);

        Target = new Vector3 (Posx, Posy, 0);
        seeker.StartPath(rb.position, Target, OnPath);
    }

    public override void Die() {
        Destroy(gameObject);
    }
}