using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

public enum BrushType
{
    Brush,
    Brush1,
    Brush2
}

public class ViveSparyController : MonoBehaviour
{
    [Header("涂鸦笔刷种类")]
    public BrushType brushType;
    //对应的色号
    [Header("需要涂鸦的色号")]
    public Color color;

    [Header("涂鸦笔刷的大小")]
    public float brushSize = 1.0f;

    //设备 手柄
    [Header("需要用来涂鸦的Vive Tracker控制器")]
    public VivePoseTracker trackedObj;

    [Header("需要用来涂鸦的Vive Tracker模拟器")]
    public GameObject tracker;


    /// <summary>
    /// 手柄输入
    /// </summary>
    private void FixedUpdate()
    {      

        if (ViveInput.GetPress(trackedObj.viveRole, ControllerButton.Trigger))
        {
            Debug.Log("扳机按住");
            tracker.transform.position = trackedObj.transform.position;
            ViveTexturePainter.Instance.VRTrackerSpary(trackedObj, tracker, color, brushType.ToString(), brushSize);
        }

        if (ViveInput.GetPressDown(trackedObj.viveRole, ControllerButton.Trigger))
        {
            Debug.Log("扳机按下！");
        }

        if (ViveInput.GetPressUp(trackedObj.viveRole, ControllerButton.Trigger))
        {
            Debug.Log("扳机抬起！");
        }
    }
}
