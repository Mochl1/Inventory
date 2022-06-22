using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryController : MonoBehaviour
{
	 DefaultInputActionsPlus nInput;
	public List<ItemCharacteristics> itemsAvailable;

	public int maxItemsPlaced;
	//UI
	public InventorySlot[] panelInventorySlots;
	public TextMeshProUGUI itemNameText;

	public bool IsitemGrabbed;
	public ItemCharacteristics itemGrabbed;
	public InventorySlot slotGrabbed;
	public ItemCharacteristics itemSelected;

	public Resolution currentResolution;

	public AudioSource audioS;
	public AudioClip clickAudio;
	private void Awake()
	{
		nInput = new DefaultInputActionsPlus();
		//Call first add items
		AddItems();
	}
	private void Update()
	{
		if (EventSystem.current.currentSelectedGameObject.GetComponent<InventorySlot>().itemAssigned != null)
		{
			itemSelected = EventSystem.current.currentSelectedGameObject.GetComponent<InventorySlot>().itemAssigned;
		}
		else
		{
			itemSelected = null;
		}
		if (itemSelected != null)
		{
			itemNameText.text = itemSelected.ItemName;
		}
		else
		{
			itemNameText.text = "";
		}
		nInput.Player.Restart.performed += ctx => ClearAndAddItems();
		nInput.Player.R2.performed += ctx => R2Pressed();
		nInput.Player.L2.performed += ctx => L2Pressed();
		nInput.Player.L1.performed += ctx => L1Pressed();

	}
	//Remove all items in the inventory
	public void ClearAndAddItems()
	{
		audioS.Play();
		if (IsitemGrabbed)
		{
			slotGrabbed.itemAssigned = null;
			itemGrabbed = null;
			IsitemGrabbed = false;
			slotGrabbed = null;
			return;
		}
		else
		{
			foreach (InventorySlot slot in panelInventorySlots)
			{
				if (slot.itemAssigned != null)
				{
					slot.itemAssigned = null;
				}
			}
			AddItems();
		}
	}
	//Add random items in the inventory
	public void AddItems()
	{
		audioS.Play();
		int counter = 0;
		foreach (InventorySlot slot in panelInventorySlots)
		{
			if (counter < maxItemsPlaced)
			{
				int randomNumber = Random.Range(0, 3);
				if (randomNumber == 0 || randomNumber == 1)
				{
					if (slot.itemAssigned == null)
					{
						slot.itemAssigned = itemsAvailable[Random.Range(0, 55)];
						counter++;
					}
				}
			}
		}
	}
	public void GrabItem(InventorySlot slot)
	{
		audioS.Play();
		if (IsitemGrabbed)
		{
			InventorySlot slotNuevo = EventSystem.current.currentSelectedGameObject.GetComponent<InventorySlot>();
			if (slotNuevo.itemAssigned != null)
			{
				slotGrabbed.itemAssigned = slotNuevo.itemAssigned;
			}
			else
			{
				slotGrabbed.itemAssigned = null;
			}
			slotNuevo.itemAssigned = itemGrabbed;
			itemGrabbed = null;
			IsitemGrabbed = false;
			slotGrabbed = null;
			return;
		}
		else
		{
			if (slot.itemAssigned != null)
			{
				itemGrabbed = slot.itemAssigned;
				slotGrabbed = slot;
				IsitemGrabbed = true;
				return;
			}
		}
	}

	public void R2Pressed()
	{
		audioS.Play();
		if (currentResolution.height == 1280)
		{
			Screen.SetResolution(1920, 1080, true);
		}
		if (currentResolution.height == 1920)
		{
			Screen.SetResolution(3860, 2160, true);
		}
		if (currentResolution.height == 3860)
		{
			Screen.SetResolution(1280, 720, true);
		}
	}
	public void L2Pressed()
	{
		audioS.Play();
		if (currentResolution.height == 1280)
		{
			Screen.SetResolution(3860, 2160, true);
		}
		if (currentResolution.height == 1920)
		{
			Screen.SetResolution(1280, 720, true);
		}
		if (currentResolution.height == 3860)
		{
			Screen.SetResolution(1920, 1080, true);
		}
	}
	public void L1Pressed()
	{
		Application.Quit();
	}

	private void OnEnable()
	{
		nInput.Player.Enable();
	}
}
