using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTime : MonoBehaviour
{
    public string nam;
    private readonly float duration = 0.8f;
    public bool isDissolved;

    Renderer thisRenderer;

    private void Start()
    {
        isDissolved = false;
        thisRenderer = GetComponent<Renderer>();
        nam = thisRenderer.material.name;
    }

    float t = 0;

    private void Update()
    {
        t += Time.deltaTime / duration;
        float value = Mathf.Lerp(0.5f, -0.215f, t);
        thisRenderer.sharedMaterial.SetFloat("_CutoffHeight", value);
        isDissolved = value <= -0.215f;

        if (isDissolved) Destroy(this.gameObject);
    }
}
