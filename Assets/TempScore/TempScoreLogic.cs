using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempScoreLogic : MonoBehaviour
{
    public Vector2 standardSize;
    public Vector2 overHeatSize;

    public Sprite cold;
    public Sprite cool;
    public Sprite warm;
    public Sprite heat;
    public Sprite overHeat;

    private mchp hp;
    [HideInInspector] public Image rend;
    [HideInInspector] public bool isOverHeat;

    public GameObject fire;
    public GameObject parentFill;

    // Start is called before the first frame update
    void Start()
    {
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<mchp>();
        rend = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        CheckScale();
        UpdateFire();
    }
    void CheckScale()
    {
        if (rend.sprite == overHeat)
        {
            parentFill.transform.localScale = overHeatSize;
            isOverHeat = true;
        }
        else
        {
            parentFill.transform.localScale = standardSize;
            isOverHeat = false;
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
    void UpdateFire()
    {
        if (rend.sprite == overHeat)
        {
            fire.SetActive(true);
        }
        else
        {
            fire.SetActive(false);
        }
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
