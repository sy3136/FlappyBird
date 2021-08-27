using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public FadeInOut fader;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void reStart()
    {
        fader.gameObject.SetActive(true);
        StartCoroutine(StartFade());
    }

    IEnumerator StartFade()
    {
        fader.FadeIn(0.7f);
        yield return new WaitForSecondsRealtime(0.7f);

        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }
}
