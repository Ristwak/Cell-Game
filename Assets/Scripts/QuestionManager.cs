using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public List<string> organelleNames;
    public static string currentCorrectOrganelle;
    public static event System.Action<string> OnCorrectOrganelleChanged;

    private List<string> unusedQuestions;

    void Start()
    {
        unusedQuestions = new List<string>(organelleNames);
        ShowNextQuestion();
    }

    public void ShowNextQuestion()
    {
        if (unusedQuestions.Count == 0)
        {
            questionText.text = "All organelles placed!";
            return;
        }

        int randomIndex = Random.Range(0, unusedQuestions.Count);
        currentCorrectOrganelle = unusedQuestions[randomIndex];
        unusedQuestions.RemoveAt(randomIndex);

        questionText.text = $"Place the {currentCorrectOrganelle}";
        OnCorrectOrganelleChanged?.Invoke(currentCorrectOrganelle);
    }
}
