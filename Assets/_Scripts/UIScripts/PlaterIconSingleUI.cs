using UnityEngine;
using UnityEngine.UI;

public class PlaterIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    
 
    public void SetKitchenObjectSO(KitchenObject_SO kitchenObject_SO)
    {
        image.sprite = kitchenObject_SO.sprite;
    }
}
