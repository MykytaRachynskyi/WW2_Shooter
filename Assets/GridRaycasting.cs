using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridRaycasting : MonoBehaviour
{
    [System.Serializable] public class HillTapEvent : UnityEvent<Collider> { }
    [System.Serializable] public class RoadTapEvent : UnityEvent<Collider> { }
    [System.Serializable] public class HideMenuEvent : UnityEvent { }

    [SerializeField] Texture2D selectedTex;
    [SerializeField] HillTapEvent hillTapEvent = new HillTapEvent();
    [SerializeField] RoadTapEvent roadTapEvent = new RoadTapEvent();
    [SerializeField] HideMenuEvent hideMenuEvent = new HideMenuEvent();
    // Use this for initialization


    // Update is called once per frame
    void Update()
    {
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "TerrainHills")
                    {

                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "TerrainHills")
                    {
                        hillTapEvent.Invoke(hit.collider);
                    }
                    else if (hit.collider.tag == "TerrainRoad")
                    {
                        roadTapEvent.Invoke(hit.collider);
                    }
                    else
                    {
                        hideMenuEvent.Invoke();
                    }
                }
            }
        }
    }
}
