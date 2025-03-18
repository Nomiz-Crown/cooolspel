using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpBar : MonoBehaviour
{
    private mchp playerHealth;  // Reference to the player's health script
    private Slider slider;

    [Header("UI Animations")]
    public Image image1;          // First UI Image (HP > 75)
    public Sprite[] animation1Frames;

    public Image image2;          // Second UI Image (50 < HP <= 75)
    public Sprite[] animation2Frames;

    public Image image3;          // Third UI Image (25 < HP <= 50)
    public Sprite[] animation3Frames;

    public Image image4;          // Fourth UI Image (10 < HP <= 25)
    public Sprite[] animation4Frames;

    public float frameRate = 0.1f; // Animation speed

    private void Start()
    {
        slider = GetComponent<Slider>();

        // Find the player in the scene
        playerHealth = FindObjectOfType<mchp>();

        if (playerHealth != null)
        {
            slider.maxValue = 100;
            slider.value = playerHealth.hp;
        }

        // Start animations for all images but disable them initially
        StartCoroutine(PlaySpriteAnimation(image1, animation1Frames));
        StartCoroutine(PlaySpriteAnimation(image2, animation2Frames));
        StartCoroutine(PlaySpriteAnimation(image3, animation3Frames));
        StartCoroutine(PlaySpriteAnimation(image4, animation4Frames));

        UpdateActiveImage(); // Set correct image at start
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            slider.value = playerHealth.hp;
            UpdateActiveImage();
        }
    }

    private IEnumerator PlaySpriteAnimation(Image image, Sprite[] frames)
    {
        int index = 0;
        while (true)
        {
            if (image.enabled) // Only animate active images
            {
                image.sprite = frames[index];
                index = (index + 1) % frames.Length;
            }
            yield return new WaitForSeconds(frameRate);
        }
    }

    private void UpdateActiveImage()
    {
        float hp = playerHealth.hp;

        if (hp > 75)
        {
            ActivateImage(image1);
        }
        else if (hp > 50)
        {
            ActivateImage(image2);
        }
        else if (hp > 25)
        {
            ActivateImage(image3);
        }
        else if (hp >= 0)
        {
            ActivateImage(image4);
        }
        else
        {
            DeactivateAllImages(); // If HP is 10 or below, turn off all images
        }
    }

    private void ActivateImage(Image activeImage)
    {
        image1.enabled = (activeImage == image1);
        image2.enabled = (activeImage == image2);
        image3.enabled = (activeImage == image3);
        image4.enabled = (activeImage == image4);
    }

    private void DeactivateAllImages()
    {
        image1.enabled = false;
        image2.enabled = false;
        image3.enabled = false;
        image4.enabled = false;
    }
}