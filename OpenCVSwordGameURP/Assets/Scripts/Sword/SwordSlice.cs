using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraScripts;

public class SwordSlice : MonoBehaviour
{
    public float forceAppliedToCut = 3f;
    public float timeBeforeVanishing = 10f;

    public GameObject bladeTip;
    public GameObject bladeBase;

    private Vector3 previousTipPosition;
    private Vector3 previousBasePosition;

    private Vector3 triggerEnterTipPosition;
    private Vector3 triggerEnterBasePosition;
    private Vector3 triggerExitTipPosition;

    public Material destroyMaterial;

    public GameObject sparkParticlesPrefab;

    private void Start()
    {
        previousTipPosition = bladeTip.transform.position;
        previousBasePosition = bladeBase.transform.position;
    }

    private void LateUpdate()
    {
        previousTipPosition = bladeTip.transform.position;
        previousBasePosition = bladeBase.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerEnterTipPosition = bladeTip.transform.position;
        triggerEnterBasePosition = bladeBase.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        triggerExitTipPosition = bladeTip.transform.position;

        // Create a trinagle between the tip and base so that we can get the normal
        Vector3 side1 = triggerExitTipPosition - triggerEnterTipPosition;
        Vector3 side2 = triggerExitTipPosition - triggerEnterBasePosition;

        // Get the point perpendicular to the triangle above (the normal)
        Vector3 normal = Vector3.Cross(side1, side2).normalized;

        // Transform the normal so that it is aligned with the object we are slicing's transform
        Vector3 transformedNormal = ((Vector3)(other.gameObject.transform.localToWorldMatrix.transpose * normal)).normalized;

        // Get the enter position relative to the object we're cutting's local transform
        Vector3 transformedStartingPoint = other.gameObject.transform.InverseTransformPoint(triggerEnterTipPosition);

        Plane plane = new Plane();

        plane.SetNormalAndPosition(transformedNormal, transformedStartingPoint);

        var direction = Vector3.Dot(Vector3.up, transformedNormal);

        // Flip the plane so taht we always know which side the positive mesh is on
        if (direction < 0)
        {
            plane = plane.flipped;
        }

        GameObject[] slices = Slicer.Slice(plane, other.gameObject, destroyMaterial, ref sparkParticlesPrefab);

        other.GetComponent<IProjectile>()?.Explode();

        Destroy(other.gameObject);

        /////Effects/////
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(0.1f, 0.1f));
        /////Effects/////
        
        Rigidbody rb = slices[1].GetComponent<Rigidbody>();
        Vector3 newNormal = transformedNormal + Vector3.up * forceAppliedToCut;
        rb.AddForce(newNormal, ForceMode.Impulse);

        //DestroyPieces(slices);
    }

    private void DestroyPieces(GameObject[] pieces)
    {
        foreach (var piece in pieces)
        {
            Destroy(piece, timeBeforeVanishing);
        }
    }
}
