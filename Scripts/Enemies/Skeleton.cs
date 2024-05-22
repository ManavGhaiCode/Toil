using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy {
    private void Update() {
        
    }

    public override void Die() {
        Destroy(gameObject);
    }
}