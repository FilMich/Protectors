using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	public Transform enemyPrefab;

	public TextMeshProUGUI countdownText;

	public Transform spawnPoint;

	private float timer = 5f; 
	private float countdown = 2f; 

	private int numberOfEnemies = 0; 
	private int enemiesSpawned = 0; 

	private float spawnDelay = 0.5f; 
	private float spawnTimer = 0f; 

	
	void Update()
	{

		if (enemiesSpawned < numberOfEnemies)
		{
			spawnTimer -= Time.deltaTime;

			if (spawnTimer <= 0f)
			{
				SpawnEnemy();
				enemiesSpawned++;
				spawnTimer = spawnDelay;
			}
		}

		if (countdown <= 0f)
		{
			enemiesSpawned = 0;
			countdown = timer;
			numberOfEnemies++;
			spawnTimer = 0f;
		}
		else
		{
			countdown -= Time.deltaTime;
			countdownText.text = Mathf.Round(countdown).ToString();
		}

		
		
	}

	void SpawnEnemy()
	{
		Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}
