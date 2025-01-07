using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Node : MonoBehaviour
{
	[Header("Node Settings")]
	public Color hoverColor;           // Color to highlight the node on hover
	public Vector2 offset = new Vector2(0, 50); // Offset for popup UI

	[Header("References")]
	public GameObject popupUIprefab;   // Assign the popup UI prefab in the Inspector
	public GameObject upgradeSellPopupUIprefab;

	private Renderer rend;
	private Color startColor;
	private GameObject turret;         // Tracks the turret on this node
	private BuildManager buildManager;
	private CoinManager coinManager;

	void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;

		// Get reference to the BuildManager instance
		buildManager = BuildManager.instance;
		coinManager = CoinManager.instance;
		if (buildManager == null)
		{
			Debug.LogError("BuildManager instance not found!");
		}
		if (coinManager == null)
		{
			Debug.LogError("CoinManager instance not found!");
		}
	}

	private void OnMouseDown()
	{
		Debug.Log("Node clicked!");

		if (turret != null)
		{
			ShowUpgradeSellPopup();
			Debug.Log("Node already has a turret.");
			return;
		}

		ShowPopup();
	}

	private void ShowPopup()
	{
		if (buildManager.currentPopup != null)
		{
			Destroy(buildManager.currentPopup); // Close any existing popup
		}

		GameObject popup = Instantiate(popupUIprefab, buildManager.canvasTransform);

		// Position the popup
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		popup.GetComponent<RectTransform>().anchoredPosition = screenPosition + offset;

		buildManager.currentPopup = popup;

		// Loop through all children and assign listeners
		Button[] buttons = popup.GetComponentsInChildren<Button>(); // Finds all buttons in the hierarchy
		foreach (Button button in buttons)
		{
			string turretType = button.name; // Ensure button names are meaningful (e.g., "BasicTurret")
			Debug.Log($"Assigning listener for button: {turretType}");
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => OnPopupButtonClick(turretType));
		}
	}


	public void OnPopupButtonClick(string turretType)
	{
		Debug.Log($"OnPopupButtonClick triggered for turretType: {turretType}");

		if (buildManager == null)
		{
			Debug.LogError("BuildManager is null!");
			return;
		}

		GameObject turretToBuild = buildManager.GetTurretPrefab(turretType);
		if (turretToBuild == null)
		{
			Debug.LogError($"Failed to build turret. No prefab found for type: {turretType}");
			return;
		}

		int turretCost = turretToBuild.GetComponent<Turret>().cost;

		if (CoinManager.instance.SpendCoins(turretCost))
		{
			// Place the turret
			turret = Instantiate(turretToBuild, transform.position, Quaternion.identity);
			Debug.Log($"Turret {turretType} built successfully at {transform.position}");
		}
		else
		{
			Debug.LogError($"Not enough coins to build turret: {turretType}");
		}

		//turret = Instantiate(turretToBuild, transform.position, Quaternion.identity);
		//Debug.Log($"Turret {turretType} built successfully at {transform.position}");

		// Close the popup
		if (buildManager.currentPopup != null)
		{
			Destroy(buildManager.currentPopup);
			buildManager.currentPopup = null;
		}
	}

	private void ShowUpgradeSellPopup()
	{
		if (buildManager.currentPopup != null)
		{
			Debug.Log("Destroying existing popup.");
			Destroy(buildManager.currentPopup); // Close any existing popup
		}

		Debug.Log("Creating upgrade/sell popup.");
		GameObject popup = Instantiate(upgradeSellPopupUIprefab, buildManager.canvasTransform);

		// Position the popup near the node
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		popup.GetComponent<RectTransform>().anchoredPosition = screenPosition + offset;

		buildManager.currentPopup = popup;

		// Assign button listeners dynamically
		Button[] buttons = popup.GetComponentsInChildren<Button>();
		foreach (Button button in buttons)
		{
			if (button.name == "SellButton")
			{
				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(SellTurret);
			}
			else if (button.name == "UpgradeButton")
			{
				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(UpgradeTurret);
			}
		}
	}

	private void SellTurret()
	{
		if (turret == null)
		{
			Debug.LogError("No turret to sell!");
			return;
		}

		Turret turretComponent = turret.GetComponent<Turret>();
		int sellValue = turretComponent != null ? turretComponent.cost / 2 : 0;

		coinManager.AddCoins(sellValue);
		Debug.Log($"Sold turret for {sellValue} coins.");

		Destroy(turret);
		turret = null;

		// Close the popup
		if (buildManager.currentPopup != null)
		{
			Destroy(buildManager.currentPopup);
			buildManager.currentPopup = null;
		}
	}

	private void UpgradeTurret()
	{
		if (turret == null)
		{
			Debug.LogError("No turret to upgrade!");
			return;
		}

		Turret turretComponent = turret.GetComponent<Turret>();
		if (turretComponent == null || turretComponent.upgradedPrefab == null)
		{
			Debug.LogError("This turret cannot be upgraded!");
			return;
		}

		if (coinManager.SpendCoins(turretComponent.upgradeCost))
		{
			// Destroy the current turret and replace it with the upgraded one
			Vector3 position = turret.transform.position;
			Quaternion rotation = turret.transform.rotation;

			Destroy(turret);
			turret = Instantiate(turretComponent.upgradedPrefab, position, rotation);
			Debug.Log("Turret upgraded successfully!");
		}
		else
		{
			Debug.LogError("Not enough coins to upgrade!");
		}

		// Close the popup
		if (buildManager.currentPopup != null)
		{
			Destroy(buildManager.currentPopup);
			buildManager.currentPopup = null;
		}
	}


	private void OnMouseEnter()
	{
		rend.material.color = hoverColor;
	}

	private void OnMouseExit()
	{
		rend.material.color = startColor;
	}
}
