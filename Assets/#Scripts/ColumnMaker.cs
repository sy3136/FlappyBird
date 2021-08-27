using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColumnMaker : MonoBehaviour
{

    public GameObject   Column;


    private float       nowTime;
    private float       makeTime = 2f;

    public Text         ScoreUI;
    private int         score = 0;
    private float       scoreTime;

    public AudioClip audioScore;
    AudioSource audio;
    float time = 0;

    private bool start = false;
    private bool start_one = false;

    // Start is called before the first frame update
    void Start()
    {
        nowTime = Time.time;
        scoreTime = Time.time + makeTime;
        audio = gameObject.GetComponent<AudioSource>();
        audio.clip = audioScore;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !start)
        {
            start = true;
            nowTime = time;
            scoreTime = time + makeTime;
        }
        if (!start)
            return;
        if (time - nowTime > makeTime)
        {
            nowTime = time;
            GameObject temp = Instantiate(Column);
            temp.transform.parent = gameObject.transform;
            
            float randomY = Random.Range(-6.15f, -1.5f);

            temp.transform.localPosition = new Vector3(-gameObject.transform.localPosition.x + 5, randomY, 0);
            temp.transform.localScale = new Vector3(1, 1, 1);

            if (start_one)
            {
                score++;
                ScoreUI.text = score.ToString();
                audio.Play();
            }
            start_one = true;

        }
 
        time += Time.deltaTime;
    }
}
