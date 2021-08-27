using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BirdControl : MonoBehaviour
{

    public FadeInOut fader;
    public GameObject gameOver;
    public GameObject scoreBorad;
    public AudioClip audioDie;
    public AudioClip audioHit;
    public AudioClip audioScore;
    public AudioClip audioWing;
    public Button reStart;
    
    AudioSource audioSource;

    public Text ScoreUI;

    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        fader.gameObject.SetActive(false);
        Screen.SetResolution(480, 800, false);
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - time > 0.7f && Time.time - time < 1.2f && gameObject.GetComponent<Rigidbody>().useGravity)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, -230f) * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.timeScale != 0)
            {
                time = Time.time;
                PlaySound("WING");
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                gameObject.GetComponent<Rigidbody>().AddForce(0, 255, 0);
                gameObject.transform.eulerAngles = new Vector3(0, 0, 25f);
            }
        }
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    fader.gameObject.SetActive(true);
        //    StartCoroutine(StartFade());
        //}
    }
    IEnumerator StartFade()
    {
        fader.FadeIn(0.7f);
        yield return new WaitForSecondsRealtime(0.7f);

        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Game Over!");
        Time.timeScale = 0;
        PlaySound("HIT");
        gameObject.GetComponent<Animator>().Play("Die");

        if (!PlayerPrefs.HasKey("score"))
            PlayerPrefs.SetInt("score", 0);
        StartCoroutine(GameOver());
        
    }
    IEnumerator GameOver()
    {

        fader.gameObject.SetActive(true);
        fader.hitFade();

        yield return new WaitForSecondsRealtime(0.7f);
        fader.gameObject.SetActive(false);

        GameObject temp1 = Instantiate(gameOver);

        Vector3 p1 = temp1.transform.position;
        ScoreUI.color = new Color(1, 1, 1, 0);
        time = 0;
        while (true)
        {
            if (time <= 0.4f)
            {
                temp1.GetComponentsInChildren<SpriteRenderer>()[0].color = new Color(1, 1, 1, time * (5 / 2));
                temp1.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, time * (5 / 2));
                temp1.transform.position = new Vector3(p1.x, p1.y + (4 - 10 * time), p1.z);
            }
            else if (time < 0.5f)
            {
                temp1.GetComponentsInChildren<SpriteRenderer>()[0].color = new Color(1, 1, 1, 1);
                temp1.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 1);
                temp1.transform.position = new Vector3(p1.x, p1.y - time + 0.4f, p1.z);
            }
            else if (time < 0.6f)
                temp1.transform.position = new Vector3(p1.x, p1.y - 0.6f + time, p1.z);
            else if (time < 0.7f)
                temp1.transform.position = new Vector3(p1.x, p1.y - (time - 0.6f) / 2, p1.z);
            else if (time < 0.8f)
                temp1.transform.position = new Vector3(p1.x, p1.y - 0.05f + (time - 0.7f)/2,p1.z);
            else
            {
                temp1.GetComponentsInChildren<SpriteRenderer>()[0].color = new Color(1, 1, 1, 1);
                temp1.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 1);
                temp1.transform.position = p1;
                yield return null;
                break;
            }
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        GameObject temp2 = Instantiate(scoreBorad);

        Vector3 p2 = temp2.transform.position;

        Text t1 = temp2.GetComponentsInChildren<Text>()[0];
        Vector3 pt1 = t1.transform.position;

        Text t2 = temp2.GetComponentsInChildren<Text>()[1];
        if (PlayerPrefs.HasKey("score"))
        {
            t2.text = PlayerPrefs.GetInt("score").ToString();
        }
        Vector3 pt2 = t2.transform.position;

        time = 0;
        while (true)
        {
            if (time < 0.2f)
            {
                Vector3 v = new Vector3(p2.x, p2.y - (6 - 30 * time), p2.z);
                t1.transform.position = new Vector3(pt1.x, pt1.y - (6 - 30 * time) * 70, pt1.z); ;
                t2.transform.position = new Vector3(pt2.x, pt2.y - (6 - 30 * time) * 70, pt2.z); ;
                temp2.transform.position = v;
            }
            else
            {
                temp2.transform.position = p2;
                t1.transform.position = pt1;
                t2.transform.position = pt2;

                float duration = 0.5f;
                float offset = float.Parse(ScoreUI.text) / duration;
                float current = 0;
                float target = float.Parse(ScoreUI.text);

                while (current < target)
                {
                    current += offset * Time.unscaledDeltaTime;
                    t1.text = ((int)current).ToString();

                    if (PlayerPrefs.GetInt("score") < (int)current)
                    {
                        temp2.GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 0, 1);
                        t2.text = ((int)current).ToString();
                    }
                    yield return null;
                }
                current = target;
                t1.text = ((int)current).ToString();
                PlayerPrefs.SetInt("score", int.Parse(t2.text));

                break;
            }
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        reStart.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        reStart.interactable = true;


    }
    void PlaySound(string action)
    {
        switch (action)
        {
            case "WING":
                audioSource.clip = audioWing;
                break;
            case "HIT":
                audioSource.clip = audioHit;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
        }
        audioSource.Play();
    }
}
