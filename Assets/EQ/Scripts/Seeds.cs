using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Seeds")]
public class Seeds : Item {

	public float growthSpeed;
	public float waterNeeded;     // water needed for plant to not die
	public float timeToWither;
	public float amountToHarvest;
	public GameObject[] growthSteps; // is withered and from there go phases like 1-little to x-full grown
	public Item itemToHarvest;
	public override void Use()
	{
		
	}

}

