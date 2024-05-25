using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float Speed = 5f;

    private Camera cam;
    private Transform player;

    private bool isFollowing = true;

    private void Start() {
        cam = Camera.main;
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate() {
        if (!isFollowing) return;
        if (player == null) return;

        Vector2 Pos = ((player.position - transform.position).normalized * Speed * Time.deltaTime);
        transform.position += new Vector3 (Pos.x, Pos.y);
    }

    public void Shake(int Intencity) {
        isFollowing = false;
        StartCoroutine(ShakeCoroutine(Intencity));

        if (Intencity == 2) {
            Time.timeScale = 0.1f;
        }
    }

    private IEnumerator ShakeCoroutine(int Intencity) {
        float now = Time.time;
        bool Shaking = true;

        System.Random rand = new System.Random();

        while (Shaking) {
            float currentTime = Time.time;

            if (currentTime - now > .2f) {
                Shaking = false;
            } else {
               Vector2 Dir;

                Dir.x = (float)rand.NextDouble();
                Dir.y = (float)rand.NextDouble();

                if (rand.NextDouble() < 0.5f) {
                    Dir.x = -Dir.x;
                }

                if (rand.NextDouble() < 0.5f) {
                    Dir.y = -Dir.y;
                }

                if (Intencity == 0) {
                    Dir = Dir.normalized; 
                    Dir /= 75;
                } else if (Intencity == 1) {
                    Dir = Dir.normalized; 
                    Dir /= 5;
                } else if (Intencity == 2) {
                    Dir *= 10;
                    Dir = Dir.normalized;

                    Dir /= 2;
                }

                transform.position += (Vector3)Dir;

                yield return new WaitForSeconds ( 0.2f / 4 );
            }
        }

        isFollowing = true;
        Time.timeScale = 1f;
    }
}