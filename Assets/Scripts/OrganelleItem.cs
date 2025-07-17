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

    public void LockToPosition(Vector3 correctPos)
    {
        transform.position = correctPos;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        // Optional: disable grabbing
    }

    public void ReturnToOriginal()
    {
        transform.position = initialPosition;
        // Optional: shake or flash red
    }
}
