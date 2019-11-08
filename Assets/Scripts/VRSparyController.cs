using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSparyController : MonoBehaviour
{
    //对应的色号
    [Header("需要涂鸦的色号")]
    public Color color;

    //设备 手柄
    [Header("需要用来涂鸦的Vive Tracker控制器")]
    public SteamVR_TrackedObject trackedObj;

    /// <summary>
    /// 手柄输入
    /// </summary>
    private void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device == null)
        {
            return;
        }
        if (trackedObj == null)
        {
            return;
        }

        //扳机按住
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("扳机按住！");
            VRTexturePainter.Instance.VRTrackerSpary(trackedObj, color);
        }

        //扳机按下
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("扳机按下！");
        }

        //扳机抬起
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("扳机抬起！");
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 cc = device.GetAxis();

            float angle = VectorAngle(new Vector2(1, 0), cc);

            if (angle > 45 && angle < 135)
            {
                //Debug.Log("下");  
            }
            else if (angle < -45 && angle > -135)
            {
                //Debug.Log("上");
            }
            else if ((angle < 180 && angle > 135) || (angle < -135 && angle > -180))
            {
                //Debug.Log("左");  
            }
            else if ((angle > 0 && angle < 45) || (angle > -45 && angle < 0))
            {
                //Debug.Log("右");  
            }
        }
    }

    /// <summary>  
    /// 根据在圆盘才按下的位置，返回一个角度值  
    /// </summary>  
    /// <param name="from"></param>  
    /// <param name="to"></param>  
    /// <returns></returns>
    /// 
    private float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }
}
