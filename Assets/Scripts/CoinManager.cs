using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
	public static CoinManager instance;

	[Header("Coin Settings")]
	public int startingCoins = 100;

	[Header("UI References")]
	public TextMeshProUGUI coinText;

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
		UpdateCoinUI();
	}

	public bool SpendCoins(int amount)
	{
		if (currentCoins >= amount)
		{
			currentCoins -= amount;
			Debug.Log($"Spent {amount} coins. Remaining coins: {currentCoins}");
			UpdateCoinUI();
			return true;
		}
		Debug.LogError("Not enough coins!");
		return false;
	}

	public void AddCoins(int amount)
	{
		currentCoins += amount;
		Debug.Log($"Added {amount} coins. Total coins: {currentCoins}");
		UpdateCoinUI();
	}

	public int GetCoins()
	{
		return currentCoins;
	}
	private void UpdateCoinUI()
	{
		if (coinText != null)
		{
			coinText.text = $"Coins: {currentCoins}";
		}
		else
		{
			Debug.LogError("Coin Text UI is not assigned!");
		}
	}

}
