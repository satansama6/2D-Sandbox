using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain.Data;

//public enum TileTypes
//{ Empty }

[System.Serializable]
public class Item
{
  public ushort ID = 0;
  public string itemName = "Empty";
  public int stackSize = 64;
  public string slug = "Empty";
  public Sprite sprite = null;
  public bool placable = true;

  public Item()
  {
    this.ID = 0;
  }

  public Item(ushort ID, string name, int stackSize, string slug)
  {
    this.ID = ID;
    this.itemName = name;
    this.stackSize = stackSize;
    this.slug = slug;
    this.sprite = Resources.Load<Sprite>("Sprites/" + slug);
  }
}