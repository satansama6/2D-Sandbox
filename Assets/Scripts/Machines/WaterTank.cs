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
  public WaterMachineCore core;

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
    if (Up().id == 150)
    {
      //&& Up().GetComponent<WaterTank>().core
      Up().GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Up().GetComponent<WaterMachineCore>();
      return;
    }

    if (Up().id == 151)
    {
      Up().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Up().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>();
      return;
    }

    if (Left().id == 150)
    {
      Left().GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Left().GetComponent<WaterMachineCore>();
      return;
    }

    if (Left().id == 151)
    {
      Left().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Left().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>();
      return;
    }

    if (Down().id == 150)
    {
      Down().GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Down().GetComponent<WaterMachineCore>();
      return;
    }

    if (Down().id == 151)
    {
      Down().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Down().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>();
      return;
    }

    if (Right().id == 150)
    {
      Right().GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Right().GetComponent<WaterMachineCore>();
      return;
    }

    if (Right().id == 151)
    {
      Right().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>().waterTanks.Add(this);
      core = Right().GetComponent<WaterTank>().core.GetComponent<WaterMachineCore>();
      return;
    }
  }
}