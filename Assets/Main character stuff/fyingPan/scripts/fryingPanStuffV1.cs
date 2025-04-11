using UnityEngine;

public class fryingPanStuffV1 : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnVelocity;
    [HideInInspector] public bool hasPan;
    [SerializeField] private float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        hasPan = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        CheckMouse(mousePosition);
    }
    void CheckMouse(Vector2 mousePosition)
    {
        if (Input.GetMouseButtonDown(0) && hasPan)
        {
             TossFryingPan(mousePosition);
        }
    }
    void TossFryingPan(Vector2 mousePosition)
    {
        hasPan = false;

        // Convert mouse position to world coordinates
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0; // Set z to 0 since we are working in 2D

        SpawnObject(CalculateAngle(transform.position, worldMousePosition));
    }
    float CalculateAngle(Vector2 a, Vector2 b)
    {
        Vector2 direction = b - a;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }
    void SpawnObject(float spawnAngle)
    {
        Vector2 SpawnOffset = new Vector2(transform.position.x, transform.position.y + yOffset);
        GameObject spawnedObject = Instantiate(objectToSpawn, SpawnOffset, Quaternion.identity);
        Vector2 direction = Quaternion.Euler(0, 0, spawnAngle) * Vector2.right;
        spawnedObject.GetComponent<Rigidbody2D>().velocity = direction * spawnVelocity;
    }
}
