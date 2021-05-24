using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordJointPosition : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 pos;
    public Vector3 localPos;

    private void Start()
    {
        offset = transform.localPosition;
    }

    private void Update()
    {
        localPos = transform.localPosition;
        pos = transform.position;
        transform.localPosition = offset;
    }
}
