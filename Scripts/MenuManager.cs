using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class MenuManager : MonoBehaviour
{
    public static string selectedCategory;  // Store the selected category

    // Function to handle category selection
    public void SelectCategory(string category)
    {
        selectedCategory = category;  // Store the selected category
        SceneManager.LoadScene("TimeSelection");  // Load the game scene after selection
    }
}
