using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] GameObject currentSelectedObject;
    [SerializeField] bool isDragging = false;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float turnSpeed;
    [SerializeField] Image arrow;
    [SerializeField] Projector projector;

    Camera camera;
    float maxX;
    float minX;
    float maxZ;
    float minZ;

    private void Start()
    {
        maxX = projector.transform.position.x + projector.orthographicSize / 2;
        minX = projector.transform.position.x - projector.orthographicSize / 2;
        maxZ = projector.transform.position.z + projector.orthographicSize * ((1 / projector.aspectRatio) / 2);
        minZ = projector.transform.position.z - projector.orthographicSize * ((1 / projector.aspectRatio) / 2);

        Debug.Log("maxX " + maxX + " minX " + minX + " maxZ " + maxZ + " minZ " + minZ);

        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.layer == 10)
                    {
                        currentSelectedObject = hit.collider.gameObject;
                        ShowSelected();
                        isDragging = true;
                    }
                    else if (hit.collider.gameObject.layer == 9)
                    {
                        currentSelectedObject = null;
                        HideSelected();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
            Drag();

        if (arrow.enabled == true)
        {
            Vector3 pos = new Vector3(currentSelectedObject.transform.position.x,
                currentSelectedObject.transform.position.y + 3f,
                currentSelectedObject.transform.position.z);
            arrow.GetComponent<RectTransform>().position = camera.WorldToScreenPoint(pos);
        }
    }

    void Drag()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            if (currentSelectedObject == null)
                return;

            if (hit.collider.gameObject.layer == 9)
            {
                Vector3 pos = new Vector3(Mathf.Clamp(hit.point.x, minX, maxX),
                    hit.point.y,
                    Mathf.Clamp(hit.point.z, minZ, maxZ));
                currentSelectedObject.transform.position = pos;
            }
        }
    }

    public void StartTurn(float direction)
    {
        if (currentSelectedObject == null)
            return;

        StartCoroutine(Turn(direction));
    }

    public IEnumerator Turn(float direction)
    {
        while (true)
        {
            currentSelectedObject.transform.Rotate(Vector3.up, Time.deltaTime * direction * turnSpeed);
            yield return null;
        }
    }

    public void EndTurn()
    {
        StopAllCoroutines();
    }

    public void ShowSelected()
    {
        arrow.enabled = true;
    }

    public void HideSelected()
    {
        arrow.enabled = false;
    }
}
