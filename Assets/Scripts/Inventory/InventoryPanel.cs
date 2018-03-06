﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TypeSafeEventManager.Events;

[RequireComponent(typeof(ItemDatabase))]
public class InventoryPanel : Inventory
{
  public static InventoryPanel sharedInstance;

  #region Prefabs

  // The visuals for the selected slot
  public GameObject inventorySelectedSlot;

  #endregion Prefabs

  private int selectedSlot = 0;
  public InventorySlot[] slots = new InventorySlot[25];

  //-----------------------------------------------------------------------------------------------------------//

  #region EventFunctions

  private void OnEnable()
  {
    EventManagerTypeSafe.AddListener<MouseEvents.OnMouseScrollEvent>(OnMouseScrollEvent);
  }

  private void OnDisable()
  {
    EventManagerTypeSafe.RemoveListener<MouseEvents.OnMouseScrollEvent>(OnMouseScrollEvent);
  }

  private void OnMouseScrollEvent(MouseEvents.OnMouseScrollEvent arg)
  {
    ChangeInventorySelectedSlot(arg.delta);
  }

  #endregion EventFunctions

  private void Awake()
  {
    // Singelton
    sharedInstance = this;
    // Get a referece for all of the slots
    // TODO Maybe: we can do something like slot number and instantiate the slots here

    int i = 0;
    foreach (Transform child in transform)
    {
      slots[i] = (child.gameObject.GetComponent<InventorySlot>());
      i++;
    }

    inventorySelectedSlot = Instantiate(inventorySelectedSlot);
    SelectInventorySlot(selectedSlot);
  }

  //-----------------------------------------------------------------------------------------------------------//

  private void Start()
  {
    ////TESTING ONLY
    //AddItem(1, 1);
    //AddItem(1, 1);
    //AddItem(1, 1);
    //AddItem(1, 1);
    //AddItem(1, 1);
    //AddItem(1, 1);
    //AddItem(1, 1);
    //AddItem(1, 1);
    //AddItem(1, 1);

    //AddItem(2, 1);
    //AddItem(2, 1);
    //AddItem(2, 1);
    //AddItem(2, 1);
    //AddItem(2, 1);

    //AddItem(2, 1);

    //AddItem(2, 1);

    //AddItem(2, 1);
    AddItem(150, 1);
    AddItem(151, 1);
    AddItem(151, 1);
    AddItem(151, 1);
  }

  //-----------------------------------------------------------------------------------------------------------//

  public void AddItem(int id, int amount)
  {
    base.AddItem(id, amount, slots);
    EventManagerTypeSafe.TriggerEvent(new UIEvents.OnItemAddedOrRemoved());
  }

  // -----------------------------------------------------------------------------------------------------------//

  public void RemoveItem(int id, int amount)
  {
    base.RemoveItem(id, amount, slots);
    EventManagerTypeSafe.TriggerEvent(new UIEvents.OnItemAddedOrRemoved());
  }

  // -----------------------------------------------------------------------------------------------------------//

  /// <summary>
  /// Move the selected slot around in the inventory
  /// </summary>
  /// <param name="delta"></param>
  private void ChangeInventorySelectedSlot(float delta)
  {
    if (delta < 0)
    {
      if (selectedSlot == slots.Length - 1)
      {
        selectedSlot = -1;
      }
      SelectInventorySlot(selectedSlot + 1);
    }
    else
    {
      if (selectedSlot == 0)
      {
        selectedSlot = slots.Length;
      }
      SelectInventorySlot(selectedSlot - 1);
    }
  }

  // -----------------------------------------------------------------------------------------------------------//

  /// <summary>
  /// Move selectedSlot image to the slotNumber slot in inventory
  /// </summary>
  /// <param name="slotNumber"></param>
  private void SelectInventorySlot(int slotNumber)
  {
    selectedSlot = slotNumber;
    inventorySelectedSlot.transform.SetParent(slots[slotNumber].transform);
    inventorySelectedSlot.transform.localPosition = new Vector3(0, 0, 0);
  }

  // -----------------------------------------------------------------------------------------------------------//

  /// <summary>
  /// Return the items ID in selectedSlot
  /// </summary>
  /// <returns> Int that represents the item ID </returns>
  public ushort GetItemInSelectedInventorySlot(bool removeItem)
  {
    if (slots[selectedSlot].ItemGO != null && slots[selectedSlot].Item.item.placable)
    {
      ushort _id = slots[selectedSlot].Item.item.ID;
      if (removeItem)
      {
        UpdateItemCount(selectedSlot, -1, slots);
        EventManagerTypeSafe.TriggerEvent(new UIEvents.OnItemAddedOrRemoved());
      }
      return _id;
    }
    else
    {
      return 0;
    }
  }

  //-----------------------------------------------------------------------------------------------------------//

  /// <summary>
  /// Returns all of the items from the player inventory
  /// </summary>
  /// <returns></returns>
  public Item[] GetItemList()
  {
    List<Item> _items = new List<Item>();
    foreach (InventorySlot slot in slots)
    {
      if (slot.ItemGO != null)
        _items.Add(slot.Item.item);
    }
    return _items.ToArray();
  }
}