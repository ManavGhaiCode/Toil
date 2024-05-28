using UnityEngine;

public class Brother : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D HitInfo){
        Player player = HitInfo.GetComponent<Player>();

        if (player != null) {
            GameObject.FindWithTag("Main")
                .GetComponent<SceneLoader>()
                .LoadScene("Win");
        }
    }
}