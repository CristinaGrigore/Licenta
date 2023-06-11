using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public class EquipmentSO : ItemSO
{
	public EquipmentSlot slot;
	public SkinnedMeshRenderer mesh;
	public int armorModifier;
	public int damageModifier;
	public bool isArmor = false;

	public override void Use()
	{
		base.Use();
		//Equip the Item
		EquipmentManager.instance.Equip(this);
		//Remove from inventory
		RemoveFromInventory();
	}
	
}

public enum EquipmentSlot
{
	Head,
	Chest,
	Legs,
	Weapon,
	Shield,
	Feet
}