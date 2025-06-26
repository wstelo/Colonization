using UnityEngine;
using UnityEngine.Pool;

public abstract class CharacterSpawner<T> : MonoBehaviour where T : Worker
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;

    private Vector3 _position = new Vector3(0, 0, 0);
    protected ObjectPool<Worker> Pool;
    private int _poolMaxSize = 10;

    public virtual void Awake()
    {
        Pool = new ObjectPool<Worker>(
            createFunc: () => CreateObject(),
            actionOnGet: (worker) => Initialize(worker),
            actionOnRelease: (worker) => worker.gameObject.SetActive(false),
            defaultCapacity: _poolCapacity,
            actionOnDestroy: (worker) => DestroyObject(worker),
            maxSize: _poolMaxSize);
    }

    private Worker CreateObject()
    {
        Worker worker = Instantiate(_prefab, _position, Quaternion.identity, transform);
        worker.gameObject.SetActive(false); 

        return worker;
    }

    private void DestroyObject(Worker worker)
    {
        Destroy(worker.gameObject);
    }

    public virtual void Initialize(Worker worker)
    {

    }

    public virtual void ReleasedObject(Worker worker)
    {

    }
}
