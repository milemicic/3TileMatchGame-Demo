using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum AnimationType
{
    Default,
    SlowFade
}

public class BottomBarManager : MonoBehaviour
{
    public Transform bottomBar; // Reference to the BottomBar
    public Transform tileCloneParent; // Reference to the new parent object
    public float tileSpacing = 100.0f; // Space between tiles
    public float horizontalOffset = 0.0f; // Horizontal offset for the initial position of the first tile
    public float verticalOffset = 0.0f; // Vertical offset for the initial position of the first tile
    public float tileMoveSpeed = 0.5f; // Speed of the tile movement animation
    public AnimationType animationType = AnimationType.Default; // Selected animation type
    public float slowFadeDuration = 0.5f; // Duration for the slow fade animation

    private List<GameObject> tiles = new List<GameObject>();
    private Dictionary<Sprite, List<GameObject>> groupedTiles = new Dictionary<Sprite, List<GameObject>>();
    private int currentOrderInLayer = 1; // Starting order in layer for the first tile

    public void AddTile(GameObject tile)
    {
        // Ensure the tile has a FruitSprite component to identify its type
        SpriteRenderer fruitSpriteRenderer = tile.transform.Find("FruitSprite").GetComponent<SpriteRenderer>();
        if (fruitSpriteRenderer == null)
        {
            Debug.LogError("Tile does not have a FruitSprite child with a SpriteRenderer.");
            return;
        }

        Sprite fruitSprite = fruitSpriteRenderer.sprite;

        // Group the tile with others of the same type
        if (!groupedTiles.ContainsKey(fruitSprite))
        {
            groupedTiles[fruitSprite] = new List<GameObject>();
        }

        // Insert the new tile at the end of the list of the same type
        groupedTiles[fruitSprite].Add(tile);

        // Reparent the tile to the new parent object
        tile.transform.SetParent(tileCloneParent, false);

        // Adjust the positions of all tiles
        AdjustTilePositions();

        // Ensure the sorting layer and order are correct for UI
        SortingGroup sortingGroup = tile.GetComponent<SortingGroup>();
        if (sortingGroup != null)
        {
            sortingGroup.sortingLayerName = "UI";
            sortingGroup.sortingOrder = currentOrderInLayer; // Set the current order in layer
        }

        // Adjust the tile's scale if necessary
        tile.transform.localScale = new Vector3(1, 1, 1);

        // Start the movement animation
        Vector3 targetPosition = GetTargetPositionForTile(tile);
        StartCoroutine(MoveTileToPosition(tile, targetPosition));

        // Increment the order in layer for the next tile
        currentOrderInLayer++;

        // Check for groups of three tiles of the same type
        CheckForGroupsOfThree();
    }

    private Vector3 GetTargetPositionForTile(GameObject tile)
    {
        int index = tiles.IndexOf(tile);
        if (index == -1)
        {
            index = tiles.Count;
        }

        Vector3 bottomBarPosition = bottomBar.position;
        float targetPosX = bottomBarPosition.x + horizontalOffset + index * tileSpacing;
        float targetPosY = bottomBarPosition.y + verticalOffset;

        return new Vector3(targetPosX, targetPosY, bottomBarPosition.z);
    }

    private void AdjustTilePositions()
    {
        tiles.Clear();
        foreach (var group in groupedTiles)
        {
            tiles.AddRange(group.Value);
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            Vector3 targetPosition = GetTargetPositionForTile(tiles[i]);
            StartCoroutine(MoveTileToPosition(tiles[i], targetPosition));
        }
    }

    private IEnumerator MoveTileToPosition(GameObject tile, Vector3 targetPosition)
    {
        Vector3 startPosition = tile.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < tileMoveSpeed)
        {
            tile.transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / tileMoveSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tile.transform.position = targetPosition; // Ensure the final position is set
    }

    private void CheckForGroupsOfThree()
    {
        List<List<GameObject>> matches = new List<List<GameObject>>();

        foreach (var group in groupedTiles)
        {
            if (group.Value.Count >= 3)
            {
                matches.Add(group.Value.GetRange(0, 3));
            }
        }

        if (matches.Count > 0)
        {
            StartCoroutine(RemoveMatchingTiles(matches));
        }
    }

    private IEnumerator RemoveMatchingTiles(List<List<GameObject>> matches)
    {
        // Trigger the scale down and fade out animation on all matching tiles simultaneously
        foreach (var match in matches)
        {
            foreach (var tile in match)
            {
                Animator animator = tile.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("Remove");
                }
            }
        }

        // Wait for the animation to complete based on the selected animation type
        if (animationType == AnimationType.SlowFade)
        {
            yield return new WaitForSeconds(slowFadeDuration); // Adjust this to match the duration of your slow fade animation
        }
        else
        {
            yield return new WaitForSeconds(tileMoveSpeed); // Default animation duration
        }

        // Wait a short time to ensure the animation finishes
        yield return new WaitForSeconds(0.1f);

        // Remove all matching tiles
        foreach (var match in matches)
        {
            foreach (var tile in match)
            {
                // Check if the tile still exists in the dictionary to avoid the KeyNotFoundException
                SpriteRenderer fruitSpriteRenderer = tile.transform.Find("FruitSprite").GetComponent<SpriteRenderer>();
                if (fruitSpriteRenderer != null && groupedTiles.ContainsKey(fruitSpriteRenderer.sprite))
                {
                    groupedTiles[fruitSpriteRenderer.sprite].Remove(tile);
                    tiles.Remove(tile);
                    Destroy(tile);
                }
            }
        }

        // Adjust positions of remaining tiles after removal
        AdjustTilePositions();
    }
}
