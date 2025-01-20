using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Room_Director : MonoBehaviour
{
    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Click_Director>().Change_Scene(GameObject.Find("Main Camera").GetComponent<Camera>());
        if (Object_Option.Is_Used[2] == 1)
        {
            GameObject.Find("Room").transform.Find("Interior").transform.Find("First_door").transform.Find("doorWing").transform.localRotation = Quaternion.Euler(GameObject.Find("Room").transform.Find("Interior").transform.Find("First_door").transform.Find("doorWing").GetComponent<Object_Option>().Action_Rotation);
            GameObject.Find("Room").transform.Find("Interior").transform.Find("First_door").transform.Find("doorWing").tag = "Action_Object";
        }
        if (Object_Option.Is_Used[13] == 1)
        {
            Destroy(GameObject.Find("Mini_Room_Wall").gameObject);
            GameObject.Find("Zippo model").transform.position = new Vector3(1, 0.12f, -5.8f);
        }
        if (Object_Option.Is_Used[8] == 1)
        {
            Destroy(GameObject.Find("Room").transform.Find("Walls").transform.Find("Dirty").gameObject);
        }
        if (Object_Option.Is_Used[15] == 1)
        {
            GameObject.Find("Room").transform.Find("Interior").transform.Find("First_door").transform.Find("doorWing").transform.localRotation = Quaternion.Euler(GameObject.Find("Room").transform.Find("Interior").transform.Find("First_door").transform.Find("doorWing").GetComponent<Object_Option>().Action_Rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
