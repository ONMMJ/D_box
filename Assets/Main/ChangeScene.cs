using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start_game()
    {
        StartCoroutine(wait_time());
    }
    private IEnumerator wait_time()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("First_Room");
        Game_DB.Game_Start();
    }
}
