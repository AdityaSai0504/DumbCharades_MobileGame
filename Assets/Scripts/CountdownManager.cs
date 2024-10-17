using UnityEngine;
using TMPro; // For TextMeshProUGUI
using UnityEngine.SceneManagement; // For loading scenes
using System.Collections; // Required for IEnumerator

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // Reference to the countdown text
    private int countdownDuration = 5;  // Countdown duration (in seconds)
	public AudioSource countdownSoundSource;  // For countdown sound
    void Start()
    {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
        StartCoroutine(StartCountdown()); // Start the countdown when the scene loads
    }

    private IEnumerator StartCountdown()
    {
        int countdown = countdownDuration;  // Start countdown from the specified duration
		countdownText.text = "Get Ready!";
        // Countdown loop
        while (countdown > 0)
        {
			if (countdownSoundSource != null && !countdownSoundSource.isPlaying)
			{
				countdownSoundSource.Play();
			}
            countdownText.text = countdown.ToString();  // Update countdown text on screen
            yield return new WaitForSeconds(1);  // Wait for 1 second
            countdown--;  // Decrease countdown by 1
        }

        // After countdown ends, load the Game scene
        countdownText.text = "Go!";  // Display "Go!" for 1 second
        yield return new WaitForSeconds(1);  // Wait for 1 second
        SceneManager.LoadScene("Game");  // Load the actual game scene
    }
}
