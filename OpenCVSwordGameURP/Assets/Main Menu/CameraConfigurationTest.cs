using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraConfigurationTest : MonoBehaviour
{
    public RawImage trackingImage;
    public Vector3 trackerPosition;
    public GameObject cameraConfigurationPanel;

    public float smoothTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    public Vector3 previousPosition;

    private void Start()
    {
        previousPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        trackerPosition.x = UdpManager.instance.CurrentData.XPosition;
        trackerPosition.y = UdpManager.instance.CurrentData.YPosition;

        Vector3 newPosition = Vector3.SmoothDamp(previousPosition, trackerPosition, ref velocity, smoothTime);

        previousPosition = trackerPosition;

        if (!cameraConfigurationPanel.activeInHierarchy) return;

        trackingImage.rectTransform.SetPositionAndRotation(trackerPosition, Quaternion.identity);
    }
}
