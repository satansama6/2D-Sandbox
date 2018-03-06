using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain.Data;

namespace Terrain.Visuals
{
  public class ChunkVisual : MonoBehaviour
  {
    public GameObject[,] tileGO = new GameObject[16, 16];
    public GameObject[,] maskOutlineGO = new GameObject[16, 16];

    //-----------------------------------------------------------------------------------------------------------//

    public void DrawChunk(TileData[,] _tiles)
    {
      GameObject _TileType = null;
      for (int _x = 0; _x < ChunkData.m_Size; _x++)
      {
        for (int _y = 0; _y < ChunkData.m_Size; _y++)
        {
          _TileType = TileDatabase.sharedInstance.FetchTileByID(_tiles[_x, _y].type).gameObject;
          _TileType = _TileType.GetComponent<PooledMonobehaviour>().Get<PooledMonobehaviour>().gameObject;

          _TileType.transform.position = new Vector3(transform.position.x + _x, transform.position.y + _y, 0);

          _TileType.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
            TileSpriteManager.sharedInstance.GetTileForPosition(_x, _y, _tiles[_x, _y].type);

          _TileType.transform.parent = transform;

          tileGO[_x, _y] = _TileType;

          WorldLoader.m_Terrain.dirtyTiles.Enqueue(_TileType.GetComponent<TileGOData>());
        }
      }

      // here we need to dirty 1 more row/coloumn of tiles around this chunk
    }

    //-----------------------------------------------------------------------------------------------------------//

    public GameObject GetTileAt(int x, int y)
    {
      return tileGO[x, y];
    }

    //-----------------------------------------------------------------------------------------------------------//

    public void SetTileAt(int x, int y, TileType type)
    {
      GameObject _TileType = null;
      _TileType = TileDatabase.sharedInstance.FetchTileByID(type).gameObject;
      _TileType = _TileType.GetComponent<PooledMonobehaviour>().Get<PooledMonobehaviour>().gameObject;
      _TileType.transform.position = new Vector3(x + transform.position.x, y + transform.position.y, 0);
      tileGO[x, y] = _TileType;
      _TileType.transform.parent = transform;

      _TileType.GetComponent<TileGOData>().Place();
    }
  }
}