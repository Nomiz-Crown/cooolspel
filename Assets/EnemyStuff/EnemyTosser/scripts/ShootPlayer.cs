using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    Transform target;
    public GameObject bullet;
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
            if (target.position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x); //den vänder sig mot spelaren
            }
            else
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
    }
    public void Shoot()
    {
        GameObject clone = Instantiate(bullet, transform);
        clone.transform.position = transform.position;
        Rigidbody2D myBulletsRigidbody = clone.GetComponent<Rigidbody2D>();
        Vector3 fuck = new Vector2(target.position.x, target.position.y + 1);
        Vector2 direction = (fuck - clone.transform.position).normalized;
        myBulletsRigidbody.velocity = direction * velocity;
    }
}
