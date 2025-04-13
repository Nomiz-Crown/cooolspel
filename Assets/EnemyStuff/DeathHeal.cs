using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    private mchp hp;
    private bool healed = false;

    void Start()
    {
        healed = false;
        hp = FindObjectOfType<mchp>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (hp != null && !healed)
            {
                print("yello");
                hp.TemperatureHealth -= 5;
                healed = true;
            }
        }
    }
}
