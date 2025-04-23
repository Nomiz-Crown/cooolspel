using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class deathScreen : MonoBehaviour
{
    public Image imageObject;
    public Sprite[] animationFrames;
    public float frameRate = 0.1f;

    public GameObject panel;
    public Vector2 startPosition;
    public Vector2 targetPosition;
    public float slideDuration = 1f;

    void Start()
    {
        if (imageObject != null && animationFrames.Length > 0)
        {
            imageObject.gameObject.SetActive(true);
            StartCoroutine(PlayImageAnimation());
        }
    }

    IEnumerator PlayImageAnimation()
    {
        for (int i = 0; i < animationFrames.Length; i++)
        {
            imageObject.sprite = animationFrames[i];
            yield return new WaitForSeconds(frameRate);
        }

        if (panel != null)
        {
            panel.SetActive(true);
            RectTransform rectTransform = panel.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = startPosition;
            StartCoroutine(SlideIn(rectTransform));
        }
    }

    IEnumerator SlideIn(RectTransform rectTransform)
    {
        float elapsed = 0f;
        Vector2 initialPos = rectTransform.anchoredPosition;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / slideDuration);
            rectTransform.anchoredPosition = Vector2.Lerp(initialPos, targetPosition, t);
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }
}
