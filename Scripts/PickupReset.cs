using UnityEngine;

public class PickupReset : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D HitInfo) {
        Player player = HitInfo.GetComponent<Player>();

        if (player != null) {
            player.ResetEffects();
            Destroy(gameObject);
        }
    }
}