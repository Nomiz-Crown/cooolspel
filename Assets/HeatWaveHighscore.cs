using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HeatWaveHighscore : MonoBehaviour
{
    string filePath;
    string myLevel = "heatwave";
    // Start is called before the first frame update
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "highscore" + myLevel + ".json");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
