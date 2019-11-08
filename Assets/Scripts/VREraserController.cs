using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VREraserController : MonoBehaviour
{
    //设备 手柄
    [Header("需要用来擦拭的Vive Tracker控制器")]
    public SteamVR_TrackedObject trackedObj;

    /// <summary>
    /// 手柄输入
    /// </summary>
    private void FixedUpdate()
    {
        VRTexturePainter.Instance.VREraserTrackerSpray(trackedObj);
    }
}
