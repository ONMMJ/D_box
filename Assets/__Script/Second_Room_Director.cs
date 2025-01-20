using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Second_Room_Director : MonoBehaviour
{
    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Click_Director>().Change_Scene(GameObject.Find("Main Camera").GetComponent<Camera>());
        if(Object_Option.Is_Pick[5] == 2)
        {
            GameObject.Find("Floor_Key").transform.Find("Floor_Key").gameObject.SetActive(true);
        }
        if (Object_Option.Is_Pick[10] == 0 && Object_Option.Is_Used[11] == 1)
        {
            GameObject.Find("Corks_Key_Backup").transform.Find("Corks_Key_Backup").gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
