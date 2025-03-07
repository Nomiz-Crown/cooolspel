using UnityEngine;

public class cameraMoveToPlayer : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private float smoothSpeed = 0.125f; // Speed of the camera movement
    [SerializeField] private Vector3 offset; // Offset from the player position

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Find player if not assigned
        }
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset; // Calculate desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Smoothly interpolate to the desired position
        transform.position = smoothedPosition; // Update camera position
    }
}
