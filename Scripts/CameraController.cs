using UnityEngine;

public class CameraController : MonoBehaviour {
    public float Speed = 5f;

    private Camera cam;
    private Transform player;

    private void Start() {
        cam = Camera.main;
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate() {
        Vector2 Pos = ((player.position - transform.position).normalized * Speed * Time.deltaTime);

        transform.position += new Vector3 (Pos.x, Pos.y);
    }
}