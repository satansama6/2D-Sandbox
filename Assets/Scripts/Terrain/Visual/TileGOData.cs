﻿using System.Collections;
using System.Collections.Generic;
using Terrain.Data;
using UnityEngine;

namespace Terrain.Visuals
{
  public class TileGOData : MonoBehaviour
  {
    public TileType type;
    public int durability;
    public bool isBreakable = true;

    public bool isMaskable = true;

    [HideInInspector]
    public bool isDirty;

    public List<TileType> itemDrop = new List<TileType>();

    [HideInInspector]
    public GameObject inventory;

    protected virtual void Start()
    {
    }

    //-----------------------------------------------------------------------------------------------------------//

    /// <summary>
    /// We call this function whenever a block gets clickedOn(mined)
    /// </summary>
    /// <param name="amount"> Mining strenght, reduce durability by this amount </param>
    /// <returns> True if tile got mined </returns>
    public virtual bool Mine(int amount)
    {
      if (isBreakable)
      {
        durability -= amount;
        if (durability <= 0)
        {
          WorldGeneration.m_Terrain.SetTileAt((int)transform.position.x, (int)transform.position.y, 0);
          WorldLoader.m_Terrain.SetTileAt((int)transform.position.x, (int)transform.position.y, 0);
          foreach (TileType type in TileDatabase.sharedInstance.FetchTileByID(type).itemDrop)
          {
            InventoryPanel.sharedInstance.AddItem(type, 1);
          }
          gameObject.SetActive(false);
          return true;
        }
      }
      return false;
    }

    //-----------------------------------------------------------------------------------------------------------//

    public virtual void Place()
    {
    }

    //-----------------------------------------------------------------------------------------------------------//

    /// <summary>
    /// If the inventory is active we deactive it
    /// If the inventory is inactive we active it
    /// </summary>
    public virtual void ShowInventory()
    {
      if (inventory != null)
      {
        if (inventory.activeInHierarchy)
        {
          inventory.SetActive(false);
          return;
        }
        inventory.SetActive(true);
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    #region NeighbourTiles

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public TileGOData UpLeft()
    {
      GameObject GO = WorldLoader.m_Terrain.GetTileAt((int)transform.position.x - 1, (int)transform.position.y + 1);
      if (GO != null)
      {
        return GO.GetComponent<TileGOData>();
      }
      else
      {
        return null;
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    public TileGOData Up()
    {
      GameObject GO = WorldLoader.m_Terrain.GetTileAt((int)transform.position.x, (int)transform.position.y + 1);
      if (GO != null)
      {
        return GO.GetComponent<TileGOData>();
      }
      else
      {
        return null;
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    public TileGOData UpRight()
    {
      GameObject GO = WorldLoader.m_Terrain.GetTileAt((int)transform.position.x + 1, (int)transform.position.y + 1);
      if (GO != null)
      {
        return GO.GetComponent<TileGOData>();
      }
      else
      {
        return null;
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    public TileGOData Left()
    {
      GameObject GO = WorldLoader.m_Terrain.GetTileAt((int)transform.position.x - 1, (int)transform.position.y);
      if (GO != null)
      {
        return GO.GetComponent<TileGOData>();
      }
      else
      {
        return null;
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    public TileGOData Right()
    {
      GameObject GO = WorldLoader.m_Terrain.GetTileAt((int)transform.position.x + 1, (int)transform.position.y);
      if (GO != null)
      {
        return GO.GetComponent<TileGOData>();
      }
      else
      {
        return null;
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    public TileGOData DownLeft()
    {
      GameObject GO = WorldLoader.m_Terrain.GetTileAt((int)transform.position.x - 1, (int)transform.position.y - 1);
      if (GO != null)
      {
        return GO.GetComponent<TileGOData>();
      }
      else
      {
        return null;
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    public TileGOData Down()
    {
      GameObject GO = WorldLoader.m_Terrain.GetTileAt((int)transform.position.x, (int)transform.position.y - 1);
      if (GO != null)
      {
        return GO.GetComponent<TileGOData>();
      }
      else
      {
        return null;
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    public TileGOData DownRight()
    {
      GameObject GO = WorldLoader.m_Terrain.GetTileAt((int)transform.position.x + 1, (int)transform.position.y - 1);
      if (GO != null)
      {
        return GO.GetComponent<TileGOData>();
      }
      else
      {
        return null;
      }
    }

    #endregion NeighbourTiles
  }
}