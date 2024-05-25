using UnityEngine;

public class HealthBar : MonoBehaviour {
    private SpriteRenderer sprite;
    [SerializeField] private Sprite[] Sprites;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void UpdateBar(int Health) {
        int i = 10 - Health;
        sprite.sprite = Sprites[i];
    }
}