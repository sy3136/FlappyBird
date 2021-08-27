using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    float time = 0;
    float moveTime = 0;

    private bool click = false;
   
    public GameObject[] ex;
    public GameObject character;
    float move = 0.005f;
    Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = character.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(click == false)
        {
            if (moveTime > 0.5f)
            {
                move = -move;
                moveTime = 0;
            }
            character.transform.position += new Vector3(0, move, 0);

            moveTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(click == false)
            {
                click = true;
                time = 0;
                character.GetComponent<Rigidbody>().useGravity = true;
                StartCoroutine(exa());

            }
        }

    }
    IEnumerator exa()
    {
        while (true)
        {
            if (Time.timeScale != 0)
            {
                if (time < 1f)
                {
                    for (int i = 0; i < ex.Length; i++)
                    {
                        ex[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - time);
                    }
                }
                else
                {
                    for (int i = 0; i < ex.Length; i++)
                    {
                        ex[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    }
                    for (int i = 0; i < ex.Length; i++)
                    {
                        ex[i].SetActive(false);
                    }
                    yield return null;
                    break;
                }
                time += Time.deltaTime;
                yield return null;
            }
        }

    }
}
