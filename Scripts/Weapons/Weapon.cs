using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public int Damage;

    public float TimeBetweenAttacks;
    public float BulletSpeed;

    protected float TimeToAttack;

    public int MaxClip;
    public int clip;

    public float BulletLifeTime;

    public GameObject BulletPerfab;
    public Transform FirePoint;

    private Transform Player;

    private void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 LookDir = mousePos - (Vector2)transform.position;

        if (Vector2.Distance(Player.position, mousePos) < 2f) {
            LookDir = mousePos + (Vector2)Player.position;
        }

        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3 (0, 0, angle));
        transform.position = Player.position + (Vector3)LookDir.normalized;
    }

    private void Start() {
        TimeToAttack = Time.time;

        Bullet BulletScrpit = BulletPerfab.GetComponent<Bullet>();

        BulletScrpit.Damage = Damage;
        BulletScrpit.LifeTime = BulletLifeTime;

        Player = GameObject.FindWithTag("Player").transform;
    }

    public virtual void Attack() {}

    public void Reload() {
        clip = MaxClip;
    }
}