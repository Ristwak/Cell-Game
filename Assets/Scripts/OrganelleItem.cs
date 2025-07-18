// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;

// public class OrganelleItem : MonoBehaviour
// {
//     private Vector3 initialPosition;
//     private Quaternion initialRotation;
//     private XRGrabInteractable grabInteractable;
//     private bool isCorrectDrop = false;

//     private void Awake()
//     {
//         initialPosition = transform.position;
//         initialRotation = transform.rotation;

//         grabInteractable = GetComponent<XRGrabInteractable>();
//         if (grabInteractable != null)
//         {
//             grabInteractable.selectExited.AddListener(OnRelease);
//             grabInteractable.selectEntered.AddListener(OnGrabbed);
//         }
//     }

//     private void OnDestroy()
//     {
//         if (grabInteractable != null)
//         {
//             grabInteractable.selectExited.RemoveListener(OnRelease);
//             grabInteractable.selectEntered.RemoveListener(OnGrabbed);
//         }
//     }

//     private void OnGrabbed(SelectEnterEventArgs args)
//     {
//         isCorrectDrop = false; // Reset flag
//     }

//     private void OnRelease(SelectExitEventArgs args)
//     {
//         OrganelleQuestionManager manager = FindObjectOfType<OrganelleQuestionManager>();

//         if (manager != null)
//         {
//             isCorrectDrop = manager.CheckAnswer(this.gameObject);
//         }

//         if (!isCorrectDrop)
//         {
//             ReturnToInitialPosition();
//         }
//     }

//     private void ReturnToInitialPosition()
//     {
//         transform.position = initialPosition;
//         transform.rotation = initialRotation;

//         Rigidbody rb = GetComponent<Rigidbody>();
//         if (rb != null)
//         {
//             rb.velocity = Vector3.zero;
//             rb.angularVelocity = Vector3.zero;
//         }
//     }
// }

using UnityEngine;

public class OrganelleItem : MonoBehaviour
{
    public string organelleName;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        organelleName = gameObject.name;
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
    }
}
