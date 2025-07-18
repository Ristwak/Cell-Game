using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class OrganelleData
{
    public string name;
    public string description;
    public string function;
    public string correctAnswer;
    public string correctName;
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

    private OrganelleItem[] allOrganelles;

    void Start()
    {
        LoadQuestions();
        ShuffleQuestions();

        allOrganelles = FindObjectsOfType<OrganelleItem>();
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

    void ShuffleQuestions()
    {
        for (int i = 0; i < organelles.Count; i++)
        {
            OrganelleData temp = organelles[i];
            int rand = Random.Range(i, organelles.Count);
            organelles[i] = organelles[rand];
            organelles[rand] = temp;
        }
    }

    void ShowCurrentQuestion()
    {
        if (currentIndex >= organelles.Count)
        {
            descriptionText.text = "🎉 All questions completed!";
            functionText.text = "";
            DisableAllGrabbables();
            return;
        }

        OrganelleData current = organelles[currentIndex];
        descriptionText.text = current.description;
        functionText.text = current.function;
        correctAnswer = !string.IsNullOrEmpty(current.correctAnswer) ? current.correctAnswer : current.correctName;

        UpdateGrabbableOrganelleStates();
    }

    void UpdateGrabbableOrganelleStates()
    {
        foreach (var item in allOrganelles)
        {
            var grab = item.GetComponent<XRGrabInteractable>();
            if (grab != null)
            {
                grab.enabled = (item.organelleName == correctAnswer);
            }
        }
    }

    void DisableAllGrabbables()
    {
        foreach (var item in allOrganelles)
        {
            var grab = item.GetComponent<XRGrabInteractable>();
            if (grab != null)
            {
                grab.enabled = false;
            }
        }
    }

    public void OnCorrectOrganallePlaced()
    {
        currentIndex++;
        ShowCurrentQuestion();
    }
}
