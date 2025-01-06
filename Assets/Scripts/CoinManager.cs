using UnityEngine;

public class CoinManager : MonoBehaviour
{
	public static CoinManager instance;

	public int startingCoins = 100; // Starting balance
	private int currentCoins;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one CoinManager in the scene!");
			return;
		}
		instance = this;
		currentCoins = startingCoins;
		Debug.Log($"Starting coins: {currentCoins}");
	}

	public bool SpendCoins(int amount)
	{
		if (currentCoins >= amount)
		{
			currentCoins -= amount;
			Debug.Log($"Spent {amount} coins. Remaining coins: {currentCoins}");
			return true;
		}
		Debug.LogError("Not enough coins!");
		return false;
	}

	public void AddCoins(int amount)
	{
		currentCoins += amount;
		Debug.Log($"Added {amount} coins. Total coins: {currentCoins}");
	}

	public int GetCoins()
	{
		return currentCoins;
	}
}
