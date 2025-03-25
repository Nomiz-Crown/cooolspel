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

    private Rigidbody2D rb;
    private bool canShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (canShoot)
            {
                SendMessage("Shoot", target);
                cooldownTime = 0f;
            }
        }
        else
        {
            MoveToPlayer();
        }
    }
    void MoveToPlayer()
    {
        if(target.transform.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if(target.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
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
            print("i see you");
            return hit.collider.transform == target; // Return true if the target is hit, false otherwise
        }

        // If nothing is hit, return true (clear line of sight)
        print("i see you");
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
        if(cooldownTime >= shootCooldown)
        {
            print("can Shoot");
            canShoot = true;
        }
        else
        {
            cooldownTime += Time.deltaTime;
            canShoot = false;
        }
    }
}
