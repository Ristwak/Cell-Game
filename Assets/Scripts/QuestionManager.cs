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
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI functionText;
    public static string currentCorrectOrganelle;
    public static event System.Action<string> OnCorrectOrganelleChanged;

    public string jsonFileName = "CellSafariOrganelles";
    private List<OrganelleData> organelles;
    private List<OrganelleData> unusedQuestions;

    private MainGameManager gameManager;  // ✅ Reference to MainGameManager

    void Start()
    {
        gameManager = FindObjectOfType<MainGameManager>();  // ✅ Auto find the GameManager

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

            // ✅ Call MainGameManager's method
            if (gameManager != null)
                gameManager.DeactivateGameObjects();
            else
                Debug.LogWarning("⚠️ MainGameManager not found!");

            return;
        }

        int randomIndex = Random.Range(0, unusedQuestions.Count);
        OrganelleData current = unusedQuestions[randomIndex];
        unusedQuestions.RemoveAt(randomIndex);

        questionText.text = current.description;
        functionText.text = current.function;

        currentCorrectOrganelle = !string.IsNullOrEmpty(current.correctAnswer)
            ? current.correctAnswer
            : current.correctName;

        OnCorrectOrganelleChanged?.Invoke(currentCorrectOrganelle);
    }

    public bool CheckAnswer(OrganelleItem item)
    {
        return item.organelleName.Trim().ToLower() == QuestionManager.currentCorrectOrganelle.Trim().ToLower();
    }
}
