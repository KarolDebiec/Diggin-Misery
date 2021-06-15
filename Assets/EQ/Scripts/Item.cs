using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "New Item";	// Name of the item
	public Sprite icon = null;				// Item icon
	public bool showInInventory = true;
	public bool isStackable;
	public GameObject prefab;
	public bool isPlaceable = false;
	// Called when the item is pressed in the inventory
	public virtual void Use ()
	{
		// Use the item
		// Something may happen
	}
	public virtual void Place()
	{

	}
	public virtual void StopAction() // stops any action that is meant to be used like placing object or building
	{

	}
	public virtual void Pick() // picks object to hand
	{
		PlayerController playerController = (GameObject.FindGameObjectWithTag("Player")).GetComponent<PlayerController>();
		playerController.ChangeObjectInHand(this);
	}
	// Call this method to remove the item from inventory
	/*public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}*/

}
