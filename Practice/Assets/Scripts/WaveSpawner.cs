using UnityEngine;
using System.Collections;
using TMPro;
public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform blueEnemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 6f;
    public float countdown = 2f;
    public int waveIndex = 0;
    public float timeBetweenEnemies = 0.5f;
    public int EnemyCount;
    public int rounds;
    [Header("User Interface")]
    public TextMeshProUGUI countdownTxt;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI endRoundText;
    private void Update()
    {
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        countdownTxt.text = Mathf.Round(countdown).ToString();

    }
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        if(EnemyCount > 1)
        {
            StartCoroutine(SpawnBlueEnemy(waveIndex + 1));
        }
        waveIndex++;
        rounds++;
        if (!PlayerStats.gameOver)
        {
            roundText.text = "Round: " + rounds.ToString();
            endRoundText.text = rounds.ToString();
        }
    }
    void SpawnEnemy()
    {
        EnemyCount++;
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    IEnumerator SpawnBlueEnemy(int count)
    {
        int enemies = 0;
        while(enemies < count)
        {
            enemies++;
            Instantiate(blueEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

    }

}
