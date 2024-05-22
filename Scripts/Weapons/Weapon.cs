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

    private void Start() {
        TimeToAttack = Time.time;
    }

    public virtual void Attack() {}

    public void Reload() {
        clip = MaxClip;
    }
}