using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionAnimation : MonoBehaviour
{
    public float rotationSpeed;
    private float timer = 0;
    public float ExplodeDuration;
    public float explosionMaxSize;
    SpriteRenderer objectRenderer;
    private Color objectColor;
    public float seeThroughTimer;
    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<SpriteRenderer>();
        objectColor = objectRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
        Expand();
        CheckTimer();
        CheckDelete();
        print(timer);
    }
    void CheckDelete()
    {
        if (timer >= ExplodeDuration * 2)
        {
            Destroy(gameObject);
        }
    }
    void Spin()
    {
        float rotation = rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);
    }
    void Expand()
    {
        float growthSpeed = explosionMaxSize / 100;
        if (timer <= ExplodeDuration && explosionMaxSize > transform.localScale.x)
        {
            transform.localScale += new Vector3(transform.localScale.x * growthSpeed, transform.localScale.y * growthSpeed, transform.localScale.z * growthSpeed);
        }
    }
    void CheckTimer()
    {
        // Check if the timer has reached the explosion duration
        if (timer >= ExplodeDuration && objectRenderer.color.a > 0)
        {
            // Gradually increase the alpha value towards 1 (opaque)
            DecreaseOpacity(seeThroughTimer);
        }
        // Increment the timer by the time elapsed since the last frame
        timer += Time.deltaTime;
    }
    public void DecreaseOpacity(float duration)
    {
        StartCoroutine(DecreaseOpacityCoroutine(duration));
    }

    private IEnumerator DecreaseOpacityCoroutine(float duration)
    {
        float startOpacity = objectColor.a;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float newOpacity = Mathf.Lerp(startOpacity, 0f, timeElapsed / duration);
            objectColor.a = newOpacity;
            objectRenderer.material.color = objectColor;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        objectColor.a = 0f;
        objectRenderer.material.color = objectColor;
    }
}
