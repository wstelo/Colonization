using UnityEngine;

public class WorkerBag : MonoBehaviour
{
    [SerializeField] Transform _productPosition;

    public Resourse CurrentProduct { get; private set; }

    public void PlaceProduct(Resourse item)
    {
        CurrentProduct = item;
        CurrentProduct.SetAsChild(_productPosition);
        
    }

    public Resourse GiveAwayCurrentProduct()
    {
        CurrentProduct.SetAsFree();
        Resourse tempProduct = CurrentProduct;
        CurrentProduct = null;
        return tempProduct;
    }

    public void ResetCurrentResourse()
    {
        CurrentProduct = null;
    }
}
