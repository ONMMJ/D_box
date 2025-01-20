using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_scene : MonoBehaviour
{
    public static List<GameObject> First_Scene;
    private static List<GameObject> Second_Scene;
    private static List<GameObject> Third_Scene;
    // Start is called before the first frame update
    void Start()
    {
        First_Scene = new List<GameObject>();
        Second_Scene = new List<GameObject>();
        Third_Scene = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Save(int i, GameObject[] Scene_Object)
    {
        if (i == 1)
        {
            First_Scene.Clear();
            foreach(GameObject temp in Scene_Object)
            {
                if (temp.transform.parent==null)
                {
                    if (temp != GameObject.Find("Never_Destory") || temp != GameObject.Find("Main Camera"))
                        First_Scene.Add(temp);
                }
            }
        }
        else if (i == 2)
        {
            Second_Scene.Clear();
            foreach (GameObject temp in Scene_Object)
            {
                if (temp.transform.parent == null)
                {
                    if (temp != GameObject.Find("Never_Destory") || temp != GameObject.Find("Main Camera"))
                        Second_Scene.Add(temp);
                }
            }
        }
        else if (i == 3)
        {
            Third_Scene.Clear();
            foreach (GameObject temp in Scene_Object)
            {
                if (temp.transform.parent == null)
                {
                    if (temp != GameObject.Find("Never_Destory") || temp != GameObject.Find("Main Camera"))
                        Third_Scene.Add(temp);
                }
            }
        }
    }
    
    public static void Load(int i)
    {
        if (i == 1 && First_Scene.Count > 0)
        {
            foreach (GameObject temp in FindObjectsOfType<GameObject>())
            {
                if (temp.transform.parent == null)
                {
                    if (temp == GameObject.Find("Never_Destory") || temp == GameObject.Find("Main Camera")) { }
                    else
                        Destroy(temp);
                }
            }
            foreach (GameObject temp in First_Scene)
            {
                Instantiate(temp, temp.transform.position, temp.transform.rotation);
            }
        }
        else if (i == 2 && Second_Scene.Count > 0)
        {
            foreach (GameObject temp in FindObjectsOfType<GameObject>())
            {
                if (temp.transform.parent == null)
                {
                    if (temp != GameObject.Find("Never_Destory") || temp != GameObject.Find("Main Camera"))
                        Destroy(temp);
                }
            }
            foreach (GameObject temp in Second_Scene)
            {
                Instantiate(temp, temp.transform.position, temp.transform.rotation);
            }
        }
        else if (i == 3 && Third_Scene.Count > 0)
        {
            foreach (GameObject temp in FindObjectsOfType<GameObject>())
            {
                if (temp.transform.parent == null)
                {
                    if (temp != GameObject.Find("Never_Destory") || temp != GameObject.Find("Main Camera"))
                        Destroy(temp);
                }
            }
            foreach (GameObject temp in Third_Scene)
            {
                Instantiate(temp, temp.transform.position, temp.transform.rotation);
            }
        }
    }
}
