using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain.Visuals;
using Terrain.Data;

public class WaterTank : TileGOData
{
  private float currentWaterAmount = 0;
  private float maxWaterAmount = 100;

  public GameObject waterFill;

  // TODO: do we want more than 1 core for 1 water tank
  private WaterMachineCore core;

  public bool AddWater(float amount)
  {
    if (currentWaterAmount + amount < maxWaterAmount)
    {
      currentWaterAmount += amount;
      waterFill.transform.localScale = new Vector3(1, currentWaterAmount / maxWaterAmount, 1);
      return true;
    }
    return false;
  }

  //TODO REFACTORING!!!
  public override void Place()
  {
    if (Up().type == TileType.WaterMachineCore)
    {
      //&& Up().GetComponent<WaterTank>().core
      Up().GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Up().GetComponent<WaterMachineCore>();
      return;
    }

    if (Up().type == TileType.WaterTank)
    {
      Up().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Up().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>();
      return;
    }

    if (Left().type == TileType.WaterMachineCore)
    {
      Left().GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Left().GetComponent<WaterMachineCore>();
      return;
    }

    if (Left().type == TileType.WaterTank)
    {
      Left().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Left().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>();
      return;
    }

    if (Down().type == TileType.WaterMachineCore)
    {
      Down().GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Down().GetComponent<WaterMachineCore>();
      return;
    }

    if (Down().type == TileType.WaterTank)
    {
      Down().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Down().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>();
      return;
    }

    if (Right().type == TileType.WaterMachineCore)
    {
      Right().GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Right().GetComponent<WaterMachineCore>();
      return;
    }

    if (Right().type == TileType.WaterTank)
    {
      Right().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Right().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>();
      return;
    }
  }
}