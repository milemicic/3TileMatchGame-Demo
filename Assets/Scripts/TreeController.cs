using UnityEngine;

[ExecuteInEditMode]
public class TreeController : MonoBehaviour
{
    public RectTransform tree1;
    public RectTransform tree2;

    [System.Serializable]
    public class TreeProperties
    {
        public Vector2 mobilePosition;
        public Vector2 mobileScale = Vector2.one;
        public Vector2 tabletPosition;
        public Vector2 tabletScale = Vector2.one;
        public Vector2 targetPosition;
        public Vector2 targetScale = Vector2.one;
    }

    public TreeProperties tree1Properties;
    public TreeProperties tree2Properties;

    // Additional vertical offset for tablet resolution
    public float tabletVerticalOffset = 200f;

    private int previousWidth;
    private int previousHeight;

    void Start()
    {
        AdjustTrees();
    }

    void OnValidate()
    {
        AdjustTrees();
    }

    void AdjustTrees()
    {
        if (tree1 == null || tree2 == null)
        {
            Debug.LogError("Tree references are not assigned.");
            return;
        }

        float aspectRatio = (float)Screen.width / Screen.height;

        if (Screen.width == 1440 && Screen.height == 2960) // Mobile Phone
        {
            AdjustTree(tree1, tree1Properties.mobilePosition, tree1Properties.mobileScale);
            AdjustTree(tree2, tree2Properties.mobilePosition, tree2Properties.mobileScale);
        }
        else if (Screen.width == 1080 && Screen.height == 2340) // Mobile V2
        {
            AdjustTree(tree1, tree1Properties.mobilePosition, tree1Properties.mobileScale);
            AdjustTree(tree2, tree2Properties.mobilePosition, tree2Properties.mobileScale);
        }
        else if (Screen.width == 2048 && Screen.height == 2732) // Tablet
        {
            AdjustTree(tree1, tree1Properties.tabletPosition + new Vector2(0, tabletVerticalOffset), tree1Properties.tabletScale);
            AdjustTree(tree2, tree2Properties.tabletPosition + new Vector2(0, tabletVerticalOffset), tree2Properties.tabletScale);
        }
        else if (Screen.width == 1080 && Screen.height == 1920) // Target Resolution
        {
            AdjustTree(tree1, tree1Properties.targetPosition, tree1Properties.targetScale);
            AdjustTree(tree2, tree2Properties.targetPosition, tree2Properties.targetScale);
        }
    }

    void AdjustTree(RectTransform tree, Vector2 position, Vector2 scale)
    {
        tree.anchoredPosition = position;
        tree.localScale = scale;
    }

    void Update()
    {
        // Adjust on screen size change
        if (Screen.width != previousWidth || Screen.height != previousHeight)
        {
            previousWidth = Screen.width;
            previousHeight = Screen.height;
            AdjustTrees();
        }
    }
}
