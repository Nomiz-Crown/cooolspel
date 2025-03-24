using UnityEngine;
using UnityEngine.UI;

public class UIAnimationAuto : MonoBehaviour
{
    public string spriteSheetName; // Name of the spritesheet
    public Image image;
    public float frameRate = 0.05f;

    private Sprite[] frames;
    private int currentFrame;
    private float timer;

    private bool isActive;
    private Image img;
    public GameObject flip;
    private TempScoreLogic flippy;

    void Start()
    {
        img = GetComponent<Image>();
        flippy = flip.GetComponent<TempScoreLogic>();
        // Load all sprites from the spritesheet automatically
        frames = Resources.LoadAll<Sprite>(spriteSheetName);

        if (frames.Length == 0)
        {
            Debug.LogError("No sprites found! Make sure your spritesheet is in Resources.");
            return;
        }
    }

    void Update()
    {
        checkActive();
        if (frames == null || frames.Length == 0 || !isActive) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0;
            currentFrame = (currentFrame + 1) % frames.Length;
            image.sprite = frames[currentFrame]; // Change the UI Image sprite
        }
    }
    void checkActive()
    {
        if (flippy.isOverHeat)
        {
            img.color = new Color(1, 1, 1, 1);
            if (!isActive)
            {
                isActive = true;
            }
        }
        else
        {
            img.color = new Color(1, 1, 1, 0);
        }
    }
}
