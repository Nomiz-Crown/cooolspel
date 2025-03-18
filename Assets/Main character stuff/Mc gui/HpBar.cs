using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpBar : MonoBehaviour
{
    private mchp playerHealth;  // mchp i mc scripts, så den kopierar från mchp till denna nya
    private Slider slider;

    [Header("UI Animations")]
    public Image image1;          // First UI (HP > 75)
    public Sprite[] animation1Frames;

    public Image image2;          // Second UI (50 < HP <= 75)
    public Sprite[] animation2Frames;

    public Image image3;          // Third UI (25 < HP <= 50)
    public Sprite[] animation3Frames;

    public Image image4;          // Fourth UI (HP <= 25)
    public Sprite[] animation4Frames;

    public float frameRate = 0.1f;

    private void Start()
    {
        slider = GetComponent<Slider>();

        playerHealth = FindObjectOfType<mchp>();

        if (playerHealth != null)
        {
            slider.maxValue = 100;
            slider.value = playerHealth.hp;
        }

        StartCoroutine(PlaySpriteAnimation(image1, animation1Frames));
        StartCoroutine(PlaySpriteAnimation(image2, animation2Frames));
        StartCoroutine(PlaySpriteAnimation(image3, animation3Frames));
        StartCoroutine(PlaySpriteAnimation(image4, animation4Frames));

        UpdateActiveImage();
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
            if (image.enabled) // bara animera dem som är active, mindre lag -_-
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
            DeactivateAllImages();
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