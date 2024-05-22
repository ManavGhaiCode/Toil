using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int Damage;
    public float LifeTime;

    private void Start() {
        Destroy(gameObject, LifeTime);
    }

    private void OnTriggerEnter2D(Collider2D HitInfo) {
        Enemy emeny = HitInfo.GetComponent<Enemy>();

        if (emeny != null) {
            emeny.TakeDamage(Damage);
        }
    }
}