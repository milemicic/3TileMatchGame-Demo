using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    [Range(0.1f, 1f)] public float transitionTime = 0.5f; // Allow adjusting in the inspector with a slider
    public bool instantTransition = false; // Toggle for instant transition

#if UNITY_EDITOR
    [SerializeField]
    private Button casualGameButton;

    [SerializeField]
    private Button backButton;
#endif

    void Start()
    {
        AssignButtonsBasedOnScene();
        if (!instantTransition)
        {
            AdjustAnimationSpeed();
        }
    }

    void AssignButtonsBasedOnScene()
    {
        if (SceneManager.GetActiveScene().name == "HomeScreen")
        {
#if UNITY_EDITOR
            if (casualGameButton == null)
            {
                Debug.LogError("Casual Game Button is not assigned in the inspector.");
            }
#endif
            casualGameButton.onClick.AddListener(LoadNextLevel);
        }
        else if (SceneManager.GetActiveScene().name == "GameScene")
        {
#if UNITY_EDITOR
            if (backButton == null)
            {
                Debug.LogError("Back Button is not assigned in the inspector.");
            }
#endif
            backButton.onClick.AddListener(LoadPreviousLevel);
        }
    }

    void AdjustAnimationSpeed()
    {
        // The default animation duration is 1 second, so the speed multiplier is 1 / transitionTime
        transition.speed = 1f / transitionTime;
    }

    public void LoadNextLevel()
    {
        if (instantTransition)
        {
            // Instant transition
            SceneManager.LoadScene(1); // Assuming GameScene is index 1
        }
        else
        {
            StartCoroutine(LoadLevel(1)); // Assuming GameScene is index 1
        }
    }

    public void LoadPreviousLevel()
    {
        if (instantTransition)
        {
            // Instant transition
            SceneManager.LoadScene(0); // Assuming HomeScreen is index 0
        }
        else
        {
            StartCoroutine(LoadLevel(0)); // Assuming HomeScreen is index 0
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (!instantTransition)
        {
            AdjustAnimationSpeed(); // Adjust the animation speed before starting the transition
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }
        SceneManager.LoadScene(levelIndex);
        if (!instantTransition)
        {
            transition.speed = 1f; // Reset the transition speed back to default after loading the scene
        }
    }
}
