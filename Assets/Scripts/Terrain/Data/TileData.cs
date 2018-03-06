using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain.Data
{
  [System.Serializable]
  public class TileData
  {
    public ushort id;

    public TileData(ushort id)
    {
      this.id = id;
    }
  }
}