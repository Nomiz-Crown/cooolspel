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

    void Start()
    {
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
        if (frames == null || frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0;
            currentFrame = (currentFrame + 1) % frames.Length;
            image.sprite = frames[currentFrame]; // Change the UI Image sprite
        }
    }
}
