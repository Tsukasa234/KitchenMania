using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObject_SO kitchenObjectPrefab;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObject_SO GetKitchenObjectSO()
    {
        return kitchenObjectPrefab;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        if(kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already has a KitchenObject!");
        }

        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plate)
    {
        if(this is PlateKitchenObject)
        {
            plate = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plate = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObject_SO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
