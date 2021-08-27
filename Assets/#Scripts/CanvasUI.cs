using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CanvasUI : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time < 0.5f)
        {
            gameObject.GetComponent<CanvasRenderer>().SetColor(new Color(1, 1, 1 ,1-time));
        }
        else
        {
            gameObject.GetComponent<CanvasRenderer>().SetColor(new Color(1 ,1 , 1, time));
            if (time > 1f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;

    }
}
