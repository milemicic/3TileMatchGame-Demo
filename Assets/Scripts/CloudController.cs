using UnityEngine;

public class CloudController : MonoBehaviour
{
    public Transform[] clouds; // Array of cloud transforms
    public float speed = 1f; // Speed of cloud movement
    public float startX = 10f; // Start position off-screen to the right
    public float endX = -10f; // End position off-screen to the left
    public int ChooseTheFrontCloud = 0; // Index of the cloud that will be in the foreground
    public float noiseAmplitude = 0.1f; // Amplitude of vertical noise
    public float noiseFrequency = 1f; // Frequency of vertical noise

    private Vector3[] initialPositions; // Store initial positions to calculate noise

    void Start()
    {
        initialPositions = new Vector3[clouds.Length];

        for (int i = 0; i < clouds.Length; i++)
        {
            float randomStartX = Random.Range(startX, endX);
            clouds[i].position = new Vector3(randomStartX, clouds[i].position.y, clouds[i].position.z);
            initialPositions[i] = clouds[i].position; // Store the initial position

            // Set alpha and sorting layer for the foreground cloud
            SpriteRenderer spriteRenderer = clouds[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                if (i == ChooseTheFrontCloud)
                {
                    Color color = spriteRenderer.color;
                    color.a = 0.6f; // 60% alpha
                    spriteRenderer.color = color;
                    spriteRenderer.sortingLayerName = "Foreground"; // Ensure the cloud is in the Foreground layer
                }
                else
                {
                    spriteRenderer.sortingLayerName = "Midground"; // Other clouds in the Midground layer
                }
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            // Move cloud to the left based on the shared speed
            clouds[i].position += Vector3.left * speed * Time.deltaTime;

            // Add vertical noise for natural movement
            float noise = Mathf.Sin(Time.time * noiseFrequency + i) * noiseAmplitude;
            clouds[i].position = new Vector3(clouds[i].position.x, initialPositions[i].y + noise, clouds[i].position.z);

            // If the cloud is past the end position, reset it to the start position
            if (clouds[i].position.x < endX)
            {
                clouds[i].position = new Vector3(startX, initialPositions[i].y + noise, clouds[i].position.z);
            }
        }
    }
}