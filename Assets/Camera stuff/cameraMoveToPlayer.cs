using System.Collections;
using UnityEngine;

public class CameraMoveToPlayer : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [Header("camera stuff")]
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player not found. Please ensure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void ShakeCamera()
    {
        StartCoroutine(Shake(shakeDuration));
    }

    private IEnumerator Shake(float duration)
    {
        float elapsed = 0.0f;

        Vector3 originalPosition = transform.position;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Calculate the new position based on the player's current position
            Vector3 newPosition = originalPosition + new Vector3(x, y, 0);
            transform.position = newPosition;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // After shaking, smoothly transition back to the player's position
        Vector3 desiredPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }

}
