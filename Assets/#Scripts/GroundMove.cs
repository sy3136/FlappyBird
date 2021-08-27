using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    float _distance = 7.67f;
    int _count = 2;
    int _index = 2;

    public GameObject[] grounds;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(480, 800, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;

        gameObject.transform.localPosition += new Vector3(-Time.deltaTime*3, 0, 0);

        _count = 2 + (int)(-gameObject.transform.localPosition.x / 7.67f);
        if (_index != _count)
        {
            grounds[(_index - 2) % 3].transform.localPosition = new Vector3(_distance * _count, -5, 0);
            _index = _count;
        }
        time += Time.deltaTime;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
