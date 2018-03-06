using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain.Data;
using Terrain.Visuals;
using TypeSafeEventManager.Events;

public class CharacterController2D : PhysicsObject
{
  public float maxSpeed = 7;
  public float jumpTakeOffSpeed = 7;

  // private SpriteRenderer spriteRenderer;
  //private Animator animator;

  //TODO: if we want to use events we need to override the physicsObject class OnEnable function
  //-----------------------------------------------------------------------------------------------------------//

  #region UnityFunctions

  private void Awake()
  {
    //spriteRenderer = GetComponent<SpriteRenderer>();
    //animator = GetComponent<Animator>();
  }

  //-----------------------------------------------------------------------------------------------------------//

  protected override void Update()
  {
    base.Update();
    if (Input.GetMouseButton(0))
    {
      Mining();
    }
    if (Input.GetMouseButtonDown(1))
    {
      PlaceBlockFromInventory();

      Vector2 _spawnTilePosition = new Vector3(Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 0.5f), Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 0.5f));

      WorldLoader.m_Terrain.GetTileAt((int)_spawnTilePosition.x, (int)_spawnTilePosition.y).GetComponent<TileGOData>().ShowInventory();
    }
    if (Input.GetKey(KeyCode.E))
    {
      Interact();
    }
  }

  #endregion UnityFunctions

  //-----------------------------------------------------------------------------------------------------------//

  #region Movement

  protected override void ComputeVelocity()
  {
    Vector2 move = Vector2.zero;

    move.x = Input.GetAxis("Horizontal");

    if (Input.GetButtonDown("Jump") && grounded)
    {
      velocity.y = jumpTakeOffSpeed;
    }
    else if (Input.GetButtonUp("Jump"))
    {
      if (velocity.y > 0)
      {
        velocity.y = velocity.y * 0.5f;
      }
    }

    //bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
    //if (flipSprite)
    //{
    //  spriteRenderer.flipX = !spriteRenderer.flipX;
    //}

    //animator.SetBool("grounded", grounded);
    //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

    targetVelocity = move * maxSpeed;
  }

  #endregion Movement

  //-----------------------------------------------------------------------------------------------------------//

  //TODO: call these functions from a global mouseController
  private void Mining()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Vector2 _mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    Vector2 _characterPosition = new Vector2(transform.position.x, transform.position.y);
    // change 3 to reachDistance
    if (Vector2.Distance(_mousePosition, _characterPosition) < 3)
    {
      RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

      if (hit.collider != null && hit.collider.transform.tag == "Tile")
      {
        //TODO: 10-pickaxe strenght
        hit.collider.GetComponent<TileGOData>().Mine(1);
      }
    }
  }

  //-----------------------------------------------------------------------------------------------------------//

  private void PlaceBlockFromInventory()
  {
    Vector2 _mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    Vector2 _characterPosition = new Vector2(transform.position.x, transform.position.y);
    // change 3 to reachDistance
    if (Vector2.Distance(_mousePosition, _characterPosition) < 3)
    {
      ushort _itemID = InventoryPanel.sharedInstance.GetItemInSelectedInventorySlot(false);
      if (_itemID != 0)
      {
        Vector2 _spawnTilePosition = new Vector3(Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 0.5f), Mathf.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 0.5f));

        if (WorldGeneration.m_Terrain.GetTileAt((int)_spawnTilePosition.x, (int)_spawnTilePosition.y) == 0)
        {
          WorldLoader.m_Terrain.SetTileAt((int)_spawnTilePosition.x, (int)_spawnTilePosition.y, _itemID);

          WorldGeneration.m_Terrain.SetTileAt((int)_spawnTilePosition.x, (int)_spawnTilePosition.y, _itemID);
          InventoryPanel.sharedInstance.GetItemInSelectedInventorySlot(true);
        }
      }
    }
  }

  //-----------------------------------------------------------------------------------------------------------//

  private void Interact()
  {
    Vector2 _characterPosition = new Vector2(transform.position.x, transform.position.y);
    IInteractable _tileToInteract = WorldLoader.m_Terrain.GetTileAt(_characterPosition.x, _characterPosition.y).GetComponent<IInteractable>();

    if (_tileToInteract != null)
    {
      _tileToInteract.Interact();
    }
  }
}