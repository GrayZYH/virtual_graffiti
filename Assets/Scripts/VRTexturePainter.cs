using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VRTexturePainter : MonoBehaviour
{
    public static VRTexturePainter Instance;
    public static List<GameObject> AllSprays =new List<GameObject>();
    public GameObject SparyWall;

    public Color color1;
    public Color color2;
    
    [Header("能涂鸦的最大距离")]
    public float SparyDis;
    [Header("涂鸦笔刷的大小")]
    public float brushSize = 1.0f;
    [Header("涂鸦效果与涂鸦墙的距离")]
    public float sprayToWallDis = 0.2f;
    [Header("喷漆罐与Vive Tracker在x轴上的偏移量")]
    public float OffsetX = 0f;
    [Header("喷漆罐与Vive Tracker在Y轴上的偏移量")]
    public float OffsetY = 0f;
    public GameObject brushContainer; 
	public Camera sceneCamera; 

	Painter_BrushMode mode; 	
	Color brushColor; 
	int brushCounter=0,MAX_BRUSH_COUNT=1000; 
	bool saving=false;

    private void Awake()
    {
        Instance = this;
    }

    void FixedUpdate()
    {

        /// 鼠标左键模拟涂鸦
		//if (Input.GetMouseButton(0))
  //      {
  //          SpawnSpary();
  //      }

        ///鼠标右键模拟橡皮
        //if (Input.GetMouseButton(1))
        //{
        //    SpawnEraser();
        //}
    }

    /// <summary>
    /// VR虚拟涂鸦
    /// </summary>
    /// <param name="viveTracker">ViveTracker Vive追踪器</param>
    /// <param name="color">Color 色号</param>
    public void VRTrackerSpary(SteamVR_TrackedObject viveTracker, Color color)
    {

        //RaycastHit2D hit;
        //Ray2D trackerRay = new Ray2D(viveTracker.transform.position, viveTracker.transform.forward); // 由Vive Tracker 发出的位置射线

        //hit = Physics2D.Raycast(viveTracker.transform.position, viveTracker.transform.forward, SparyDis);

        //if (hit.collider.tag !=null)
        //{
        //    Debug.Log("涂鸦的目标" + hit.collider.tag);
        //    GameObject brushObj;

        //    brushColor = color;

        //    brushObj = (GameObject)Instantiate(Resources.Load("TexturePainter-Instances/Brush"));
        //    brushObj.GetComponent<SpriteRenderer>().color = brushColor;

        //    brushObj.transform.parent = brushContainer.transform;
        //    brushObj.transform.localPosition = new Vector3(hit.point.x, hit.point.y + OffsetY, SparyWall.transform.position.z - sprayToWallDis);
        //    brushObj.transform.localScale = Vector3.one * brushSize;


        //    Debug.Log("涂鸦完成！");
        //}

        RaycastHit hit;
        Ray trackerRay = new Ray(viveTracker.transform.position, viveTracker.transform.forward);

        if (Physics.Raycast(trackerRay, out hit))
        {
            Debug.Log("涂鸦的目标" + hit.collider.tag);
            GameObject brushObj;

            brushColor = color;

            brushObj = (GameObject)Instantiate(Resources.Load("TexturePainter-Instances/Brush"));
            brushObj.GetComponent<SpriteRenderer>().color = brushColor;

            brushObj.transform.parent = brushContainer.transform;
            brushObj.transform.localPosition = new Vector3(hit.point.x+OffsetX, hit.point.y + OffsetY, SparyWall.transform.position.z - sprayToWallDis);
            brushObj.transform.localScale = new Vector3(brushSize, brushSize, brushSize);

            AllSprays.Add(brushObj);
            Debug.Log("涂鸦完成！");
        }

    }

    /// <summary>
    /// VR虚拟涂鸦擦拭
    /// </summary>
    /// <param name="viveTracker"></param>
    public void VREraserTrackerSpray(SteamVR_TrackedObject viveTracker)
    {

        //RaycastHit2D hit;
        //hit = Physics2D.Raycast(viveTracker.transform.position, viveTracker.transform.forward, SparyDis);
        //if (hit.collider.tag == "Spary")
        //{
        //    Destroy(hit.transform.gameObject);
        //    Debug.Log("擦拭目标！");
        //}

        RaycastHit hit;
        Ray trackerRay = new Ray(viveTracker.transform.position, viveTracker.transform.forward); // 由Vive Tracker 发出的位置射线

        if (Physics.Raycast(trackerRay, out hit, SparyDis))
        {
            Debug.Log("需要擦拭的目标" + hit.collider.tag);

            if (hit.collider.tag == "Spary")
            {
                Destroy(hit.transform.gameObject);
                AllSprays.Remove(hit.transform.gameObject);
                Debug.Log("擦拭目标！");
            }
        }
    }

    #region  鼠标涂鸦测试

    /// <summary>
    /// VR涂鸦橡皮檫 测试
    /// </summary>
    public void SpawnEraser()
    {
        RaycastHit hit;
        Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        Ray cursorRay = sceneCamera.ScreenPointToRay(cursorPos);

        if (Physics.Raycast(cursorRay, out hit, SparyDis))
        {
            //Debug.Log("需要擦拭的目标" + hit.collider.tag);

            if (hit.collider.tag == "Spary")
            {
                Destroy(hit.collider.transform.parent.gameObject);
            }
        }
    }

    /// <summary>
    /// VR涂鸦  测试
    /// </summary>
    public void SpawnSpary()
    {
        RaycastHit hit;
        Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        Ray cursorRay = sceneCamera.ScreenPointToRay(cursorPos);

        if (Physics.Raycast(cursorRay, out hit, SparyDis))
        {
            GameObject brushObj;


            brushColor = color1;


            brushObj = (GameObject)Instantiate(Resources.Load("TexturePainter-Instances/Brush")); 
            brushObj.GetComponent<SpriteRenderer>().color = brushColor;

            brushObj.transform.parent = brushContainer.transform; 
            brushObj.transform.localPosition = new Vector3(hit.point.x, hit.point.y, SparyWall.transform.position.z - 0.1f);
            brushObj.transform.localScale = Vector3.one * brushSize;
        }
    }

    #endregion

    ////////////////// OPTIONAL METHODS //////////////////

#if !UNITY_WEBPLAYER
    IEnumerator SaveTextureToFile(Texture2D savedTexture){		
			brushCounter=0;
			string fullPath=System.IO.Directory.GetCurrentDirectory()+"\\UserCanvas\\";
			System.DateTime date = System.DateTime.Now;
			string fileName = "CanvasTexture.png";
			if (!System.IO.Directory.Exists(fullPath))		
				System.IO.Directory.CreateDirectory(fullPath);
			var bytes = savedTexture.EncodeToPNG();
			System.IO.File.WriteAllBytes(fullPath+fileName, bytes);
			Debug.Log ("<color=orange>Saved Successfully!</color>"+fullPath+fileName);
			yield return null;
		}
	#endif
}
