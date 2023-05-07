using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region Singleton
	// singleton
	public static Inventory instance;

	void Awake()
	{
		if(instance != null)
		{
			Debug.LogWarning("More than one instance of Inventory found");
			return;
		}

		instance = this;
	}
	#endregion
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 20;
	public List<ItemSO> items = new List<ItemSO>();

	public bool Add (ItemSO item)
	{
		if(!item.isDefaultItem)
		{
			if(items.Count >= space)
			{
				Debug.Log("Not enough room");
				return false;
			}
			items.Add(item);
			//trigger UI to update Inventory gfx (check if there are methods subscribed to
			//this callback
			if(onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}
		return true;
	}

	public void Remove (ItemSO item)
	{
		items.Remove(item);
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}
}
