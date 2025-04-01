using UnityEngine;

public class ExplosionLogic : MonoBehaviour
{
    private float timer = 0;
    public float  animationDuration;

    public GameObject canvas;
    private PerformanceTallyLogicV1 tally;

    private EnemyHealth enemysHealth;
    // Start is called before the first frame update
    void Start()
    {
        tally = canvas.GetComponentInChildren<PerformanceTallyLogicV1>();   
    }

    // Update is called once per frame
    void Update()
    {
        TimerOfDuration();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            tally.UpdateTally("+ ZIGGS R", "Add");
            GankComponent(collision.gameObject);
            enemysHealth.Kys();
        }
    }
    void GankComponent(GameObject obj)
    {
        if (obj.GetComponent<EnemyHealth>() != null)
        {
            enemysHealth = obj.GetComponent<EnemyHealth>();
        }
    }
    void TimerOfDuration()
    {
        if(timer >= animationDuration)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
