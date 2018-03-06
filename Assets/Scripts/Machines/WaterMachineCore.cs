using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain.Visuals;
using Terrain.Data;

public class WaterMachineCore : TileGOData, IInteractable
{
  private int dirtCount = 0;
  private float dirtPercentage = 0;

  private InventorySlot dirtSlot;
  private InventorySlot sandSlot;

  public List<WaterTank> waterTanks = new List<WaterTank>();

  protected override void Start()
  {
  }

  public void Interact()
  {
    // TODO: Do it when you put items in the machine slot
    if (dirtSlot.ItemGO != null)
    {
      dirtCount = dirtSlot.Item.itemCount;

      dirtPercentage += 0.5f;

      if (dirtPercentage >= 100)
      {
        dirtPercentage = 0;
        dirtCount--;
        dirtSlot.UpdateItemCount(-1);
        sandSlot.AddItem(TileType.Sand, 1);
      }
      if (waterTanks.Count == 0)
      {
        // We need to show a warning message to the player that he needs to place a water tank near the core
        Debug.LogWarning("No water tank");
      }
      foreach (WaterTank _waterTank in waterTanks)
      {
        if (_waterTank.AddWater(0.1f))
        {
          return;
        }
        else
        {
          continue;
        }
      }
    }
  }

  public override void ShowInventory()
  {
    if (inventory == null)
    {
      inventory = InventoryUtility.sharedInstance.CreateGenericInventory(2);
      dirtSlot = inventory.transform.GetChild(1).transform.GetChild(0).GetComponent<InventorySlot>();
      sandSlot = inventory.transform.GetChild(1).transform.GetChild(1).GetComponent<InventorySlot>();
      inventory.SetActive(false);
    }
    base.ShowInventory();
  }

  public override void Mine(int amount)
  {
    base.Mine(amount);
  }
}