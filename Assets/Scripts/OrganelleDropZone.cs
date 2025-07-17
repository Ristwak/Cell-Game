using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OrganelleDropZone : MonoBehaviour
{
    public string expectedOrganelleName;
    public Material correctMaterial;
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        OrganelleItem item = other.GetComponent<OrganelleItem>();
        Debug.Log($"OrganelleDropZone: Triggered with {other.gameObject.name}");

        if (item != null)
        {
            bool isCorrect = item.organelleName == expectedOrganelleName;

            // Vibrate the controller (simple haptic)
            XRBaseControllerInteractor interactor = other.GetComponentInParent<XRBaseControllerInteractor>();
            if (interactor != null)
            {
                interactor.SendHapticImpulse(0.5f, 0.2f); // amplitude, duration
            }

            // Play sound
            audioSource.clip = isCorrect ? correctSound : incorrectSound;
            audioSource.Play();

            // Animate this drop zone
            if (isCorrect)
            {
                StartCoroutine(AnimateBounce());
            }
            else
            {
                StartCoroutine(AnimateShake());
            }

            // Destroy the dropped object if correct
            if (isCorrect)
            {
                Destroy(other.gameObject);

                // Change material of all matching organelle visuals
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

    private System.Collections.IEnumerator AnimateBounce()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 bounceScale = originalScale * 1.1f;
        float time = 0f;

        while (time < 0.2f)
        {
            transform.localScale = Vector3.Lerp(originalScale, bounceScale, time / 0.2f);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;
        while (time < 0.2f)
        {
            transform.localScale = Vector3.Lerp(bounceScale, originalScale, time / 0.2f);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }

    private System.Collections.IEnumerator AnimateShake()
    {
        Vector3 originalPos = transform.localPosition;
        float duration = 0.3f;
        float strength = 0.02f;
        float time = 0f;

        while (time < duration)
        {
            float x = Random.Range(-strength, strength);
            float y = Random.Range(-strength, strength);
            transform.localPosition = originalPos + new Vector3(x, y, 0);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
