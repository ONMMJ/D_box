using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Ending_Director : MonoBehaviour
{
    public Sprite A_ending;
    public Sprite B_ending;
    public Sprite C_ending;
    public Sprite Hidden_ending;
    public Sprite Real_ending;
    public Sprite Bad_ending;
    public Image Ending_image;
    public Text Ending_text;
    // Start is called before the first frame update
    void Start()
    {

        Object_Option.Is_Used = Enumerable.Repeat(0, Object_Option.Is_Used.Length).ToArray();
        Object_Option.Is_Pick = Enumerable.Repeat(0, Object_Option.Is_Pick.Length).ToArray();
        switch (Game_DB.Ending_num)
        {
            case 1:
                Ending_image.sprite = A_ending;
                Ending_text.text = "Normal Ending1\nA급 판정";
                break;
            case 2:
                Ending_image.sprite = B_ending;
                Ending_text.text = "Normal Ending2\nB급 판정";
                break;
            case 3:
                Ending_image.sprite = C_ending;
                Ending_text.text = "Normal Ending3\nC급 판정";
                break;
            case 4:
                Ending_image.sprite = Hidden_ending;
                Ending_text.text = "Hidden Ending\n불량품 엔딩";
                break;
            case 5:
                Ending_image.sprite = Real_ending;
                Ending_text.text = "Real Ending1\n탈출..?";
                break;
            case 6:
                Ending_image.sprite = Bad_ending;
                Ending_text.text = "Bad Ending1\n폐기 처분..";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
