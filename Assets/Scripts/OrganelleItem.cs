using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OrganelleItem : MonoBehaviour
{
    public string organelleName;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        organelleName = gameObject.name;
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Check if it's dropped in a valid zone — this is just a placeholder
        if (!IsInValidDropZone())
        {
            ReturnToInitialPosition();
        }
    }

    private bool IsInValidDropZone()
    {
        // You can later replace this with actual logic: trigger area, tag, etc.
        return false; // For now, always return to initial position
    }

    private void ReturnToInitialPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
