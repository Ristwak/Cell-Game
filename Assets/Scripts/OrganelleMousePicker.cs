using UnityEngine;

public class OrganelleMousePicker : MonoBehaviour
{
    private Camera cam;
    private OrganelleItem selectedItem;
    private Vector3 offset;
    private float zDistance = 2f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickItem();
        }
        else if (Input.GetMouseButton(0) && selectedItem != null)
        {
            DragItem();
        }
        else if (Input.GetMouseButtonUp(0) && selectedItem != null)
        {
            DropItem();
        }
    }

    void TryPickItem()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            OrganelleItem item = hit.collider.GetComponent<OrganelleItem>();
            if (item != null)
            {
                selectedItem = item;
                offset = selectedItem.transform.position - hit.point;
            }
        }
    }

    void DragItem()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPos = ray.GetPoint(zDistance) + offset;
        selectedItem.transform.position = Vector3.Lerp(selectedItem.transform.position, targetPos, Time.deltaTime * 10f);
    }

    void DropItem()
    {
        selectedItem = null;
    }
}
