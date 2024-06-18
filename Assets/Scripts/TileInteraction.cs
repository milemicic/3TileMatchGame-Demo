using UnityEngine;
using System.Collections;

public class TileInteraction : MonoBehaviour
{
    private Animator animator;
    private BottomBarManager bottomBarManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bottomBarManager = FindObjectOfType<BottomBarManager>(); // Find the BottomBarManager in the scene
    }

    private void OnMouseEnter()
    {
        if (gameObject.tag == "HorizontalTile")
        {
            animator.SetBool("isHovering", true);
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.tag == "HorizontalTile")
        {
            animator.SetBool("isHovering", false);
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.tag == "HorizontalTile")
        {
            StartCoroutine(ClickAnimation());
        }
    }

    private IEnumerator ClickAnimation()
    {
        animator.SetBool("isClicked", true);
        yield return new WaitForSeconds(0.2f); // Adjust the duration to match your animation length
        animator.SetBool("isClicked", false);

        // Add the tile to the bottom bar
        bottomBarManager.AddTile(gameObject);
    }
}
