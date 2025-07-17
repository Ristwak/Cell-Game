using UnityEngine;

public class OrganelleDropZone : MonoBehaviour
{
    public string expectedOrganelleName;
    public Material correctMaterial;

    private void OnTriggerEnter(Collider other)
    {
        OrganelleItem item = other.GetComponent<OrganelleItem>();
        Debug.Log($"OrganelleDropZone: Triggered with {other.gameObject.name}");
        if (item != null && item.organelleName == expectedOrganelleName)
        {
            // Destroy the held object
            Destroy(other.gameObject);

            // Find all GameObjects in the scene with the same name
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == expectedOrganelleName)
                {
                    Renderer rend = obj.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        rend.material = correctMaterial;
                    }
                }
            }
        }
    }
}
