using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObject_SO", menuName = "Scriptable Objects/KitchenObject_SO")]
public class KitchenObject_SO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
