using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("触发器进入！");
        if (other.transform.tag == "Spary")
        {
            Debug.Log("擦拭涂鸦墙！");
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("触发器停留！");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("触发器离开！");
    }
}
