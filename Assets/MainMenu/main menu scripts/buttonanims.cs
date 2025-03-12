using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonanims : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image targetImage; //vilken image som ska visa animationen
    public Sprite[] frames; // dem frames som är med, ui bullshit
    public float frameRate = 0.1f; 

    private int currentFrame;
    private float timer;
    private bool isHovering = false;

    void Start()
    {
        if (targetImage != null)
            targetImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isHovering || targetImage == null || frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0;
            currentFrame = (currentFrame + 1) % frames.Length; // Loop, behövs inte om man predeterminade det innan
            targetImage.sprite = frames[currentFrame];
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(true);
            isHovering = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(false); 
            isHovering = false;
            currentFrame = 0; // du kan typ reseta animation frame, så det går till frame 0
        }
    }
}