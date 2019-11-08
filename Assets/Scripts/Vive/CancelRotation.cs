using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelRotation : MonoBehaviour
{
    public Vector3 RotationVec;

	void FixedUpdate()
    {
        this.transform.localRotation = new Quaternion(RotationVec.x, RotationVec.y, RotationVec.z,0);
    }
}
