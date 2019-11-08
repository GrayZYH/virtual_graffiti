using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintCanvas : MonoBehaviour {

    public Transform ViveHead;

    Vector3 Offext;
	// Use this for initialization
	void Start () {
        Offext = transform.position - ViveHead.position;
        Debug.Log(ViveHead.position);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = ViveHead.position + Offext;
	}
}
