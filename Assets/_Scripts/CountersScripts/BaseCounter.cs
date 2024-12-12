using System.Collections;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    //private void Start()
    //{
    //    StartCoroutine(HandleFriedTimer());
    //}

    //private IEnumerator HandleFriedTimer()
    //{
    //    yield return new WaitForSeconds(1f);
    //}

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }
    
    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("BaseCounter.InteractAlternate()");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
