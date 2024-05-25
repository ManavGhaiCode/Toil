using UnityEngine;

public class WaveController : MonoBehaviour {
    [SerializeField] private GameObject[] Enemies;
    private int WitchCount = 0;
    private int CurrentWave = 0;

    private void Start() {
        Enemies[CurrentWave].SetActive(true);
    }

    public void EnemySpawn() {
        WitchCount += 1;
    }

    public void EnemyDeath() {
        WitchCount -= 1;

        if (WitchCount <= 0) {
            Enemies[CurrentWave].SetActive(false);
            CurrentWave += 1;

            if (CurrentWave < Enemies.Length) {
                Enemies[CurrentWave].SetActive(true);
            }
        }
    }
}