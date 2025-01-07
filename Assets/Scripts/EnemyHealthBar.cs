using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
	public Slider healthSlider; // Reference to the health bar slider

	public void UpdateHealthBar(int currentHealth, int maxHealth)
	{
		healthSlider.value = (float)currentHealth / maxHealth; // Update the slider value
	}
}
