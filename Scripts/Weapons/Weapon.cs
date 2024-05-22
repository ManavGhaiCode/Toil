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

        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3 (0, 0, angle));

        Vector2 PosDir = ((Vector2)Player.position - mousePos).normalized;
        PosDir = -PosDir;

        transform.position = Player.position + (Vector3)PosDir;

        Debug.Log(mousePos.normalized - (Vector2)Player.position);
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