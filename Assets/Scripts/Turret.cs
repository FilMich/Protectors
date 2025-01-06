using UnityEngine;

public class Turret : MonoBehaviour
{
	[Header("Turret Costs")]
	public int cost = 50;             // The cost to place this turret
	public int upgradeCost = 100;     // The cost to upgrade this turret

	[Header("Upgrade")]
	public GameObject upgradedPrefab; // The prefab for the upgraded turret
}
