using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Director : MonoBehaviour
{
    public Button[] Ending;
    // Start is called before the first frame update
    void Start()
    {
        Game_DB.File_Load();

        for (int i = 0; i < 6; i++)
        {
            if (Game_DB.Ending_Clear[i] == 1)
            {
                Debug.Log(Game_DB.Ending_Clear[i]);
                Ending[i].interactable = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
