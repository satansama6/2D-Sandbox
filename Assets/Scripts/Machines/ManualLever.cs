using System.Collections;
using System.Collections.Generic;
using Terrain.Visuals;
using UnityEngine;

public class ManualLever : MachineBlock, IInteractable
{
  public override void Place()
  {
    CheckForCore();
  }

  public override void Interact()
  {
    core.Interact();
  }
}