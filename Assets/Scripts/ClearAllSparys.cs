using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAllSparys : MonoBehaviour
{
    public GameObject brushContainer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("清空所有涂鸦墙上的涂料！");
            int sparyCount = brushContainer.gameObject.transform.childCount;
            Debug.Log("sparyCount : " + sparyCount);
            if (sparyCount > 0)
            {
                for (int i = 0; i < sparyCount; i++)
                {
                    Destroy(brushContainer.transform.GetChild(i).gameObject);
                }
            }
        }
    }

}
