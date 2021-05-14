using UnityEngine;

/* An Item that can be consumed. So far that just means regaining health */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumable")]
public class Consumable : Item {

	public int healthGain;      // How much health?
	public int staminaRepletion;
	public int hungerReduction;
	public int thirstReduction;
	// This is called when pressed in the inventory
	public override void Use()
	{
		PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		// Heal the player
		//PlayerStats playerStats = Player.instance.playerStats;
		//playerStats.Heal(healthGain);
		playerController.AddHealth(healthGain);
		playerController.AddStamina(staminaRepletion);
		playerController.ReduceHunger(hungerReduction);
		playerController.ReduceThirst(thirstReduction);
		Debug.Log(name + " consumed!");
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>().Remove(this);
		//RemoveFromInventory();	// Remove the item after use
	}

}
