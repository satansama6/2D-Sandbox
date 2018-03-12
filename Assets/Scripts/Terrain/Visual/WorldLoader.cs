﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain.Data;

namespace Terrain.Visuals
{
  public class WorldLoader : MonoBehaviour
  {
    public static WorldLoader m_Terrain;

    public GameObject m_Character;
    public GameObject m_ChunkGO;

    // The rendering distance in chunks
    public int m_RenderDisatance;

    // Stores the chunks and the corresponding positions in actual world position so chunk_2_2 position is 32,32
    private Dictionary<Vector2, ChunkVisual> m_ChunkMap;

    // We store the dirty tiles in this que and in each frame we recalculate the visuals for the dirt tiles
    public Queue<TileGOData> dirtyTiles = new Queue<TileGOData>();

    private float timer = 0;

    private void Awake()
    {
      m_Terrain = this;
      m_ChunkMap = new Dictionary<Vector2, ChunkVisual>();
    }

    private void Update()
    {
      timer += Time.deltaTime;
      if (timer > 0.1f)
      {
        timer = 0;
        FindChunksToLoad();
        FindChunksToUnload();
        RedrawDirtyTiles();
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    /// <summary>
    /// We load all the chunks that is inside of renderDistance
    /// Distance is in chunk size
    /// </summary>
    private void FindChunksToLoad()
    {
      int _x;
      int _y;

      _x = (int)m_Character.transform.position.x / ChunkData.m_Size;
      _y = (int)m_Character.transform.position.y / ChunkData.m_Size;

      for (int x = _x - m_RenderDisatance; x < _x + m_RenderDisatance; x++)
      {
        for (int y = _y - m_RenderDisatance; y < _y + m_RenderDisatance; y++)
        {
          LoadChunkAt(x, y);
        }
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    //In world positions these are multiplied with chunks size
    // Chunk(5,5)= 5*chunk.size, 5*chunk.size in world
    private void LoadChunkAt(int x, int y)
    {
      int _chunkX = x * ChunkData.m_Size;    // world positions
      int _chunkY = y * ChunkData.m_Size;
      GameObject _chunk = null;
      if (m_ChunkMap.ContainsKey(new Vector2(_chunkX, _chunkY)) == false)
      {
        _chunk = m_ChunkGO.GetComponent<PooledMonobehaviour>().Get<PooledMonobehaviour>().gameObject;
        _chunk.transform.position = new Vector3(_chunkX, _chunkY, 0);

        _chunk.transform.name = "Chunk_" + x + "_" + y;
        _chunk.transform.parent = transform;
        m_ChunkMap.Add(new Vector2(_chunkX, _chunkY), _chunk.GetComponent<ChunkVisual>());

        if (x > 0 && x < WorldGeneration.m_ChunkMap.GetLength(0) && y > 0 && y < WorldGeneration.m_ChunkMap.GetLength(1))
        {
          _chunk.GetComponent<ChunkVisual>().DrawChunk(WorldGeneration.m_ChunkMap[x, y].GetTiles());
        }
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    private void FindChunksToUnload()
    {
      List<ChunkVisual> _deleteChunks = new List<ChunkVisual>(m_ChunkMap.Values);
      Queue<ChunkVisual> _deleteQueue = new Queue<ChunkVisual>();

      for (int i = 0; i < _deleteChunks.Count; i++)
      {
        float _distance = Vector3.Distance(m_Character.transform.position, _deleteChunks[i].transform.position);

        // There was an issue where we kept loading and unloading the chunks
        // thats why there is a +2 here so we only unload if we passed that number
        if (_distance > (m_RenderDisatance + 2) * ChunkData.m_Size)
        {
          _deleteQueue.Enqueue(_deleteChunks[i]);
        }
      }
      while (_deleteQueue.Count > 0)
      {
        ChunkVisual _chunk = _deleteQueue.Dequeue();
        m_ChunkMap.Remove(_chunk.transform.position);
        UnLoadChunk(_chunk.gameObject);
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    private void UnLoadChunk(GameObject _chunk)
    {
      foreach (Transform child in _chunk.transform)
      {
        child.gameObject.SetActive(false);
      }
      _chunk.SetActive(false);
    }

    //-----------------------------------------------------------------------------------------------------------//

    public GameObject GetTileAt(float x, float y)
    {
      x = Mathf.Round(x);
      y = Mathf.Round(y);

      // No need to devide with 16
      int _chunkX = (int)(x / 16) * 16;
      int _chunkY = (int)(y / 16) * 16;

      int _tileX = (int)x % ChunkData.m_Size;
      int _tileY = (int)y % ChunkData.m_Size;
      if (m_ChunkMap.ContainsKey(new Vector2(_chunkX, _chunkY)))
      {
        return m_ChunkMap[new Vector2(_chunkX, _chunkY)].GetTileAt(_tileX, _tileY);
      }
      return null;
    }

    //-----------------------------------------------------------------------------------------------------------//

    public TileType GetTileIDAt(float x, float y)
    {
      x = Mathf.Round(x);
      y = Mathf.Round(y);

      // No need to devide with 16
      int _chunkX = (int)(x / 16) * 16;
      int _chunkY = (int)(y / 16) * 16;

      int _tileX = (int)x % ChunkData.m_Size;
      int _tileY = (int)y % ChunkData.m_Size;
      if (m_ChunkMap.ContainsKey(new Vector2(_chunkX, _chunkY)))
      {
        return m_ChunkMap[new Vector2(_chunkX, _chunkY)].GetTileAt(_tileX, _tileY).GetComponent<TileGOData>().type;
      }
      return 0;
    }

    //-----------------------------------------------------------------------------------------------------------//

    public void SetTileAt(int x, int y, TileType type)
    {
      // We need this division and multiplication in the if below
      int _chunkX = x / ChunkData.m_Size;
      int _chunkY = y / ChunkData.m_Size;

      int _tileX = x % ChunkData.m_Size;
      int _tileY = y % ChunkData.m_Size;
      if (m_ChunkMap.ContainsKey(new Vector2(_chunkX * ChunkData.m_Size, _chunkY * ChunkData.m_Size)) == true)
      {
        m_ChunkMap[new Vector2(_chunkX * ChunkData.m_Size, _chunkY * ChunkData.m_Size)].SetTileAt(_tileX, _tileY, type);
      }
      else
      {
        Debug.LogError("Could not find chunk at " + _chunkX + "_" + _chunkY + " position!");
      }
    }

    //-----------------------------------------------------------------------------------------------------------//

    /// <summary>
    /// Recalcuate the visuals for every dirty tile (Mask and outline)
    /// </summary>
    private void RedrawDirtyTiles()
    {
      while (dirtyTiles.Count != 0)
      {
        //TODO: We dont want to do this for the machines
        // we need an if statement here
        TileGOData _tileToRedraw = dirtyTiles.Dequeue();

        // Check if we have the 4 cells for our tile (later we can remove this testing when we will have the cells for every tile)

        // Check for all the 4 cells in the corner to see which outline we should place
        int bitmaskValue = 0;

        // UpLeft
        if (_tileToRedraw.Up() != null && _tileToRedraw.Up().type != _tileToRedraw.type)
        {
          bitmaskValue += 2;
        }

        if (_tileToRedraw.UpLeft() != null && _tileToRedraw.UpLeft().type != _tileToRedraw.type)
        {
          bitmaskValue += 1;
        }

        if (_tileToRedraw.Left() != null && _tileToRedraw.Left().type != _tileToRedraw.type)
        {
          bitmaskValue += 8;
        }

        bitmaskValue = TileSpriteManager.sharedInstance.GetMaskIntTL(bitmaskValue);

        _tileToRedraw.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_TopLeftMaskTile", bitmaskValue);

        bitmaskValue = 0;
        // UpRight
        if (_tileToRedraw.Up() != null && _tileToRedraw.Up().type != _tileToRedraw.type)
        {
          bitmaskValue += 2;
        }

        if (_tileToRedraw.UpRight() != null && _tileToRedraw.UpRight().type != _tileToRedraw.type)
        {
          bitmaskValue += 4;
        }

        if (_tileToRedraw.Right() != null && _tileToRedraw.Right().type != _tileToRedraw.type)
        {
          bitmaskValue += 16;
        }
        bitmaskValue = TileSpriteManager.sharedInstance.GetMaskIntTR(bitmaskValue);

        _tileToRedraw.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_TopRightMaskTile", bitmaskValue);

        bitmaskValue = 0;
        // DownLeft
        if (_tileToRedraw.Down() != null && _tileToRedraw.Down().type != _tileToRedraw.type)
        {
          bitmaskValue += 64;
        }

        if (_tileToRedraw.DownLeft() != null && _tileToRedraw.DownLeft().type != _tileToRedraw.type)
        {
          bitmaskValue += 32;
        }
        if (_tileToRedraw.Left() != null && _tileToRedraw.Left().type != _tileToRedraw.type)
        {
          bitmaskValue += 8;
        }
        bitmaskValue = TileSpriteManager.sharedInstance.GetMaskIntBL(bitmaskValue);

        _tileToRedraw.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_BottomLeftMaskTile", bitmaskValue);

        bitmaskValue = 0;
        // DownRight

        if (_tileToRedraw.Down() != null && _tileToRedraw.Down().type != _tileToRedraw.type)
        {
          bitmaskValue += 64;
        }

        if (_tileToRedraw.DownRight() != null && _tileToRedraw.DownRight().type != _tileToRedraw.type)
        {
          bitmaskValue += 128;
        }

        if (_tileToRedraw.Right() != null && _tileToRedraw.Right().type != _tileToRedraw.type)
        {
          bitmaskValue += 16;
        }
        bitmaskValue = TileSpriteManager.sharedInstance.GetMaskIntBR(bitmaskValue);

        _tileToRedraw.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_BottomRightMaskTile", bitmaskValue);

        float _x = _tileToRedraw.transform.position.x;
        float _y = _tileToRedraw.transform.position.y;

        _tileToRedraw.GetComponentInChildren<SpriteRenderer>().material.SetVector("_UVOffset", GetUVOffset(_x, _y));
      }
    }

    private Vector4 GetUVOffset(float _x, float _y)
    {
      Vector4 _v4 = new Vector4(0, 0, 0, 0);
      _x %= 8;
      _y %= 8;

      switch ((int)_x)
      {
        case 0:
          _v4.x = 0.05f;
          break;

        case 1:
          _v4.x = 0.15f;
          break;

        case 2:
          _v4.x = 0.25f;
          break;

        case 3:
          _v4.x = 0.35f;
          break;

        case 4:
          _v4.x = 0.45f;
          break;

        case 5:
          _v4.x = 0.55f;
          break;

        case 6:
          _v4.x = 0.65f;
          break;

        case 7:
          _v4.x = 0.75f;
          break;

        default:
          _v4.x = 0;
          break;
      }

      switch ((int)_y)
      {
        case 0:
          _v4.y = 0.05f;
          break;

        case 1:
          _v4.y = 0.15f;
          break;

        case 2:
          _v4.y = 0.25f;
          break;

        case 3:
          _v4.y = 0.35f;
          break;

        case 4:
          _v4.y = 0.45f;
          break;

        case 5:
          _v4.y = 0.55f;
          break;

        case 6:
          _v4.y = 0.65f;
          break;

        case 7:
          _v4.y = 0.75f;
          break;

        default:
          _v4.y = 0;
          break;
      }
      return _v4;
    }
  }
}