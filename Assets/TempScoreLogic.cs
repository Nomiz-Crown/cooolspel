using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScoreLogic : MonoBehaviour
{
    public float standardWidth;
    public float overHeatWidth;

    public Sprite cold;
    public Sprite cool;
    public Sprite warm;
    public Sprite heat;
    public Sprite overHeat;

    private mchp hp;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<mchp>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        CheckScale();
        print("hi");
    }
    void CheckScale()
    {
        if (rend.sprite == overHeat)
        {
            transform.localScale = new Vector2(overHeatWidth, transform.localScale.y);
        }
        else
        {
            transform .localScale = new Vector2(standardWidth, transform.localScale.y);
        }
    }
    void CheckState()
    {
        CheckCold();
        CheckCool();
        CheckWarm();
        CheckHeat();
        CheckOverHeat();
    }
    void CheckCold()
    {
        if( cold != null && hp.TemperatureHealth >= 0 && hp.TemperatureHealth <= 20) 
        {
            rend.sprite = cold;
        }
    }
    void CheckCool()
    {
        if(cool != null && hp.TemperatureHealth >= 21 && hp.TemperatureHealth <= 40)
        {
            rend.sprite = cool;
        }
    }
    void CheckWarm()
    {
        if (warm != null && hp.TemperatureHealth >= 41 && hp.TemperatureHealth <= 60)
        {
            rend.sprite = warm;
        }
    }
    void CheckHeat()
    {
        if (heat != null && hp.TemperatureHealth >= 61 && hp.TemperatureHealth <= 80)
        {
            rend.sprite = heat;
        }
    }
    void CheckOverHeat()
    {
        if (overHeat != null && hp.TemperatureHealth >= 81 && hp.TemperatureHealth <= 100)
        {
            rend.sprite = overHeat;
        }
    }
}
