using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField] Projector projector;
    [SerializeField] UnityOutlineManager unityOutlineManager;
    [SerializeField] UnityOutlineFX unityOutlineFX;
    Vector3 projectorOrigin;

    // Use this for initialization
    void Start()
    {
        projectorOrigin = projector.transform.position;
    }

    public void SpawnObject(AssociatedObject go)
    {
        GameObject g = null;
        Ray ray = new Ray(projectorOrigin, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            g = Instantiate(go.associatedObject, hit.point, Quaternion.identity, this.transform) as GameObject;
            g.AddComponent<BoxCollider>().size = go.colliderSize;
            g.layer = 10;
        }
    }
}


