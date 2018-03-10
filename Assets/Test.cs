using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Test : MonoBehaviour
{
  public Renderer rend;

  private void Start()
  {
    rend = GetComponent<Renderer>();
  }

  private void Update()
  {
    float offsetX = transform.position.x / 8;
    float offsetY = transform.position.y / 2;
    rend.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
    rend.material.SetTextureOffset("_EmissionTex", new Vector2(offsetX, offsetY));
  }
}