using Unity.VisualScripting;
using UnityEngine;

public class ExtractedResourseDetector : MonoBehaviour
{
    private ResourseCollector _resourseSystem;
    private VisitorCollector _visitorCollector;

    private void Awake()
    {
        _resourseSystem = GetComponent<ResourseCollector>();
        _visitorCollector = new VisitorCollector(_resourseSystem);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Worker worker) && worker.CurrentResourseInBag != null && transform.position == worker.CurrentCampPosition)
        {
            Resourse product = worker.CurrentResourseInBag;
            product.CollectedObject();
            product.Accept(_visitorCollector);
        }
    }
}