using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpriteManager : MonoBehaviour
{
  public static TileSpriteManager sharedInstance;

  private void Awake()
  {
    sharedInstance = this;
  }

  public Sprite[] dirtTiles = new Sprite[64];
  public Sprite[] stoneTiles = new Sprite[64];
  public Sprite[] snowTiles = new Sprite[64];

  public Sprite snow;

  public Sprite sand;

  public Sprite iron;

  public Sprite gold;

  public Sprite copper;

  public Sprite waterMachineCore;

  public Sprite waterTank;

  public Sprite quarryCore;

  public Sprite manualLever;

  public Sprite plantIncubatorCore;

  public Sprite plantHolder;

  public Sprite furnace;

  public List<Sprite> mask = new List<Sprite>();
  public List<Sprite> outline = new List<Sprite>();

  public Sprite GetTileForPosition(int x, int y, TileType type)
  {
    x = x % 8;
    y = y % 8;

    if (type == TileType.Dirt)
    {
      return dirtTiles[(56 - y * 8) + x];
    }

    if (type == TileType.Stone)
    {
      return stoneTiles[(56 - y * 8) + x];
    }
    if (type == TileType.Sand)
    {
      return sand;
    }
    if (type == TileType.Snow)
    {
      return snowTiles[(56 - y * 8) + x];
    }
    if (type == TileType.Iron)
    {
      return iron;
    }

    if (type == TileType.Gold)
    {
      return gold;
    }

    if (type == TileType.Copper)
    {
      return copper;
    }
    if (type == TileType.WaterMachineCore)
    {
      return waterMachineCore;
    }
    if (type == TileType.WaterTank)
    {
      return waterTank;
    }

    if (type == TileType.QuarryCore)
    {
      return quarryCore;
    }

    if (type == TileType.ManualLever)
    {
      return manualLever;
    }

    if (type == TileType.PlantIncubatorCore)
    {
      return plantIncubatorCore;
    }

    if (type == TileType.PlantHolder)
    {
      return plantHolder;
    }

    if (type == TileType.Furnace)
    {
      return furnace;
    }

    if (type == TileType.Empty)
    {
      return null;
    }

    Debug.LogWarning("Can not find sprite for tile: " + type);
    return null;
  }

  public int GetMaskIntTL(int maskValue)
  {
    switch (maskValue)
    {
      case 0: return 12;
      case 1: return 7;
      case 2: return 6;
      case 3: return 9;
      case 8: return 5;
      case 9: return 11;
      case 10: return 14;
      case 11: return 15;
      default:
        Debug.LogWarning("Bitmask value not implemented in TL: " + maskValue);
        return -1;
    }
  }

  public int GetMaskIntTR(int maskValue)
  {
    switch (maskValue)
    {
      case 0: return 12;
      case 2: return 7;
      case 4: return 6;
      case 6: return 9;
      case 16: return 4;
      case 18: return 13;
      case 20: return 10;
      case 22: return 1;
      default:
        Debug.LogWarning("Bitmask value not implemented in TR: " + maskValue);
        return -1;
    }
  }

  public int GetMaskIntBL(int maskValue)
  {
    switch (maskValue)
    {
      case 0: return 12;
      case 8: return 7;
      case 32: return 5;
      case 40: return 11;
      case 64: return 4;
      case 72: return 13;
      case 96: return 8;
      case 104: return 2;

      default:
        Debug.LogWarning("Bitmask value not implemented in BL: " + maskValue);
        return -1;
    }
  }

  public int GetMaskIntBR(int maskValue)
  {
    switch (maskValue)
    {
      case 0: return 12;
      case 16: return 6;
      case 64: return 5;
      case 80: return 14;
      case 128: return 4;
      case 144: return 10;
      case 192: return 8;
      case 208: return 3;
      default:
        Debug.LogWarning("Bitmask value not implemented: " + maskValue);
        return -1;
    }
  }
}