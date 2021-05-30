using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenu : MonoBehaviour
{
    public Transform[] cameraPositions;

    public Transform currentPosition;

    private void Start()
    {
        currentPosition = cameraPositions[0];
    }
}
