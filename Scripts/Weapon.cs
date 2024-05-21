using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public int Damage;
    public float TimeBetweenAttacks;

    public int MaxClip;
    public int clip;

    public Transform FirePoint;

    public void Attack() {}
    public void Reload() {
        clip = MaxClip;
    }
}