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

  public List<Sprite> mask = new List<Sprite>();
  public List<Sprite> outline = new List<Sprite>();

  public Sprite GetTileForPosition(int x, int y, ushort id)
  {
    x = x % 8;
    y = y % 8;

    if (id == 1)
    {
      return dirtTiles[(56 - y * 8) + x];
    }
    if (id == 2)
    {
      return stoneTiles[(56 - y * 8) + x];
    }
    if (id == 4)
    {
      return sand;
    }
    if (id == 5)
    {
      return snowTiles[(56 - y * 8) + x];
    }
    if (id == 6)
    {
      return iron;
    }

    if (id == 7)
    {
      return gold;
    }

    if (id == 8)
    {
      return copper;
    }
    if (id == 150)
    {
      return waterMachineCore;
    }
    if (id == 151)
    {
      return waterTank;
    }
    if (id == 0)
    {
      return null;
    }

    Debug.LogWarning("Can not find sprite for tile: " + id);
    return null;
  }

  public Sprite GetOutline(int bitmaskValue)
  {
    switch (bitmaskValue)
    {
      case 0: return null;
      case 1: return outline[0];
      case 2: return null;// return outline[4];
      case 3: return outline[4];
      case 4: return outline[3];
      case 6: return outline[4];
      case 8: return null;// return outline[5];
      case 9: return outline[5];
      case 10: return null;// outline[2];
      case 11: return outline[2];
      case 16: return null;// return outline[5];
      case 20: return outline[5];
      case 18: return outline[1];
      case 22: return outline[1];
      case 32: return null;
      case 40: return outline[5];
      case 64: return null;// return outline[4];
      case 72: return outline[3];
      case 80: return null;// outline[0];
      case 96: return outline[4];
      case 104: return outline[3];
      case 128: return null;
      case 144: return outline[5];
      case 192: return outline[4];
      case 208: return outline[0];

      default:
        Debug.Log("Bitmask value not implemented: " + bitmaskValue);
        return null;
    }
  }

  public Sprite GetMask(int maskValue)
  {
    switch (maskValue)
    {
      case 0: return mask[12];
      case 1: return mask[4];
      case 2: return null;// mask[12];
      case 3: return mask[9];
      case 4: return mask[7];
      case 6: return mask[9];
      case 8: return null;// mask[12];
      case 9: return mask[11];
      case 10: return mask[2];
      case 11: return mask[2];
      case 16: return null;// mask[12];
      case 18: return mask[1];
      case 20: return mask[10];
      case 22: return mask[1];
      case 32: return mask[5];
      case 40: return mask[11];
      case 64: return null;// mask[12];
      case 72: return mask[3];
      case 80: return mask[0];
      case 96: return mask[8];
      case 104: return mask[3];
      case 128: return mask[6];
      case 144: return mask[10];
      case 192: return mask[8];
      case 208: return mask[0];

      default:
        Debug.LogWarning("Bitmask value not implemented: " + maskValue);
        return null;
    }
  }
}