using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    public float speed;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        bool isUIHovered = false;
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                isUIHovered = true;
                break;
            }
        }

        if (!isUIHovered)
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * speed * Time.deltaTime);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5f, 5f), Mathf.Clamp(transform.position.y, -5f, 5f), transform.position.z);
        }
    }
}
