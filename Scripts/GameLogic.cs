using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using UnityEngine.UI; 
using System.Collections; 
using System.IO;  

public class GameLogic : MonoBehaviour
{
    public TextMeshProUGUI wordLabel;  
    public TextMeshProUGUI timerLabel;  
    public GameObject feedbackPanel;  
    public TextMeshProUGUI feedbackText;  
    public Image feedbackBackground;  
    public AudioSource countdownSound;  // For countdown sound
    //public AudioClip countdownClip;     // Add an AudioClip for the countdown sound

    private string[] words;
    private float gameTime;
    private float timeRemaining;
    private float accelerometerThreshold = 0.5f;
    private bool showingFeedback = false;  

    private string[] correctAnswersArray = new string[100]; 
    private string[] passAnswersArray = new string[100];    
    private int correctCount = 0;  
    private int passCount = 0;  	

    private WordList wordList;

    // Class to handle the word list from JSON
    [System.Serializable]
    public class WordList
    {
        public string[] words;
    }

    void Start()
    {
        // Load the word list based on the selected category
        LoadWordsFromJSON(MenuManager.selectedCategory);

        // Get the selected time from TimeSelectionManager
        gameTime = TimeSelectionManager.selectedTime * 60; 
        timeRemaining = gameTime;

        ShowRandomWord();
    }

    void LoadWordsFromJSON(string category)
    {
        // Create the file name based on the category
        string fileName = "";

        if (category == "Movies")
        {
            fileName = "movies";
        }
        else if (category == "Animals")
        {
            fileName = "animals";
        }
        else if (category == "Things")
        {
            fileName = "things";
        }
        else
        {
            Debug.LogError("Invalid category selected.");
            return;
        }

        // Load the JSON file from the Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if (jsonFile != null)
        {
			Debug.Log("I am in jsonFile!=null");
            wordList = JsonUtility.FromJson<WordList>(jsonFile.text);
            words = wordList.words;
        }
        else
        {
            Debug.LogError(fileName + " JSON file not found.");
        }
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
            PlayCountdownSound(); // Play countdown sound if necessary
        }
        else
        {
            EndGame();
        }

        if (!showingFeedback)
        {
            Vector3 acceleration = Input.acceleration;

            if (acceleration.z > accelerometerThreshold)
            {
                correctAnswersArray[correctCount++] = wordLabel.text;  
                ShowFeedback("Correct!", Color.green);
            }
            else if (acceleration.z < -accelerometerThreshold)
            {
                passAnswersArray[passCount++] = wordLabel.text;  
                ShowFeedback("Pass!", Color.red);
            }
        }
    }

    void PlayCountdownSound()
    {
        if (timeRemaining <= 6 && countdownSound != null && !countdownSound.isPlaying)
        {
            
            countdownSound.Play();
        }
    }

    void ShowRandomWord()
    {
        if (words != null && words.Length > 0)
        {
			Debug.Log("I am in words!=null");
            int randomIndex = Random.Range(0, words.Length);
            string word = words[randomIndex];
            wordLabel.text = word;
        }
        else
        {
            Debug.LogError("No words loaded.");
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerLabel != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void ShowFeedback(string message, Color color)
    {
        feedbackText.text = message;
        feedbackPanel.GetComponent<Image>().color = color;
        feedbackPanel.SetActive(true);
        showingFeedback = true;
        Invoke("HideFeedback", 1f);
    }

    void HideFeedback()
    {
        feedbackPanel.SetActive(false); 
        ShowRandomWord();  
        showingFeedback = false;  
    }

    void EndGame()
    {
        Debug.Log("Time's up!");
        Screen.orientation = ScreenOrientation.Portrait;

        System.Array.Resize(ref correctAnswersArray, correctCount);
        System.Array.Resize(ref passAnswersArray, passCount);

        ResultManager.correctAnswers = correctAnswersArray;
        ResultManager.passAnswers = passAnswersArray;

        SceneManager.LoadScene("ThankYouScene");
    }
}
