using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonanims : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image targetImage; // Assign the child Image in the Inspector
    public Sprite[] frames; // Drag your sprite frames here
    public float frameRate = 0.1f; // Adjust animation speed

    private int currentFrame;
    private float timer;
    private bool isHovering = false;

    void Start()
    {
        if (targetImage != null)
            targetImage.gameObject.SetActive(false); // Ensure it starts hidden
    }

    void Update()
    {
        if (!isHovering || targetImage == null || frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0;
            currentFrame = (currentFrame + 1) % frames.Length; // Loop animation
            targetImage.sprite = frames[currentFrame]; // Update the assigned UI Image
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(true); // Show image when hovering
            isHovering = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(false); // Hide image when not hovering
            isHovering = false;
            currentFrame = 0; // Reset animation frame
        }
    }
}