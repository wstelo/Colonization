using UnityEngine;

public class WorkerBag : MonoBehaviour
{
    [SerializeField] Transform _productPosition;

    private Resourse _currentProduct = null;

    public Resourse CurrentProduct => _currentProduct;

    public void PlaceProduct(Resourse item)
    {
        _currentProduct = item;
        _currentProduct.SetAsChild(_productPosition);
    }

    public Resourse GiveAwayCurrentProduct()
    {
        _currentProduct.SetAsFree();
        Resourse tempProduct = _currentProduct;
        _currentProduct = null;
        return tempProduct;
    }
}
