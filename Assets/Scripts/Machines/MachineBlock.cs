using System.Collections;
using System.Collections.Generic;
using Terrain.Visuals;
using UnityEngine;

public class MachineBlock : TileGOData
{
  // [HideInInspector]
  public bool isCoreless = true;

  public MachineBlock core;

  public List<TileType> allowedCores = new List<TileType>();

  public virtual void Interact()
  {
  }

  #region NotCore

  public void CheckForCore()
  {
    CheckDirections();
    isCoreless = false;
  }

  private void CheckDirections()
  {
    if (allowedCores.Contains(Up().type) || Up().GetComponent<MachineBlock>() != null && !Up().GetComponent<MachineBlock>().isCoreless)
    {
      core = Up().GetComponent<MachineBlock>().core;
      isCoreless = false;
    }
    if (allowedCores.Contains(Left().type) || Left().GetComponent<MachineBlock>() != null && !Left().GetComponent<MachineBlock>().isCoreless)
    {
      core = Left().GetComponent<MachineBlock>().core;
      isCoreless = false;
    }
    if (allowedCores.Contains(Down().type) || Down().GetComponent<MachineBlock>() != null && !Down().GetComponent<MachineBlock>().isCoreless)
    {
      core = Down().GetComponent<MachineBlock>().core;
      isCoreless = false;
    }
    if (allowedCores.Contains(Right().type) || Right().GetComponent<MachineBlock>() != null && !Right().GetComponent<MachineBlock>().isCoreless)
    {
      core = Right().GetComponent<MachineBlock>().core;
      isCoreless = false;
    }
  }

  #endregion NotCore

  #region Core

  public void SetNeighboursToIsCoreless()
  {
    if (Up().GetComponent<MachineBlock>() != null && Up().GetComponent<MachineBlock>().isCoreless == false)
    {
      Up().GetComponent<MachineBlock>().SetNeighboursToIsCoreless();
    }
    if (Left().GetComponent<MachineBlock>() != null && Left().GetComponent<MachineBlock>().isCoreless == false)
    {
      Left().GetComponent<MachineBlock>().SetNeighboursToIsCoreless();
    }
    if (Down().GetComponent<MachineBlock>() != null && Down().GetComponent<MachineBlock>().isCoreless == false)
    {
      Down().GetComponent<MachineBlock>().SetNeighboursToIsCoreless();
    }
    if (Right().GetComponent<MachineBlock>() != null && Right().GetComponent<MachineBlock>().isCoreless == false)
    {
      Right().GetComponent<MachineBlock>().SetNeighboursToIsCoreless();
    }

    if (Up().GetComponent<MachineBlock>() != null)
    {
      Up().GetComponent<MachineBlock>().isCoreless = true;
    }
    if (Left().GetComponent<MachineBlock>() != null)
    {
      Left().GetComponent<MachineBlock>().isCoreless = true;
    }
    if (Down().GetComponent<MachineBlock>() != null)
    {
      Down().GetComponent<MachineBlock>().isCoreless = true;
    }
    if (Right().GetComponent<MachineBlock>() != null)
    {
      Right().GetComponent<MachineBlock>().isCoreless = true;
    }
  }

  public void CheckForCoreInNeighbours()
  {
    CheckDirections();

    if (Up().GetComponent<MachineBlock>() != null && Up().GetComponent<MachineBlock>().isCoreless == true)
    {
      Up().GetComponent<MachineBlock>().CheckForCoreInNeighbours();
    }
    if (Left().GetComponent<MachineBlock>() != null && Left().GetComponent<MachineBlock>().isCoreless == true)
    {
      Left().GetComponent<MachineBlock>().CheckForCoreInNeighbours();
    }
    if (Down().GetComponent<MachineBlock>() != null && Down().GetComponent<MachineBlock>().isCoreless == true)
    {
      Down().GetComponent<MachineBlock>().CheckForCoreInNeighbours();
    }
    if (Right().GetComponent<MachineBlock>() != null && Right().GetComponent<MachineBlock>().isCoreless == true)
    {
      Right().GetComponent<MachineBlock>().CheckForCoreInNeighbours();
    }
  }

  #endregion Core
}