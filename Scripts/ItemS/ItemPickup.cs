using UnityEngine;

public class ItemPickup : Interactible
{
	public ItemSO item;
	public override void Interract()
	{
		base.Interract();

		PickUp();
	}

	void PickUp()
	{
		Debug.Log("Pick up " + item.name);

		// Add item to inventory
		bool hasBeenAdded = Inventory.instance.Add(item);
		if(hasBeenAdded)
			Destroy(gameObject);
	}
}
