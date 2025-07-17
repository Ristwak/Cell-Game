using UnityEngine;

public class OrganelleItem : MonoBehaviour
{
    public string organelleName;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        organelleName = gameObject.name;
    }
}
