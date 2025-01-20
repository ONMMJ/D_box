using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Director : MonoBehaviour
{
    Text timeText;
    Text lifeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GameObject.Find("Never_Destroy").transform.Find("Canvas").transform.Find("Time").GetComponent<Text>();
        lifeText = GameObject.Find("Never_Destroy").transform.Find("Canvas").transform.Find("Life").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game_DB.Is_Coroutine)
        {
            Game_DB.time -= Time.deltaTime;
            timeText.text = ((int)Game_DB.time).ToString();
        }
        if( Game_DB.time <= 0)
        {
            Game_DB.time = 60;
            StartCoroutine(Game_DB.Time_OUT());
        }
    }

    public void setLife(int count)
    {
        string text = "";
        for (int i = 0; i < count; i++)
        {
            text += "■";
        }
        lifeText.text = text;
    }
}
