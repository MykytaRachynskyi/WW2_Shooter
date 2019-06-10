using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class CoverPointManager : MonoBehaviour
    {

        [SerializeField] List<Transform> availableCoverPoints = new List<Transform>();

        public Transform GetFreePoint(Vector3 position)
        {
            Transform closestPoint = availableCoverPoints[0];
            float distanceToBeat = Vector3.Distance(position, closestPoint.position);
            if (availableCoverPoints.Count > 0)
            {
                foreach (Transform point in availableCoverPoints)
                {
                    if (Vector3.Distance(position, point.position) < distanceToBeat)
                        closestPoint = point;
                }
                availableCoverPoints.Remove(closestPoint);
            }
            else
                closestPoint = null;

            return closestPoint;
        }

        public void ReturnPointToAvailabe(Transform point)
        {
            if (point.parent == this.transform)
            {
                availableCoverPoints.Add(point);
            }
        }
    }
}
