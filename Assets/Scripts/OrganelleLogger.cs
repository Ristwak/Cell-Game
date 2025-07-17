using UnityEngine;

public class OrganelleLogger : MonoBehaviour
{
    public GameObject[] organelles;

    void Start()
    {
        foreach (GameObject organelle in organelles)
        {
            Vector3 pos = organelle.transform.localPosition;
            Debug.Log($"\"{organelle.name}\" : [{pos.x:F2}, {pos.y:F2}, {pos.z:F2}]");
        }
    }
}
