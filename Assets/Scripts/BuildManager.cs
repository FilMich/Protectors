using UnityEngine;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;

	[Header("UI References")]
	public Transform canvasTransform; // Assign the Canvas transform in the Inspector
	public GameObject currentPopup;   // Tracks the currently active popup

	[Header("Turret Prefabs")]
	public GameObject defaultTurretPrefab; // Default turret prefab
	public GameObject advancedTurretPrefab; // Optional advanced turret prefab

	private Dictionary<string, GameObject> turretPrefabs; // Dictionary to store turret types

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in the scene!");
			Destroy(gameObject);
			return;
		}
		instance = this;
		Debug.Log("BuildManager instance initialized.");
	}

	private void Start()
	{
		// Initialize the turret prefabs dictionary
		turretPrefabs = new Dictionary<string, GameObject>
		{
			{ "BasicTurret", defaultTurretPrefab },
			{ "AdvancedTurret", advancedTurretPrefab }
		};

		Debug.Log("Turret prefabs initialized.");
	}

	public GameObject GetTurretPrefab(string turretType)
	{
		Debug.Log($"Looking for turret type: {turretType}");

		if (turretPrefabs == null)
		{
			Debug.LogError("Turret prefabs dictionary is not initialized!");
			return null;
		}

		if (turretPrefabs.ContainsKey(turretType))
		{
			Debug.Log($"Found turret prefab for type: {turretType}");
			return turretPrefabs[turretType];
		}

		Debug.LogError($"Turret type {turretType} not found in the dictionary!");
		return null;
	}
}
