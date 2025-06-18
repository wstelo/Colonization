using UnityEngine;

public class WorkerAnimationData : MonoBehaviour
{
    public static readonly int IsIdle = Animator.StringToHash(nameof(IsIdle));
    public static readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));
    public static readonly int IsMining = Animator.StringToHash(nameof(IsMining));
}
