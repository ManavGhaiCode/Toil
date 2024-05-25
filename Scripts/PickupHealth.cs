using UnityEngine;

public class PickupHealth : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D HitInfo) {
        Player player = HitInfo.GetComponent<Player>();

        if (player != null) {
            player.TakeHealth(1);
            Destroy(gameObject);
        }
    }
}