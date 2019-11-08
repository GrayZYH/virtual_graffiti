using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

public class ViveTexturePainter : MonoBehaviour
{
    public static ViveTexturePainter Instance;
    public static List<GameObject> AllSprays = new List<GameObject>();
    public GameObject SparyWall;


    [Header("能涂鸦的最大距离")]
    public float SparyDis;
    [Header("涂鸦效果与涂鸦墙的距离")]
    public float sprayToWallDis = 0.01f;
    [Header("喷漆罐与Vive Tracker在x轴上的偏移量")]
    public float OffsetX = 0f;
    [Header("喷漆罐与Vive Tracker在Y轴上的偏移量")]
    public float OffsetY = 0f;
    public GameObject brushContainer;

    public Text LogText;

    private Color brushColor;


    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// VR虚拟涂鸦
    /// </summary>
    /// <param name="viveTracker">ViveTracker Vive追踪器</param>
    /// <param name="color">Color 色号</param>
    public void VRTrackerSpary(VivePoseTracker viveTracker,GameObject varTracker, Color color,string brush,float brushSize)
    {
        RaycastHit hit;
        Ray trackerRay = new Ray(viveTracker.transform.position, varTracker.transform.forward);

        if (Physics.Raycast(trackerRay, out hit))
        {
            Debug.Log("涂鸦的目标" + hit.collider.tag);

            //LogText.text = "扳机按住！";
            GameObject brushObj;

            brushColor = color;

            brushObj = (GameObject)Instantiate(Resources.Load("TexturePainter-Instances/"+brush));
            brushObj.GetComponent<SpriteRenderer>().color = brushColor;

            brushObj.transform.parent = brushContainer.transform;
            brushObj.transform.localPosition = new Vector3(hit.point.x + OffsetX, hit.point.y + OffsetY, SparyWall.transform.position.z - sprayToWallDis);
            brushObj.transform.localScale = new Vector3(brushSize, brushSize, brushSize);

            AllSprays.Add(brushObj);
            Debug.Log("涂鸦完成！");
        }

    }

    /// <summary>
    /// VR虚拟涂鸦擦拭
    /// </summary>
    /// <param name="viveTracker"></param>
    public void VREraserTrackerSpray(VivePoseTracker viveTracker, GameObject varTracker)
    {

        RaycastHit hit;
        Ray trackerRay = new Ray(viveTracker.transform.position, varTracker.transform.forward); // 由Vive Tracker 发出的位置射线

        if (Physics.Raycast(trackerRay, out hit))
        {
            Debug.Log("需要擦拭的目标" + hit.collider.tag);

            if (hit.collider.tag == "Spary")
            {
                Destroy(hit.transform.gameObject);
                AllSprays.Remove(hit.transform.gameObject);
            }
        }
    }
}
