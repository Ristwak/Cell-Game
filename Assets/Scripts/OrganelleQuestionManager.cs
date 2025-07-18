using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using System;

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
    [Header("UI")]
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI functionText;
    public string jsonFileName = "CellSafariOrganelles"; // without .json

    [Header("State")]
    private List<OrganelleData> organelles;
    private int currentIndex = 0;
    private string correctAnswer;

    private List<OrganelleInteractionLock> allOrganelles = new List<OrganelleInteractionLock>();

    // ✅ Static event and correct organelle info for other scripts to subscribe
    public static string currentCorrectOrganelle;
    public static event Action<string> OnCorrectOrganelleChanged;

    void Start()
    {
        LoadQuestions();
        ShuffleQuestions();
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
            int rand = UnityEngine.Random.Range(i, organelles.Count);
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

        currentCorrectOrganelle = correctAnswer;
        OnCorrectOrganelleChanged?.Invoke(currentCorrectOrganelle); // 🔥 Trigger the event

        UpdateGrabbableOrganelleStates();
    }

    void UpdateGrabbableOrganelleStates()
    {
        foreach (var item in allOrganelles)
        {
            item.SetGrabbable(item.organelleName == correctAnswer);
        }
    }

    void DisableAllGrabbables()
    {
        foreach (var item in allOrganelles)
        {
            item.SetGrabbable(false);
        }
    }

    public void OnCorrectOrganallePlaced()
    {
        currentIndex++;
        ShowCurrentQuestion();
    }

    // Called by OrganelleInteractionLock during Start()
    public void RegisterOrganelle(OrganelleInteractionLock organelle)
    {
        if (!allOrganelles.Contains(organelle))
        {
            allOrganelles.Add(organelle);
        }
    }
}
