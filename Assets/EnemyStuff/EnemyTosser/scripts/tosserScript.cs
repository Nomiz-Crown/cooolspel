using UnityEngine;

public class TosserScript : MonoBehaviour
{
    private Transform target;
    private Vector2 realTarget;

    public LayerMask obstructionMask;
    public LayerMask playerLayer;

    public float shootCooldown;
    private float cooldownTime;

    public float speed;

    public Rigidbody2D rb;
    public bool canShoot = false;
    private ShootPlayer shooter;

    private Animator anim;  // Animator reference

    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<ShootPlayer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Get the Animator component
        UpdateRealTarget();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRealTarget();
        BehaviourLogic();
        ShootCooldownTimer();
    }

    void BehaviourLogic()
    {
        if (HasClearLineOfSight())
        {
            // No movement, only shooting
            rb.velocity = new Vector2(0, rb.velocity.y); // Stop movement
            if (canShoot)
            {
                anim.Play("Shoot");  // Play the "Shoot" animation directly
                shooter.Shoot(); // Perform shooting
                cooldownTime = 0f; // Reset cooldown timer after shooting
            }
        }
        else
        {
            // Move towards the player
            MoveToPlayer();
            anim.Play("Idle");  // Play the "Idle" animation when moving or not shooting
        }
    }

    void MoveToPlayer()
    {
        // Calculate direction
        float direction = target.position.x > transform.position.x ? 1f : -1f;

        // Apply movement with smooth acceleration
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    // Method to update the real target position
    private void UpdateRealTarget()
    {
        realTarget = new Vector2(target.position.x, target.position.y + 1);
    }

    // Method to check if there is a clear line of sight to the target
    public bool HasClearLineOfSight()
    {
        // Calculate the direction from this object to the real target
        Vector2 directionToTarget = (realTarget - (Vector2)transform.position).normalized;

        // Perform a raycast from this object to the real target
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, Vector2.Distance(transform.position, realTarget), obstructionMask);

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
            Gizmos.DrawLine(transform.position, realTarget);
        }
    }

    void ShootCooldownTimer()
    {
        if (cooldownTime >= shootCooldown)
        {
            canShoot = true; // Tosser can shoot
        }
        else
        {
            cooldownTime += Time.deltaTime; // Increase cooldown time
            canShoot = false; // Tosser cannot shoot yet
        }
    }
}
