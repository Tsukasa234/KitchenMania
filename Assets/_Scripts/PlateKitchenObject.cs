using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObject_SO kitchenObject_SO;
    }

    [SerializeField] private List<KitchenObject_SO> validKitchenObjectSO;

    private List<KitchenObject_SO> kitchenObjectList;
     
    private void Awake()
    {
        kitchenObjectList = new List<KitchenObject_SO>();
    }


    public bool TryAddIngredient(KitchenObject_SO kitchenObject_SO)
    {
        if (!validKitchenObjectSO.Contains(kitchenObject_SO))
        {
            //Not valid ingredient
            return false;
        }
        if (kitchenObjectList.Contains(kitchenObject_SO))
        {
            //Already Has this Type
            return false;
        }
        kitchenObjectList.Add(kitchenObject_SO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            kitchenObject_SO = kitchenObject_SO
        });
        return true;
    }

    public List<KitchenObject_SO> GetKitchenObjectList()
    {
        return kitchenObjectList;
    }
}
