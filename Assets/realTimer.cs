using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class realTimer : MonoBehaviour
{
    TextMeshProUGUI gromp;
    [HideInInspector] public string displayMe;
    // Start is called before the first frame update
    void Start()
    {
        gromp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        gromp.text = displayMe;   
    }
}
