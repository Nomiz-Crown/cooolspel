using UnityEngine;
using TMPro;

public class AirbornePanLogic : MonoBehaviour
{
    fryingPanStuffV1 guy;
    [SerializeField] private LayerMask PlayerLayer;
    Rigidbody2D rb;
    public float PickupRange;
    [HideInInspector] public bool isLethal;
    CapsuleCollider2D SecondaryCollider;
    bool ReturnedToRegularCollider;

    // References to the GUI components
    [SerializeField] private GameObject scobert;   // This can be a UI Image or any other GameObject
    [SerializeField] private TextMeshProUGUI trex;

    // Timer related variables
    float timer = 0;
    [SerializeField] float timeToReturn;

    void Start()
    {
        isLethal = true;
        guy = GameObject.FindGameObjectWithTag("Player").GetComponent<fryingPanStuffV1>();
        rb = GetComponent<Rigidbody2D>();
        SecondaryCollider = GetComponent<CapsuleCollider2D>();
        ReturnedToRegularCollider = false;

        // Try to find "scobert" and its "trex" TextMeshPro component more robustly
        scobert = GameObject.Find("scobert");
        if (scobert != null)
        {
            Transform trexTransform = scobert.transform.Find("trex");
            if (trexTransform != null)
            {
                trex = trexTransform.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("trex not found as a child of scobert");
            }
        }
        else
        {
            Debug.LogError("scobert not found in the scene");
        }
    }

    void Update()
    {
        if (IsWithinProximity(transform.position, guy.transform.position, PickupRange) && !isLethal)
        {
            guy.hasPan = true;
            SetGUIOpacity(0f); // Reset opacity when frying pan is picked up
            Destroy(gameObject);
        }

        if (!ReturnedToRegularCollider && !isLethal)
        {
            SecondaryCollider.isTrigger = false;
        }

        CheckReturn();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Do cool stuff here
        }
        else
        {
            isLethal = false;
        }
        rb.gravityScale = 1;
    }

    private bool IsWithinProximity(Vector2 positionA, Vector2 positionB, float threshold)
    {
        return Vector2.Distance(positionA, positionB) <= threshold;
    }

    void CheckReturn()
    {
        if (isLethal) return;

        if (Timer())
        {
            guy.hasPan = true;
            SetGUIOpacity(0f); // Reset opacity after timer hits 0
            Destroy(gameObject);
        }
    }

    bool Timer()
    {
        if (timer >= timeToReturn)
        {
            timer = 0;
            return true;
        }
        else
        {
            timer += Time.deltaTime;
            UpdateCountdownText(); // Update the countdown text each frame
        }
        return false;
    }

    void SetGUIOpacity(float opacity)
    {
        if (scobert != null)
        {
            CanvasGroup canvasGroup = scobert.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = opacity; // Set opacity of the entire "scobert" GUI
            }

            if (trex != null)
            {
                trex.alpha = opacity; // Set opacity of "trex" text
            }
        }
        else
        {
            Debug.LogError("scobert is null. Check if it's in the scene.");
        }
    }

    void UpdateCountdownText()
    {
        if (trex != null)
        {
            float remainingTime = Mathf.Max(0, timeToReturn - timer);
            trex.text = Mathf.Ceil(remainingTime).ToString(); // Display remaining time in seconds
        }
        else
        {
            Debug.LogError("trex is null. Check if it's assigned properly.");
        }
    }
}
