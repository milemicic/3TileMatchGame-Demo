using UnityEngine;

public class AdjustUiLayout : MonoBehaviour
{
    public RectTransform bottomLayout;    // Reference to the RectTransform of the bottom layout
    public RectTransform centerLayout;    // Reference to the RectTransform of the center layout
    public RectTransform buttonsLayout;   // Reference to the RectTransform of the buttons layout

    private int previousWidth;
    private int previousHeight;

    void Start()
    {
        AdjustUILayout();
    }

    void AdjustUILayout()
    {
        // Check if the references are assigned
        if (bottomLayout == null || centerLayout == null || buttonsLayout == null)
        {
            Debug.LogError("References are not assigned in the inspector.");
            return;
        }

        float aspectRatio = (float)Screen.width / Screen.height;

        // Adjust Bottom Layout Height and Scale
        if (aspectRatio >= 1.7f)  // Example condition for wider screens and 16:9
        {
            bottomLayout.sizeDelta = new Vector2(bottomLayout.sizeDelta.x, 100);
            bottomLayout.localScale = new Vector3(1f, 1f, 1);
        }
        else  // Condition for taller screens (e.g., mobile)
        {
            bottomLayout.sizeDelta = new Vector2(bottomLayout.sizeDelta.x, 150);
            bottomLayout.localScale = new Vector3(1.2f, 1.2f, 1);  // Adjusted for taller screens
        }

        // Adjust Center Layout Size
        if (aspectRatio >= 1.7f)  // Example condition for wider screens (16:9)
        {
            centerLayout.localScale = new Vector3(1.1f, 1.1f, 1);
        }
        else  // Condition for taller screens (mobile)
        {
            centerLayout.localScale = new Vector3(1.6f, 1.6f, 1);  // Adjusted for taller screens
        }

        // Adjust Buttons Layout Scale
        if (aspectRatio >= 1.7f)  // Example condition for wider screens
        {
            buttonsLayout.localScale = new Vector3(1f, 1f, 1);
        }
        else  // Condition for taller screens
        {
            buttonsLayout.localScale = new Vector3(1f, 1f, 1);  // Adjusted for taller screens
        }
    }

    void Update()
    {
        // Adjust on screen size change
        if (Screen.width != previousWidth || Screen.height != previousHeight)
        {
            previousWidth = Screen.width;
            previousHeight = Screen.height;
            AdjustUILayout();
        }
    }
}
