using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadControl : MonoBehaviour {

    public Transform HeadCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = HeadCamera.rotation;
	}
}
