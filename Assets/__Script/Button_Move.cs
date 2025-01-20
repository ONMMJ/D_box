using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Move : MonoBehaviour
{
    bool click;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button()
    {
        transform.localPosition = new Vector3(0, -0.01f, 0);
        StartCoroutine(back());
    }

    IEnumerator back()
    {
        while(transform.localPosition.y <= 0.005)
        {
            transform.Translate(new Vector3(0, 0.05f*Time.deltaTime, 0));
            yield return new WaitForSeconds(.001f);
        }
    }
}
