using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecipeDatabase : MonoBehaviour
{
  public static RecipeDatabase sharedInstance;

  public List<Recipe> recipeDatabase = new List<Recipe>();

  private void Awake()
  {
    sharedInstance = this;
  }

  private void Start()
  {
    List<Item> _inputItems = new List<Item>
    {
      ItemDatabase.sharedInsatance.FetchItemByID(1),
      ItemDatabase.sharedInsatance.FetchItemByID(5)
    };
    recipeDatabase.Add(new Recipe(ItemDatabase.sharedInsatance.FetchItemByID(100), _inputItems));

    _inputItems = new List<Item>();
    _inputItems.Add(ItemDatabase.sharedInsatance.FetchItemByID(5));

    recipeDatabase.Add(new Recipe(ItemDatabase.sharedInsatance.FetchItemByID(101), _inputItems));
  }
}