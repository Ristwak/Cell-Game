using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OrganelleInteractionLock : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public string organelleName;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        organelleName = gameObject.name;
    }

    private void Start()
    {
        // Register with manager
        OrganelleQuestionManager manager = FindObjectOfType<OrganelleQuestionManager>();
        if (manager != null)
        {
            manager.RegisterOrganelle(this);
        }
    }

    // Called by manager to set whether this organelle is currently grabbable
    public void SetGrabbable(bool isEnabled)
    {
        grabInteractable.enabled = isEnabled;
    }
}
