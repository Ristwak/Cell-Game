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

    void OnEnable()
    {
        QuestionManager.OnCorrectOrganelleChanged += HandleOrganelleChange;
    }

    void OnDisable()
    {
        QuestionManager.OnCorrectOrganelleChanged -= HandleOrganelleChange;
    }

    void HandleOrganelleChange(string currentCorrectName)
    {
        grabInteractable.enabled = organelleName == currentCorrectName;
    }
}
