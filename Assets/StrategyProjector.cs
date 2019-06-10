using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyProjector : MonoBehaviour
{
    Projector projector;
    // Use this for initialization
    void Start()
    {
        projector = GetComponent<Projector>();
        RaycastHit hit;
        Ray ray = new Ray(this.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = hit.point;
        }
        ray = new Ray(new Vector3(this.transform.position.x + projector.orthographicSize / 2f,
            this.transform.position.y, this.transform.position.z), Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = hit.point;
        }
    }

}
