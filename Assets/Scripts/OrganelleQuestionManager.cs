using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[System.Serializable]
public class OrganelleData
{
    public string name;
    public string description;
    public string function;
    public string correctAnswer; // optional
    public string correctName;   // optional
}

[System.Serializable]
public class OrganelleList
{
    public List<OrganelleData> organelles;
}

public class OrganelleQuestionManager : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI functionText;
    public string jsonFileName = "CellSafariOrganelles"; // without .json

    private List<OrganelleData> organelles;
    private int currentIndex = 0;
    private string correctAnswer;

    void Start()
    {
        LoadQuestions();
        ShowCurrentQuestion();
    }

    void LoadQuestions()
    {
        TextAsset jsonText = Resources.Load<TextAsset>(jsonFileName);
        if (jsonText != null)
        {
            organelles = JsonUtility.FromJson<OrganelleList>(jsonText.text).organelles;
        }
        else
        {
            Debug.LogError("❌ JSON file not found in Resources!");
        }
    }

    void ShowCurrentQuestion()
    {
        if (currentIndex >= organelles.Count)
        {
            Debug.Log("✅ All questions completed!");
            return;
        }

        OrganelleData current = organelles[currentIndex];

        descriptionText.text = $"{current.description}";
        functionText.text = $"{current.function}";

        // Decide correct answer
        correctAnswer = !string.IsNullOrEmpty(current.correctAnswer) ? current.correctAnswer : current.correctName;
        Debug.Log("Correct organelle to select: " + correctAnswer);
    }

    // This should be called by each organelle when clicked
    public void OnOrganelleSelected(GameObject selectedObj)
    {
        string selectedName = selectedObj.name;

        if (selectedName == correctAnswer)
        {
            Debug.Log("✅ Correct organelle selected!");

            // Apply correct animation
            PlayFeedback(selectedObj, true);
        }
        else
        {
            Debug.Log($"❌ Incorrect organelle. Selected: {selectedName}, Expected: {correctAnswer}");

            // Apply wrong animation
            PlayFeedback(selectedObj, false);
        }

        currentIndex++;
        ShowCurrentQuestion();
    }

    void PlayFeedback(GameObject obj, bool isCorrect)
    {
        // ✅ Trigger VR haptic
        // You may use OVRInput / XR toolkit or OpenXR's Haptics here.
        // Placeholder:
        // HapticManager.Instance.TriggerHapticPulse();

        // ✅ Play Audio
        AudioSource audio = obj.GetComponent<AudioSource>();
        if (audio != null)
        {
            AudioClip clip = isCorrect ? Resources.Load<AudioClip>("Audio/correct") : Resources.Load<AudioClip>("Audio/wrong");
            audio.PlayOneShot(clip);
        }

        // ✅ Trigger Animation
        Animator anim = obj.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger(isCorrect ? "Correct" : "Wrong");
        }
        else
        {
            // fallback: simple scale bounce
            LeanTween.scale(obj, obj.transform.localScale * 1.2f, 0.2f).setEasePunch();
        }
    }
}
