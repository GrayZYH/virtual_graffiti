using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBrush : MonoBehaviour {

    public Transform Track;
    public GameObject Cube;
    Vector3 Target;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Track.localPosition, Track.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Target = hit.point;

                Vector3 screenPos = Camera.main.WorldToScreenPoint(Target);

                GameObject g = Instantiate(Cube, Target, Quaternion.identity);
            }
        }
	}
}
