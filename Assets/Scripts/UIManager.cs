using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject topUI;           // Reference to the top UI panel
    public Button casualGameButton;    // Reference to the Casual Game button
    public Button jokesButton;         // Reference to the Jokes button
    public Animator transitionAnimator; // Reference to the transition animator

    [Range(0.1f, 1f)] public float transitionTime = 0.5f; // Transition time for scene loading
    public bool instantTransition = false;                // Toggle for instant transition

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        // Disable the Jokes button's interactivity and the top UI elements
        jokesButton.interactable = false;
        topUI.SetActive(false);
    }

    // Scene Loading
    public void LoadScene(string sceneName)
    {
        if (instantTransition)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            StartCoroutine(LoadSceneWithTransition(sceneName));
        }
    }

    private IEnumerator LoadSceneWithTransition(string sceneName)
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        SceneManager.LoadScene(sceneName);
    }

    // Method to be called on Casual Game button click
    public void OnCasualGameButtonClicked()
    {
        LoadScene("GameScene");
    }
}
