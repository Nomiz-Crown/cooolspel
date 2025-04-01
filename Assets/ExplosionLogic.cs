using UnityEngine;

public class ExplosionLogic : MonoBehaviour
{
    private float timer = 0;
    public float  animationDuration;
    // Start is called before the first frame update
    void Start()
    {
        
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
            GankComponent(collision.gameObject).Kys();
        }
    }
    EnemyHealth GankComponent(GameObject obj)
    {
        EnemyHealth flippy;
        if (obj.GetComponent<EnemyHealth>() != null)
        {
            flippy = obj.GetComponent<EnemyHealth>();
            return flippy;
        }
        else
        {
            return null;
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
