using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class BatallionControl : MonoBehaviour
{
    [SerializeField] Transform[] targets;
    List<AICharacterControl> soldiers = new List<AICharacterControl>();
    public Vector3 centerPoint;

    // Use this for initialization
    void Start()
    {
        centerPoint = Vector3.zero;
        int number = 0;
        foreach (Transform child in transform)
        {
            AICharacterControl temp = child.GetChild(0).GetComponent<AICharacterControl>();

            if (temp != null)
            {
                soldiers.Add(child.GetComponent<AICharacterControl>());
                centerPoint += temp.transform.position;
                number++;
            }
        }

        centerPoint /= number;

        StartCoroutine(InitializeBatallion());
    }
    void Update()
    {
        centerPoint = Vector3.zero;
        int number = 0;
        foreach (Transform child in transform)
        {
            AICharacterControl temp = child.GetComponent<AICharacterControl>();

            if (temp != null)
            {
                soldiers.Add(child.GetComponent<AICharacterControl>());
                centerPoint += temp.transform.position;
                number++;
            }
        }
        centerPoint /= number;
    }

    IEnumerator InitializeBatallion()
    {
        yield return new WaitForSeconds(2f);
        foreach (AICharacterControl soldier in soldiers)
        {
            Vector3 offset = soldier.transform.position - centerPoint;
            Vector3[] path = new Vector3[targets.Length];
            for (int i = 0; i < targets.Length; i++)
            {
                path[i] = targets[i].position + offset;
            }
            soldier.EnterMarchingState(path);
        }
    }
}