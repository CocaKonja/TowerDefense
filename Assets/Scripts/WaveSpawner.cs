using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    public enum SpawnState {
        SPAWNING,
        WAITING,
        COUNTING
    };

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public Transform spawnPoint;
    private int waveIndex = 0;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    private float searchCountdown = 1f;

    public Text waveCountdownText;

    private SpawnState state = SpawnState.COUNTING;
    private void Start() {
        waveCountdown = timeBetweenWaves;
    }

    private void Update() {
        waveCountdownText.text = Mathf.Floor(waveCountdown + 1).ToString();

        if(state == SpawnState.WAITING) {
            if (!EnemyIsAlive()) {
                WaveCompleted();
            } else {
                return;
            }
        }
        if(waveCountdown <= 0) {
            if(state != SpawnState.SPAWNING) {
                StartCoroutine(SpawnWave(waves[waveIndex]));
            }
        } else {
            waveCountdown -= Time.deltaTime;
        }
    }

    //most likely update this to be desired functionality
    void WaveCompleted() {

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(waveIndex + 1 > waves.Length - 1) {
            waveIndex = 0;
        } else {
            waveIndex++;
        }

    }
    bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f) {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null) {

                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++){
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy) {
        Instantiate(_enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
