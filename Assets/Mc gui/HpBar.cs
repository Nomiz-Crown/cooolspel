using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpBar : MonoBehaviour
{
    private mchp playerHealth;
    private Slider slider;

    [Header("UI Animations")]
    public Image image1;  // 0–20%
    public Sprite[] animation1Frames;

    public Image image2;  // 20–40%
    public Sprite[] animation2Frames;

    public Image image3;  // 40–60%
    public Sprite[] animation3Frames;

    public Image image4;  // 60–80%
    public Sprite[] animation4Frames;

    public Image image5;  // 80–100%
    public Sprite[] animation5Frames;

    public float frameRate = 0.1f;

    private void Start()
    {
        slider = GetComponent<Slider>();
        playerHealth = FindObjectOfType<mchp>();

        if (playerHealth != null)
        {
            slider.maxValue = 100;
            slider.value = playerHealth.TemperatureHealth;
        }

        StartCoroutine(PlaySpriteAnimation(image1, animation1Frames));
        StartCoroutine(PlaySpriteAnimation(image2, animation2Frames));
        StartCoroutine(PlaySpriteAnimation(image3, animation3Frames));
        StartCoroutine(PlaySpriteAnimation(image4, animation4Frames));
        StartCoroutine(PlaySpriteAnimation(image5, animation5Frames));

        UpdateActiveImage();
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            slider.value = playerHealth.TemperatureHealth;
            UpdateActiveImage();
        }
    }


    private IEnumerator PlaySpriteAnimation(Image image, Sprite[] frames)
    {
        int index = 0;
        while (true)
        {
            if (image.enabled) // Only animate the active image to save performance
            {
                image.sprite = frames[index];
                index = (index + 1) % frames.Length;
            }
            yield return new WaitForSeconds(frameRate);
        }
    }

    private void UpdateActiveImage()
    {
        float hp = playerHealth.TemperatureHealth;

        if (hp > 80)
        {
            ActivateImage(image5);
        }
        else if (hp > 60)
        {
            ActivateImage(image4);
        }
        else if (hp > 40)
        {
            ActivateImage(image3);
        }
        else if (hp > 20)
        {
            ActivateImage(image2);
        }
        else
        {
            ActivateImage(image1);
        }
    }

    private void ActivateImage(Image activeImage)
    {
        image1.enabled = (activeImage == image1);
        image2.enabled = (activeImage == image2);
        image3.enabled = (activeImage == image3);
        image4.enabled = (activeImage == image4);
        image5.enabled = (activeImage == image5);
    }
}
