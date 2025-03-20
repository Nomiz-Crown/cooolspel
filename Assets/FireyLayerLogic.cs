using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireyLayerLogic : MonoBehaviour
{
    private Image img;
    private mchp player;
    private float hpLastChecked;
    [SerializeField] private float OpacityIncreasePerDamageTook;
    [SerializeField] private float MaxOpacity;
    [SerializeField] private float PassiveOpacityDecrease;

    private float timer;
    [SerializeField] private float OpacityDecreaseEveryBlank;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<mchp>();

    }

    // Update is called once per frame
    void Update()
    {
        DecreaseOpacityPassively();
        CompareHealth();
        UpdateValues();
    }
    void CompareHealth()
    {
        if(player.TemperatureHealth > hpLastChecked)
        {
            IncreaseOpacity();
        }
    }
    void DecreaseOpacityPassively()
    {
        if (PassiveDecreaseTimer())
        {
            img.color -= new Color(0, 0, 0, PassiveOpacityDecrease);
        }
    }
    void IncreaseOpacity()
    {
        if (CheckIfCanAddOpacity())
        {
            img.color += new Color(0, 0, 0, OpacityIncreasePerDamageTook);
        }
    }
    bool CheckIfCanAddOpacity()
    {
        if (img.color.a < MaxOpacity)
        {
            return true;
        }
        return false;
    }
    void UpdateValues()
    {
        hpLastChecked = player.TemperatureHealth;
    }
    bool PassiveDecreaseTimer()
    {
        if(timer >= OpacityDecreaseEveryBlank && OpacityIsGreaterThan(0))
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
    bool OpacityIsGreaterThan(float num)
    {
        if(img.color.a > num)
        {
            return true;
        }
        return false;
    }
    //at 100-50, min value should be 0
    //at <50, the min value is 5, so (50 - currentHealth) / 2? 30:10, 20:15, 10:20, 0:25???
    //fråga simon? va tyckeru
}
