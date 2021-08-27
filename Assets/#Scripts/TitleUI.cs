using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    float time;
    float move = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0.5f)
        {
            move = -move;
            time = 0;
        }
        
        gameObject.transform.localPosition += new Vector3(0, move, 0);

        time += Time.deltaTime;

    }
}
