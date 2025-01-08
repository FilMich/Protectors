using UnityEngine;
public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 10f;

    private Transform target;
    private int indexOfWaypoint = 0;

	public int maxHealth = 100; // Maximum health
	private int currentHealth;

	public int coinValue = 10; // Coins awarded when killed

	void Start()
    {
		currentHealth = maxHealth;
		target = WayPoints.waypoints[0];
		UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World); //normalization kvoli relativnej rychlosti a vzdialenosti k cielu  

        if (Vector3.Distance(transform.position, target.position) <= 0.4f) {

            if (indexOfWaypoint >= WayPoints.waypoints.Length -1) { 
                Destroy(gameObject);
                return;
            }

			indexOfWaypoint++;
            target = WayPoints.waypoints[indexOfWaypoint];

		}
    }

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		UpdateHealthBar();

		if (currentHealth <= 0)
		{
			Debug.Log("Enemy killed");
			CoinManager.instance.AddCoins(coinValue);
			Debug.Log("Coins should be added");
			Destroy(gameObject);
		}
	}

	private void UpdateHealthBar()
	{
		// Notify the health bar to update (to be implemented in Step 2)
		EnemyHealthBar healthBar = GetComponentInChildren<EnemyHealthBar>();
		if (healthBar != null)
		{
			healthBar.UpdateHealthBar(currentHealth, maxHealth);
		}
	}

}
