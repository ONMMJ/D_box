using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Corks_Key : MonoBehaviour
{
    float loof_time;
    float now_time;
    int up_down;

    // Start is called before the first frame update
    void Start()
    {
        up_down = -1;
        now_time = Time.time;
        loof_time = now_time + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        now_time += Time.deltaTime;
        if (loof_time <= now_time)
        {
            loof_time = now_time + 0.5f;
            up_down *= -1;
        }
        if (transform.position.x >= -2.4)
        {
            transform.Translate(-1 * Time.deltaTime, up_down * 0.1f * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(0, up_down * 0.1f * Time.deltaTime, 0);
        }
    }
}
