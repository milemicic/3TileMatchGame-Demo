using UnityEngine;

public class PrefabGenerator : MonoBehaviour
{
    public GameObject prefab; // Prefab to instantiate
    public int numberOfPrefabs; // Number of prefabs to generate
    public float spaceBetweenPrefabs; // Space between prefabs

    void Start()
    {
        GeneratePrefabs();
    }

    void GeneratePrefabs()
    {
        // Calculate total width of all prefabs and spaces
        float totalWidth = (prefab.transform.localScale.x + spaceBetweenPrefabs) * numberOfPrefabs;

        // Calculate starting position
        Vector3 startPos = transform.position - new Vector3(totalWidth / 2f, 0f, 0f);

        // Generate prefabs
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 prefabPosition = startPos + new Vector3(i * (prefab.transform.localScale.x + spaceBetweenPrefabs), 0f, 0f);
            Instantiate(prefab, prefabPosition, Quaternion.identity, transform);
        }
    }
}