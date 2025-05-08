using Spine.Unity;
using UnityEngine;

/// <summary> 自动播放Spine动画 </summary>
public class AutoPlaySpine : MonoBehaviour
{
    SkeletonGraphic Spine;
    public string OnceAnimName;
    public float OnceAnimTime;
    public string LoopAnimName;

    private void OnEnable()
    {
        Spine = GetComponent<SkeletonGraphic>();
        Spine.startingLoop = false;
        Spine.PlayAnim(OnceAnimName, false);
        CancelInvoke(nameof(SetLoopAnim));
        Invoke(nameof(SetLoopAnim), OnceAnimTime);
    }
    void SetLoopAnim()
    {
        Spine.PlayAnim(LoopAnimName, true);
        Spine.startingLoop = true;
    }
}
