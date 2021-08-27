using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOutBackground : MonoBehaviour
{
    public FadeInOut fader;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGameFade());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(StartFade());
        }
    }
    IEnumerator StartFade()
    {
        fader.FadeIn(0.7f);
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Game");
    }
    IEnumerator StartGameFade()
    {
        fader.FadeOut(0.7f);
        yield return null;
    }
}
