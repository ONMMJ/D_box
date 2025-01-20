using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_Room_Director : MonoBehaviour
{
    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Click_Director>().Change_Scene(GameObject.Find("Main Camera").GetComponent<Camera>());

        if (Object_Option.Is_Used[6] == 1)
            GameObject.Find("Stop_Button").transform.Find("default").gameObject.tag = "Object";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
