using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipeSO", menuName = "Scriptable Objects/FryingRecipeSO")]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObject_SO input;
    public KitchenObject_SO output;
    public int fryingTimerMax;
    
}
