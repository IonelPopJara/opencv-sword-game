using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ExtraScripts
{
    class Slicer
    {
        public static GameObject[] Slice(Plane plane, GameObject objectToCut, Material destroyMaterial, ref GameObject sliceParticles)
        {
            // Get the current mesh and its verts and tris
            Mesh mesh = objectToCut.GetComponent<MeshFilter>().mesh;
            var a = mesh.GetSubMesh(0);
            Sliceable sliceable = objectToCut.GetComponent<Sliceable>();

            if (sliceable == null)
            {
                // Revisar antes si puedo cortar cosas, ponerlas en capas por ejemplo
                Debug.Log($"Cannot slice throug {objectToCut.name}");
                throw new NotSupportedException("Cannot slice non sliceable object, add the sliceable script to the object");
            }

            // Create left and right slice of hollow object
            SlicesMetadata slicesMeta = new SlicesMetadata(plane, mesh, sliceable.IsSolid, sliceable.ReverseWireTriangles, sliceable.ShareVertices, sliceable.SmoothVertices);

            GameObject positiveObject = CreateMeshGameObject(objectToCut, destroyMaterial);
            positiveObject.name = string.Format("{0}_positive", objectToCut.name);


            GameObject negativeObject = CreateMeshGameObject(objectToCut, destroyMaterial);
            negativeObject.name = string.Format("{0}_negative", objectToCut.name);

            var positiveSideMeshData = slicesMeta.PositiveSideMesh;
            var negativeSideMeshData = slicesMeta.NegativeSideMesh;


            positiveObject.GetComponent<MeshFilter>().mesh = positiveSideMeshData;
            negativeObject.GetComponent<MeshFilter>().mesh = negativeSideMeshData;


            SetupCollidersAndRigidbodies(ref positiveObject, positiveSideMeshData, sliceable.UseGravity, ref sliceParticles);
            SetupCollidersAndRigidbodies(ref negativeObject, negativeSideMeshData, sliceable.UseGravity, ref sliceParticles);

            return new GameObject[] { positiveObject, negativeObject };
        }

        private static GameObject CreateMeshGameObject(GameObject originalObject, Material destroyMaterial)
        {
            var originalMaterial = originalObject.GetComponent<MeshRenderer>().materials;

            GameObject meshGameObject = new GameObject();
            Sliceable originalSliceable = originalObject.GetComponent<Sliceable>();

            meshGameObject.AddComponent<MeshFilter>();
            meshGameObject.AddComponent<MeshRenderer>();
            Sliceable sliceable = meshGameObject.AddComponent<Sliceable>();

            sliceable.IsSolid = originalSliceable.IsSolid;
            sliceable.ReverseWireTriangles = originalSliceable.ReverseWireTriangles;
            sliceable.UseGravity = originalSliceable.UseGravity;

            //meshGameObject.GetComponent<MeshRenderer>().materials = originalMaterial;
            meshGameObject.GetComponent<MeshRenderer>().material = destroyMaterial;

            meshGameObject.transform.localScale = originalObject.transform.localScale;
            meshGameObject.transform.rotation = originalObject.transform.rotation;
            meshGameObject.transform.position = originalObject.transform.position;

            meshGameObject.tag = originalObject.tag;

            return meshGameObject;
        }

        private static void SetupCollidersAndRigidbodies(ref GameObject gameObject, Mesh mesh, bool useGravity, ref GameObject sparkParticles)
        {
            MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;
            meshCollider.convex = true;

            var rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = useGravity;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            //  Set Layer for Sliceable Object
            gameObject.layer = 10;

            // Add the component to dissolve over time
            gameObject.AddComponent<DissolveTime>();

            SliceProjectile projectile = gameObject.AddComponent<SliceProjectile>();
            projectile.explosionParticles = sparkParticles;
        }
    }
}
