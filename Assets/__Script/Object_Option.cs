using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_Option : MonoBehaviour
{
    public Sprite image;
    public string Text;
    public string Use_Item_name;
    public string Use_Text;
    public int Pick_Item_ID;
    public int Used_Item_ID;
    public Camera Applied_camera;
    public static int[] Is_Pick = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //열쇠1, 와인, 치약, 매니큐어, 성냥, 열쇠2, 라이터, 수건, 열쇠3, 열쇠2바닥, 코르크마개열쇠
    public static int[] Is_Used = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 ,0 }; //문1, 문2, 문3, 열쇠철문, 문제철문, 전기문, 문1버튼, 코르크마개열쇠, 힌트지우기, back문1, back문2, 깊은물, 최종문, 라이터방투명벽 ,button1, button2, button3

    private Vector3 Default_position;
    private Vector3 Default_Rotation;
    private bool Is_Action;
    public Vector3 Action_position;
    public Vector3 Action_Rotation;

    // Start is called before the first frame update
    void Start()
    {
        if(Pick_Item_ID != 0 && Is_Pick[Pick_Item_ID-1] > 0)
        {
            Destroy(gameObject);
        }
        Default_position = transform.localPosition;
        Default_Rotation = transform.localRotation.eulerAngles;
        Is_Action = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void No_Item_Used(GameObject SayText)
    {
        SayText.GetComponent<Text>().text = Use_Text;
        GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Use_Director>().Input_Y_N(Used_Item_ID, gameObject);
    }
    public void Used_Item(GameObject SayText, GameObject used_Item)
    {
        if (used_Item == null && Object_Option.Is_Used[Used_Item_ID - 1] >= 1)
        {
            SayText.GetComponent<Text>().text = Use_Text;
            if (Used_Item_ID == 1 || Used_Item_ID == 2 || Used_Item_ID == 4 || Used_Item_ID == 5 || Used_Item_ID == 13)
                GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Use_Director>().Select(Used_Item_ID, gameObject);
        }
        else if (Use_Item_name == used_Item.GetComponent<Image>().sprite.name || (Used_Item_ID == 4 && used_Item.GetComponent<Image>().sprite.name == "Zippolighter"))
        {
            SayText.GetComponent<Text>().text = Use_Text;
            GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Use_Director>().Select(Used_Item_ID, gameObject);
            Object_Option.Is_Used[Used_Item_ID-1] = 1;
            used_Item.GetComponent<Image>().sprite = null;
        }
        else if (Use_Item_name != used_Item.GetComponent<Image>().sprite.name)
        {
            if (Used_Item_ID == 4 && used_Item.GetComponent<Image>().sprite.name == "Match")
            {
                SayText.GetComponent<Text>().text = "젖어서 불이 붙지 않는다.";
            }
            else if (Used_Item_ID == 4 && used_Item.GetComponent<Image>().sprite.name == "Combi1")
            {
                SayText.GetComponent<Text>().text = "치약으로는 아무 효과가 없는거 같다.";
            }
            else
            {
                SayText.GetComponent<Text>().text = "여기엔 사용할 수 없다.";
            }
        }
    }

    public void Action_Object()
    {
        if (!Is_Action)
        {
            transform.localPosition = Action_position;
            transform.localRotation = Quaternion.Euler(Action_Rotation);
            Is_Action = true;
        }
        else
        {
            transform.localPosition = Default_position;
            transform.localRotation = Quaternion.Euler(Default_Rotation);
            Is_Action = false;
        }
    }
    
    public void Is_Pick_Item(int i)
    {
        Is_Pick[Pick_Item_ID-1] = 1;
    }
}
