using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fryingPanStuffV1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouse();   
    }
    void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            TossFryingPan(mousePosition);
        }
    }
    void TossFryingPan(Vector2 Mouse)
    {

    }
}
