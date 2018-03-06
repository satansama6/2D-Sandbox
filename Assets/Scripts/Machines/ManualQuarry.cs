using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain.Visuals;

public class ManualQuarry : MonoBehaviour
{
  private void Start()
  {
  }

  private float timer = 0;

  private void Update()
  {
    timer += Time.deltaTime;
    if (timer > 3)
    {
      // Destroy(WorldLoader.m_Terrain.GetTileAt(transform.position.x + 3, transform.position.y));
      timer = -1111;
    }
  }

  public void Work()
  {
  }
}