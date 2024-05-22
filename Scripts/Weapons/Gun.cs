using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
    public override void Attack() {
        if (Time.time < TimeToAttack) return;
        TimeToAttack = Time.time + TimeBetweenAttacks;

        GameObject bullet = Instantiate(BulletPerfab, FirePoint.position, FirePoint.rotation);
        Rigidbody2D BulletRB = bullet.GetComponent<Rigidbody2D>();
        Bullet BulletScrpit = bullet.GetComponent<Bullet>();

        BulletRB.AddForce(FirePoint.right * BulletSpeed, ForceMode2D.Impulse);
    }
}