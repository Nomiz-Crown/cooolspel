using System.Collections;
using UnityEngine;

public class DeactivationZone : MonoBehaviour
{
    public DarknessStuff darknessScript;
    public Camera mainCamera;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (darknessScript != null)
            {
                darknessScript.DeactivateAll();
            }

            if (mainCamera != null)
            {
                StartCoroutine(SmoothZoomOut(5f, 10f, 1.5f));
            }
        }
    }

    IEnumerator SmoothZoomOut(float startSize, float endSize, float duration)
    {
        float elapsed = 0f;
        mainCamera.orthographicSize = startSize;

        while (elapsed < duration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(startSize, endSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = endSize;
    }
}
