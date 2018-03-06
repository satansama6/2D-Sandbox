using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain.Visuals;
using Terrain.Data;

public class WaterMachineCore : TileGOData, IInteractable
{
  private int dirtCount = 10000;
  public List<WaterTank> waterTanks = new List<WaterTank>();

  protected override void Start()
  {
    inventory = GameObject.Find("WaterMachineUI");
  }

  public void Interact()
  {
    if (dirtCount > 0)
    {
      dirtCount--;

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

  public override void Mine(int amount)
  {
    base.Mine(amount);
  }
}