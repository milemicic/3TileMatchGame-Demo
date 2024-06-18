using UnityEngine;
using UnityEngine.EventSystems;

public class CasualGameButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("Idle");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        animator.SetTrigger("Hover");
    }
}
