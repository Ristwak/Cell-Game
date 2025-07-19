using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OrganelleItem : MonoBehaviour
{
    public string organelleName;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private XRGrabInteractable grabInteractable;
    private bool isCorrectDrop = false;
    private bool wasReleased = false;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        organelleName = gameObject.name;

        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnRelease);
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnRelease);
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isCorrectDrop = false;
        wasReleased = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        wasReleased = true;

        // Check with QuestionManager if this is the correct organelle
        QuestionManager manager = FindObjectOfType<QuestionManager>();
        if (manager != null)
        {
            isCorrectDrop = manager.CheckAnswer(this);
        }

        if (!isCorrectDrop)
        {
            // Wait a frame to allow Unity physics to settle, then reset
            Invoke(nameof(ResetPosition), 0.1f);
        }
    }

    private void Update()
    {
        // Safety fallback: If released and falls too far (or stuck), reset
        if (wasReleased && !isCorrectDrop && transform.position.y < -1f)
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        wasReleased = false;
    }
}
