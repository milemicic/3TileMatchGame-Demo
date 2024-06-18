using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Ensure the initial state is Idle
        if (animator != null)
        {
            animator.Play("Idle");
        }
    }

    // Button Animations
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Hover");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Idle");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Pressed");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Hover");
        }
    }
}
