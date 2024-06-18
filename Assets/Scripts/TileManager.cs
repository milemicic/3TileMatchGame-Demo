using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour
{
    public GameObject horizontalBaseTilePrefab; // Assign the HorizontalBaseTile prefab in the Inspector
    public GameObject verticalBaseTilePrefab; // Assign the VerticalBaseTile prefab in the Inspector
    public Sprite[] fruitSprites; // Assign the fruit sprites in the Inspector

    // Public variables for customization
    public Vector3 horizontalTileScale = new Vector3(0.5f, 0.5f, 0.5f); // Scale of horizontal tiles
    public Vector3 verticalTileScale = new Vector3(0.5f, 0.5f, 0.5f); // Scale of vertical tiles
    public float horizontalTileOffset = 0.0f; // Horizontal offset between horizontal tiles
    public float verticalTileOffset = 0.0f; // Vertical offset between vertical tiles
    public float rowSpacing = 0.3f; // Spacing between rows
    public float columnSpacing = 0.7f; // Spacing between columns

    public int rows = 4; // Number of horizontal rows
    public int columns = 6; // Number of tiles per horizontal row

    void Start()
    {
        StartCoroutine(CreateVerticalColumns());
        StartCoroutine(CreateHorizontalTiles());
    }

    IEnumerator CreateHorizontalTiles()
    {
        float tileWidth = 0.3f; // Width of each tile including some spacing
        float startX = -1.45f; // Starting X position (adjust based on your scene)
        float startY = 2.15f; // Starting Y position (adjust based on your scene)
        float delay = 0.05f; // Delay between each tile appearance

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Instantiate a new HorizontalBaseTile
                GameObject newTile = Instantiate(horizontalBaseTilePrefab);

                // Calculate position for the tile
                float posX = startX + col * (tileWidth + horizontalTileOffset);
                float posY = startY - row * (tileWidth + rowSpacing); // Adjust vertical position for each row with increased separation

                // Set the position of the tile
                newTile.transform.position = new Vector3(posX, posY, 0);

                // Scale the tile down to almost invisible
                newTile.transform.localScale = Vector3.zero;

                // Animate the scale to create a pop effect
                StartCoroutine(ScaleTile(newTile, horizontalTileScale));

                // Ensure sorting layer and order are correct
                SpriteRenderer baseTileRenderer = newTile.GetComponent<SpriteRenderer>();
                if (baseTileRenderer != null)
                {
                    baseTileRenderer.sortingLayerName = "Foreground";
                    baseTileRenderer.sortingOrder = 1; // Base tile order
                }

                // Ensure the FruitSprite is also correctly sorted and assign a fruit sprite
                Transform fruitSprite = newTile.transform.Find("FruitSprite");
                if (fruitSprite != null)
                {
                    SpriteRenderer fruitRenderer = fruitSprite.GetComponent<SpriteRenderer>();
                    if (fruitRenderer != null)
                    {
                        fruitRenderer.sortingLayerName = "Foreground";
                        fruitRenderer.sortingOrder = 2; // Fruit sprite order
                        // Assign a random fruit sprite
                        fruitRenderer.sprite = fruitSprites[Random.Range(0, fruitSprites.Length)];
                        // Adjust the scale and position of the fruit sprite
                        fruitSprite.localScale = new Vector3(1, 1, 1);
                        fruitSprite.localPosition = new Vector3(-0.043f, 0.022f, 0);
                    }
                }

                // Parent the tile to the TileManager to keep hierarchy organized
                newTile.transform.SetParent(transform);

                // Wait for a short delay before spawning the next tile
                yield return new WaitForSeconds(delay);
            }
        }
    }

    IEnumerator CreateVerticalColumns()
    {
        int verticalRows = 7; // Number of tiles per vertical column
        int verticalColumns = 3; // Number of vertical columns
        float tileHeight = 0.3f; // Height of each tile including some spacing
        float startX = -1.1f; // Adjusted starting X position for columns (move to the right)
        float startY = 2.5f; // Starting Y position for columns (adjust based on your scene)
        float delay = 0.05f; // Delay between each tile appearance

        for (int col = 0; col < verticalColumns; col++)
        {
            for (int row = 0; row < verticalRows; row++)
            {
                // Instantiate a new VerticalBaseTile
                GameObject newTile = Instantiate(verticalBaseTilePrefab);

                // Calculate position for the tile
                float posX = startX + col * columnSpacing;
                float posY = startY - row * (tileHeight + verticalTileOffset); // Adjust vertical position for each row with increased separation

                // Set the position of the tile
                newTile.transform.position = new Vector3(posX, posY, 0);

                // Scale the tile down to almost invisible
                newTile.transform.localScale = Vector3.zero;

                // Animate the scale to create a pop effect
                StartCoroutine(ScaleTile(newTile, verticalTileScale));

                // Ensure sorting layer and order are correct
                SpriteRenderer baseTileRenderer = newTile.GetComponent<SpriteRenderer>();
                if (baseTileRenderer != null)
                {
                    baseTileRenderer.sortingLayerName = "Foreground";
                    baseTileRenderer.sortingOrder = 0; // Column tile order, behind the rows
                }

                // Ensure the FruitSprite is also correctly sorted and assign a fruit sprite
                Transform fruitSprite = newTile.transform.Find("FruitSprite");
                if (fruitSprite != null)
                {
                    SpriteRenderer fruitRenderer = fruitSprite.GetComponent<SpriteRenderer>();
                    if (fruitRenderer != null)
                    {
                        fruitRenderer.sortingLayerName = "Foreground";
                        fruitRenderer.sortingOrder = 1; // Fruit sprite order for columns
                        // Assign a random fruit sprite
                        fruitRenderer.sprite = fruitSprites[Random.Range(0, fruitSprites.Length)];
                        // Adjust the scale and position of the fruit sprite
                        fruitSprite.localScale = new Vector3(1, 1, 1);
                        fruitSprite.localPosition = new Vector3(-0.043f, 0.022f, 0);
                        // Apply darker color to the sprite to distinguish it
                        fruitRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1); // Darken the sprite
                    }
                }

                // Parent the tile to the TileManager to keep hierarchy organized
                newTile.transform.SetParent(transform);

                // Wait for a short delay before spawning the next tile
                yield return new WaitForSeconds(delay);
            }
        }
    }

    IEnumerator ScaleTile(GameObject tile, Vector3 targetScale)
    {
        float duration = 0.1f; // Duration of the scaling effect
        Vector3 initialScale = Vector3.zero; // Initial scale
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            tile.transform.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tile.transform.localScale = targetScale; // Ensure the final scale is set
    }
}
