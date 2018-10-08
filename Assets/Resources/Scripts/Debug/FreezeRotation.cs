using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour {

    Vector3 LocalPos;
    Quaternion LocalRot;

    private void Start()
    {
        LocalPos = transform.localPosition;
        LocalRot = transform.localRotation;
    }

    void LateUpdate()
    {
        transform.localPosition= LocalPos;
        transform.localRotation= LocalRot;
    }
}
