using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemCharacteristics itemAssigned;
	//To detect if this item is highlighted
	public Button slotButton;
	public Image myImage;
	public Image overlay;
	private void Update()
	{
		if (itemAssigned != null)
		{
			myImage.sprite = itemAssigned.icon;
			myImage.color = Color.white;
		}
		else
		{
			myImage.sprite = null;
			myImage.color = Color.clear;
		}
	}
}
