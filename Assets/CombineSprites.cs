using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineSprites : MonoBehaviour
{
  public static CombineSprites sharedInstance;

  public Texture2D[] atlasTextures;
  private Rect[] rects;
  private Texture2D atlas;

  private Color[] pixels;

  private Sprite combinedSprite;

  private void Awake()

  {
    sharedInstance = this;
  }

  private void Start()
  {
    atlas = new Texture2D(256, 256);
    // atlasTextures = new Texture2D[4];
    pixels = new Color[128 * 128];

    combinedSprite = Sprite.Create(atlas, new Rect(0, 0, atlas.width, atlas.height), new Vector2(0, 0));

    rects = atlas.PackTextures(atlasTextures, 2, 256);

    MaterialPropertyBlock block = new MaterialPropertyBlock();
    block.SetTexture("_MainTex", atlas);
    GetComponent<SpriteRenderer>().SetPropertyBlock(block);
  }

  public Sprite SpriteCombine(Sprite[] _sprites)
  {
    for (int i = 0; i < _sprites.Length; i++)
    {
      atlasTextures[i] = SpriteToTexture2D(_sprites[i]);
    }
    rects = atlas.PackTextures(atlasTextures, 2, 256);

    return Sprite.Create(atlas, new Rect(0, 0, atlas.width, atlas.height), new Vector2(0, 0), 256, 0, SpriteMeshType.FullRect);
  }

  private Texture2D SpriteToTexture2D(Sprite _sprite)
  {
    Texture2D _tex = new Texture2D((int)_sprite.rect.width, (int)_sprite.rect.height);
    Color[] _pixels = _sprite.texture.GetPixels((int)_sprite.textureRect.x,
                                                (int)_sprite.textureRect.y,
                                                (int)_sprite.textureRect.width,
                                                (int)_sprite.textureRect.height);

    pixels[0] = Color.black;
    _tex.SetPixels(pixels);
    _tex.Apply();
    return _tex;
  }
}