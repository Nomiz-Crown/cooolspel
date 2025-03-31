using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarColorController : MonoBehaviour
{
    public Color ColdColor;
    public Color HotColor;
    mchp player;
    Image imageController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<mchp>();
        imageController = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        TransitionColor();
    }
    private void TransitionColor()
    {
        if (player != null)
        {
            float healthPercentage = player.TemperatureHealth / 100f; // Assuming hp is between 0 and 100
            imageController.color = Color.Lerp(ColdColor, HotColor, healthPercentage);
        }
    }
}
