using UnityEngine;

public class mchp : MonoBehaviour
{
    [SerializeField] private float overheatInterval;
    [SerializeField] private float heatIncPerInterval;

    [Range(0, 100)] public float TemperatureHealth = 0;
    public GameObject death; //death är gameobject med death animation btw

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (death != null)
        {
            death.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DieConditions();
        PassivelyOverheat();
    }
    void PassivelyOverheat()
    {
        if (TimerColon3())
        {
            TemperatureHealth += heatIncPerInterval;
        }
    }
    bool TimerColon3()
    {
        if(timer >= overheatInterval)
        {
            timer = 0;
            return true;
        }
        else
        {
            timer += Time.deltaTime;
            return false;
        }
    }

    void DieConditions()
    {
        if (TemperatureHealth >= 100)
        {
            TemperatureHealth = 100;
            Die();
        }
    }
    void Die()
    {
        // mer debug

        gameObject.SetActive(false);

        if (death != null)
        {
            death.transform.parent = null;

            death.SetActive(true);
            death.transform.position = transform.position; 
            death.transform.rotation = transform.rotation; 

            // Deeeeeebuuuug
        }
    }
    public void TakeDamage(float amount)
    {
        TemperatureHealth += Random.Range(amount - 10, amount + 10);
    }
    public void RestoreHealth(float amount)
    {
        float flippy = Random.Range(amount - 10, amount + 10);
        if (TemperatureHealth - flippy < 0) TemperatureHealth -= TemperatureHealth;
        else TemperatureHealth -= flippy;
    }
}
