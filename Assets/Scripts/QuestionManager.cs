using UnityEngine;
using TMPro;
using System.Collections.Generic;

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

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;  // ✅ Will show description now
    public TextMeshProUGUI functionText;  // ✅ Optional, shows function
    public static string currentCorrectOrganelle;
    public static event System.Action<string> OnCorrectOrganelleChanged;

    public string jsonFileName = "CellSafariOrganelles"; // Without .json
    private List<OrganelleData> organelles;
    private List<OrganelleData> unusedQuestions;

    void Start()
    {
        LoadQuestions();
        if (organelles != null && organelles.Count > 0)
        {
            unusedQuestions = new List<OrganelleData>(organelles);
            ShowNextQuestion();
        }
        else
        {
            questionText.text = "❌ No organelle data loaded.";
            functionText.text = "";
        }
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

    public void ShowNextQuestion()
    {
        if (unusedQuestions == null || unusedQuestions.Count == 0)
        {
            questionText.text = "🎉 All organelles placed!";
            functionText.text = "";
            return;
        }

        int randomIndex = Random.Range(0, unusedQuestions.Count);
        OrganelleData current = unusedQuestions[randomIndex];
        unusedQuestions.RemoveAt(randomIndex);

        // ✅ Use JSON description as the question
        questionText.text = current.description;

        // ✅ Optionally show function if field exists
        functionText.text = current.function;

        // ✅ Update correct organelle reference
        currentCorrectOrganelle = !string.IsNullOrEmpty(current.correctAnswer)
            ? current.correctAnswer
            : current.correctName;

        OnCorrectOrganelleChanged?.Invoke(currentCorrectOrganelle);
    }
}
