using UnityEngine;

[CreateAssetMenu(fileName = "BurningRecipeSO", menuName = "Scriptable Objects/BurningRecipeSO")]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObject_SO input;
    public KitchenObject_SO output;
    public int burningTimerMax;
}
