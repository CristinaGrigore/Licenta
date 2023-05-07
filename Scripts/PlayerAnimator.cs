using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : AnimationControoler
{
	public WeaponAnimation[] weaponAnimations;
	Dictionary<EquipmentSO, AnimationClip[]> weaponAnimationsDictionary;

	protected override void Start()
	{
		base.Start();
		EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

		weaponAnimationsDictionary = new Dictionary<EquipmentSO, AnimationClip[]>();
		foreach(WeaponAnimation a in weaponAnimations)
		{
			weaponAnimationsDictionary.Add(a.weapon, a.clips);
		}
	}
	void OnEquipmentChanged(EquipmentSO newItem, EquipmentSO oldItem)
	{
		if(newItem != null && newItem.slot == EquipmentSlot.Weapon)
		{
			//equip sword or shield or both
			animator.SetLayerWeight(1, 1);
			if(weaponAnimationsDictionary.ContainsKey(newItem))
			{
				currentAttackAnimationSet = weaponAnimationsDictionary[newItem];
			}
		}
		if (newItem != null && newItem.slot == EquipmentSlot.Shield)
		{
			//equip shield
			animator.SetLayerWeight(2, 1);
		}
		if(newItem == null && oldItem != null && oldItem.slot == EquipmentSlot.Weapon)
		{
			//unequip sword or shield or both
			animator.SetLayerWeight(1, 0);
			currentAttackAnimationSet = defaultAttackAnimationSet;
		}
		if (newItem == null && oldItem != null && oldItem.slot == EquipmentSlot.Shield)
		{
			//unequip sword or shield or both
			animator.SetLayerWeight(2, 0);
		}

	}

	[System.Serializable]
	public struct WeaponAnimation
	{
		public EquipmentSO weapon;
		public AnimationClip[] clips;
	}
 
} 
