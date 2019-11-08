using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class ViveEraserController : MonoBehaviour
{
    //设备 手柄
    [Header("需要用来擦拭的Vive Tracker控制器")]
    public VivePoseTracker trackedObj;

    [Header("需要用擦拭的Vive Tracker模拟器")]
    public GameObject tracker;

    /// <summary>
    /// 手柄输入
    /// </summary>
    private void FixedUpdate()
    {
        tracker.transform.position = trackedObj.transform.position;

        ViveTexturePainter.Instance.VREraserTrackerSpray(trackedObj, tracker);
    }
}
