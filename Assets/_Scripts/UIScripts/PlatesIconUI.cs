using UnityEngine;
using UnityEngine.UI;

public class PlatesIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform item in transform)
        {
            if (item == iconTemplate) continue;
            Destroy(item.gameObject);
        }
        foreach (KitchenObject_SO item in plateKitchenObject.GetKitchenObjectList()) 
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlaterIconSingleUI>().SetKitchenObjectSO(item);
        }
    }
}
