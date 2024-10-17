using UnityEngine;
using UnityEngine.UI; // For UI components
using UnityEngine.SceneManagement; // For scene management
using TMPro; // For TextMeshPro

public class TimeSelectionManager : MonoBehaviour
{
    public Slider timeSlider; // Reference to the Slider component
    public TextMeshProUGUI selectedTimeText; // Reference to the TextMeshProUGUI for displaying the selected time
    public static float selectedTime;  // Static variable to store the selected time

    void Start()
    {
        // Set the slider's min and max values
        timeSlider.minValue = 1; // Minimum value
        timeSlider.maxValue = 5; // Maximum value

        // Set the initial value
        timeSlider.value = 1; // Default to 1 minute

        // Update the display at the start
        OnSliderValueChanged(timeSlider.value);

        // Add a listener to call OnSliderValueChanged whenever the slider value changes
        timeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // Function to update the displayed text when the slider value changes
    public void OnSliderValueChanged(float value)
    {
        // Update the text to show the currently selected time
        selectedTimeText.text = "Selected Time: " + value + " minute(s)";
    }

    // Function to handle the confirmation of time selection
    public void ConfirmTime()
    {
        // Store the selected time when the button is clicked
        selectedTime = timeSlider.value;
        Debug.Log("Selected Time: " + selectedTime + " minute(s)");

        // Load the Game scene
        SceneManager.LoadScene("CountdownScene");
    }
}
