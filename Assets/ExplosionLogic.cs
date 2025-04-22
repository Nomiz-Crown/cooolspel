using UnityEngine;

public class ExplosionLogic : MonoBehaviour
{
    private float timer = 0;
    public float  animationDuration;

    public GameObject canvas;
    private PerformanceTallyLogicV1 tally;

    private EnemyHealth enemysHealth;
    public bool IsGoobers;
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
            GankComponent(collision.gameObject);
            if (IsGoobers) enemysHealth.Kys("GOOBER MISSILE");
            else enemysHealth.Kys("ZIGGS R");
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
