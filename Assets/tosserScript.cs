using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tosserScript : MonoBehaviour
{
    Transform target;
    public LayerMask obstructionMask;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
    }

    // Update is called once per frame
    void Update()
    {
        HasClearLineOfSight();
    }
    // Method to check if there is a clear line of sight to the target
    public bool HasClearLineOfSight()
    {
        // Calculate the direction from this object to the target
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        // Perform a raycast from this object to the target
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, Vector2.Distance(transform.position, target.position), obstructionMask);

        // If the raycast hits something, check if it's the target
        if (hit.collider != null)
        {
            return hit.collider.transform == target; // Return true if the target is hit, false otherwise
        }

        // If nothing is hit, return true (clear line of sight)
        return true;
    }

    private void OnDrawGizmos()
    {
        // Draw the ray in the editor for visualization
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
