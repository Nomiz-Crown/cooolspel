using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    new Collider2D collider;
    bool isDying = false;
    public float myHealth;
    [SerializeField] private GameObject Score;
    private StyleLogicV2 realScore;

    // Start is called before the first frame update
    void Start()
    {
        realScore = Score.GetComponentInChildren<StyleLogicV2>();
        collider = GetComponent<Collider2D>();
        if (collider == null || Score == null)
        {
            print("uuooohhhhhhhhhhhhh");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myHealth > 0 || isDying == false) return;

        if (myHealth <= 0)
        {
            realScore.AdjustScore(100);
        }
        else if (isDying)
        {
            realScore.AdjustScore(200);
        }
        Destroy(gameObject);
    }
    void Kys()
    {
        isDying = true;
    }
}
