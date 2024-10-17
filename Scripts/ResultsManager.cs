using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.SceneManagement; // Add this line for SceneManager functionality
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public GameObject answerTextPrefab;  // Drag your AnswerTextPrefab here in the Inspector
    public Transform contentArea;        // Assign the "Content" of the Scroll View here
    public GameObject medalIcon;         // Reference to the medal icon

    // Declare static fields for correct and pass answers so they can be accessed across scenes
    public static string[] correctAnswers;
    public static string[] passAnswers;
	public TextMeshProUGUI correctAnswersText;  // Reference to the TextMeshPro for displaying correct answers count

    void Start()
    {
        // Display the results when the scene loads
        DisplayResults(correctAnswers, passAnswers);
    }

    public void DisplayResults(string[] correctAnswers, string[] passAnswers)
{
    // Check if the prefab is assigned
    if (answerTextPrefab == null)
    {
        Debug.LogError("answerTextPrefab is not assigned in the Inspector.");
        return; // Exit the method early
    }

    // Display correct answers in green
    foreach (var answer in correctAnswers)
    {
        GameObject answerText = Instantiate(answerTextPrefab, contentArea);
        TextMeshProUGUI textComponent = answerText.GetComponent<TextMeshProUGUI>();
        textComponent.text = answer;
        textComponent.color = Color.green; // Set color to green
    }

    // Display pass answers in red
    foreach (var answer in passAnswers)
    {
        GameObject answerText = Instantiate(answerTextPrefab, contentArea);
        TextMeshProUGUI textComponent = answerText.GetComponent<TextMeshProUGUI>();
        textComponent.text = answer;
        textComponent.color = Color.red; // Set color to red
    }

    // Update medal icon with the correct answer count
    int correctAnswersCount = correctAnswers.Length;
    UpdateMedalIcon(correctAnswersCount);
}


    private void UpdateMedalIcon(int correctCount)
	{
		if (correctAnswersText != null)  // Check if the text reference is assigned
		{
			// Update the text on the screen with the number of correct answers
			correctAnswersText.text = "Score: " + correctCount.ToString();
		}
		else
		{
			Debug.LogError("CorrectAnswersText is not assigned in the Inspector.");
		}
	}


    // Function to return to the main menu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");  // Load the main menu scene
    }
}
