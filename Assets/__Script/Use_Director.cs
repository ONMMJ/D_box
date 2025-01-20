using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Use_Director : MonoBehaviour
{
    public static GameObject Y_N_Button;
    public GameObject Corks_Key_Prefab;
    private int Y_N_ID;
    private GameObject SayText;
    private GameObject Un_Used_Object;  //아이템을 사용하지 않는 오브젝트
    // Start is called before the first frame update
    void Start()
    {
        Y_N_Button = GameObject.Find("Never_Destroy").transform.Find("Canvas").transform.Find("Y_N_Button").gameObject;
        SayText = GameObject.Find("Never_Destroy").transform.Find("Canvas").transform.Find("SayText").transform.Find("SayText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ON_OFF_Y_N_Button(bool ON)
    {
        if (ON)
        {
            Y_N_Button.SetActive(true);
        }
        else
        {
            Y_N_Button.SetActive(false);
        }
    }

    public void Select(int i, GameObject Used_Object)      //아이템을 사용하는 함수를 선택하는 함수
    {
        switch (i)
        {
            case 1:
                Use_Door1(Used_Object);
                break;
            case 2:
                Use_Door2(Used_Object);
                break;
            case 4:
                room3_key_Box(Used_Object);
                break;
            case 5:
                room3_Question_Box(Used_Object);
                break;
            case 9:
                Use_Towel(Used_Object);
                break;
            case 12:
                drop_Corks_Key();
                break;
            case 13:
                Real_Ending();
                break;
        }
    }

    public void Input_Y_N(int index, GameObject G)
    {
        ON_OFF_Y_N_Button(true);
        Y_N_ID = index;
        Un_Used_Object = G;
    }

    public void Select_Y_N()    //아이템을 사용하지 않는 함수를 선택하는 함수
    {
        switch (Y_N_ID)
        {
            case 3:
                last_door();
                break;
            case 6:
                Use_Volt_Door();
                break;
            case 7:
                Use_Door1_Button();
                break;
            case 10:
                Change_scene("First_Room");
                break;
            case 11:
                Change_scene("Second_Room");
                break;
            case 14:
                IN_room_Zippo();
                break;
            case 15:
                No_Button();
                break;
            case 16:
                Yes_Button();
                break;
            case 17:
                No_Button();
                break;
        }
        Click_Director.Is_No_Item_Use_Click = false;
        GameObject.Find("Canvas").transform.Find("On_Inven").GetComponent<Button>().enabled = true;
        ON_OFF_Y_N_Button(false);
    }

    public void Select_No()
    {
        Click_Director.Is_No_Item_Use_Click = false;
        GameObject.Find("Canvas").transform.Find("On_Inven").GetComponent<Button>().enabled = true;
        ON_OFF_Y_N_Button(false);
    }

    private void Real_Ending()
    {
        Game_DB.Play_Count += 1;
        Game_DB.Ending_Clear[4] = 1;
        Debug.Log("real");
        Game_DB.Ending_num = 5;
        Game_DB.File_Save();
        StartCoroutine(Game_DB.Ending_Fade_Out());
    }

    //아이템 사용하지 않는 함수
    private void last_door()    //세번째 결말문(엔딩)
    {
        if(Object_Option.Is_Used[2] == 0)
        {
            SayText.transform.parent.gameObject.SetActive(true);
            SayText.GetComponent<Text>().text = "잠겨있다.";
        }
        else if(Object_Option.Is_Used[2] == 1)  //엔딩
        {
            Debug.Log(Game_DB.life);
            Debug.Log(Game_DB.use_life);
            if (Game_DB.use_life == 0 && Game_DB.life == 5)
            {
                Game_DB.Play_Count += 1;
                Game_DB.Ending_Clear[3] = 1;
                Debug.Log("불량");
                Game_DB.Ending_num = 4;
                Game_DB.File_Save();
                StartCoroutine(Game_DB.Ending_Fade_Out());
            }
            else if (Game_DB.use_life == 0 && Game_DB.life < 5)
            {
                Game_DB.Play_Count += 1;
                Game_DB.Ending_Clear[0] = 1;
                Debug.Log("A");
                Game_DB.Ending_num = 1;
                Game_DB.File_Save();
                StartCoroutine(Game_DB.Ending_Fade_Out());
            }
            else if(Game_DB.use_life < 4)
            {
                Game_DB.Play_Count += 1;
                Game_DB.Ending_Clear[1] = 1;
                Debug.Log("B");
                Game_DB.Ending_num = 2;
                Game_DB.File_Save();
                StartCoroutine(Game_DB.Ending_Fade_Out());
            }
            else if (Game_DB.use_life >= 4)
            {
                Game_DB.Play_Count += 1;
                Game_DB.Ending_Clear[2] = 1;
                Debug.Log("C");
                Game_DB.Ending_num = 3;
                Game_DB.File_Save();
                StartCoroutine(Game_DB.Ending_Fade_Out());
            }
        }
    }
    private void Use_Volt_Door()    //두번째 방 전기문
    {
        if(Object_Option.Is_Pick[5] == 1)       //1: 인벤토리 있는 상태, 2: 바닥에 버려진 상태, 3: 사용한 상태
        {
            StartCoroutine(Game_DB.Red_to_Black_Fade_OUT("으에ㅔㄹ엙"));
            Object_Option.Is_Pick[5] = 2;
            Un_Used_Object.GetComponent<AudioSource>().Play();
            GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Click_Director>().delete_room2key();
        }
        else if(Object_Option.Is_Pick[9] == 1)
        {
            StartCoroutine(Game_DB.Red_to_Black_Fade_OUT("으에ㅔㄹ엙"));
            Object_Option.Is_Pick[9] = 0;
            Un_Used_Object.GetComponent<AudioSource>().Play();
            GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Click_Director>().delete_room2key();
        }
        else
        {
            Camera Door_Camera = GameObject.Find("Door_Camera").GetComponent<Camera>();
            Camera Main_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (Door_Camera.enabled == false)
            {
                Door_Camera.enabled = true;
                Main_Camera.enabled = false;
                GameObject.Find("Room").transform.Find("RevolvingDoor").transform.Find("Volt_Gate").GetComponent<Object_Option>().Applied_camera = Door_Camera;
                GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Click_Director>().Set_Now_Camera(Door_Camera);
            }
            else
            {
                Main_Camera.enabled = true;
                Door_Camera.enabled = false;
                GameObject.Find("Room").transform.Find("RevolvingDoor").transform.Find("Volt_Gate").GetComponent<Object_Option>().Applied_camera = Main_Camera;
                GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Click_Director>().Set_Now_Camera(Main_Camera);
            }
        }
    }

    private void Use_Door1_Button()     //첫번째 방 버튼
    {
        Object_Option.Is_Used[0] = 1;
        Object_Option.Is_Used[6] = 1;
        Un_Used_Object.GetComponent<AudioSource>().Play();
        StartCoroutine(Game_DB.Red_to_Black_Fade_OUT("윽.. 정신이 희미해진다.."));
    }

    private void IN_room_Zippo()
    {
        Un_Used_Object.GetComponent<AudioSource>().Play();
        StartCoroutine(Game_DB.Red_to_Black_Fade_OUT("..."));
        Object_Option.Is_Used[13] = 1;
    }

    private void Yes_Button()
    {
        Un_Used_Object.GetComponent<Button_Move>().Button();
        if (Object_Option.Is_Used[15] == 0)
        {
            Object_Option.Is_Used[15] = 1;
            Object_Option.Is_Used[2] = 1;
            SayText.transform.parent.gameObject.SetActive(true);
            SayText.GetComponent<Text>().text = "무슨 소리가 들린 것 같다";
            GameObject.Find("Room").transform.Find("Interior").transform.Find("First_door").transform.Find("doorWing").transform.localRotation = Quaternion.Euler(GameObject.Find("Room").transform.Find("Interior").transform.Find("First_door").transform.Find("doorWing").GetComponent<Object_Option>().Action_Rotation);
            GameObject.Find("Room").transform.Find("Interior").transform.Find("First_door").transform.Find("doorWing").tag = "Action_Object";
            if (Game_DB.use_life == 0 && Game_DB.life == 5)  //진엔딩 조건
            {
                Object_Option.Is_Used[12] = 1;
            }
        }
    }

    private void No_Button()
    {
        Un_Used_Object.GetComponent<Button_Move>().Button();
        Un_Used_Object.GetComponent<AudioSource>().Play();
        StartCoroutine(Game_DB.Red_to_Black_Fade_OUT("이게..아닌..가..?"));
    }



    //아이템 사용 함수

    private void Use_Door1(GameObject Used_Object)  //첫번째 방 문 잠금 해제
    {
         Used_Object.transform.localRotation = Quaternion.Euler(Used_Object.GetComponent<Object_Option>().Action_Rotation);
        Used_Object.GetComponent<AudioSource>().Play();
         StartCoroutine(WaitDoor1());
    }
    IEnumerator WaitDoor1()
    {
        yield return new WaitForSeconds(1.0f);
        Change_scene("Second_Room");
    }
    private void Use_Door2(GameObject Used_Object)  //두번째 방 문 잠금 해제
    {
        Used_Object.transform.localRotation = Quaternion.Euler(Used_Object.GetComponent<Object_Option>().Action_Rotation);
        Object_Option.Is_Pick[5] = 3;
        Object_Option.Is_Pick[9] = 3;
        Object_Option.Is_Pick[10] = 3;
        Used_Object.GetComponent<AudioSource>().Play();
        StartCoroutine(WaitDoor2());
    }
    IEnumerator WaitDoor2()
    {
        yield return new WaitForSeconds(1.0f);
        Change_scene("Third_Room");
    }

    private void drop_Corks_Key()   // 두번째 방 물에 코르크마개 열쇠 넣기
    {
        Object_Option.Is_Pick[5] = 3;
        Object_Option.Is_Used[11] = 1;
        GameObject Cork_key = Instantiate(Corks_Key_Prefab, new Vector3(8, -3.4f, 10.5f), Quaternion.identity);
        Cork_key.GetComponent<Object_Option>().Applied_camera = GameObject.Find("Door_Camera").GetComponent<Camera>();
    }

    private void room3_key_Box(GameObject Used_Object)   //세번째 방 철문 심지
    {
        if (Object_Option.Is_Used[3] == 0)
        {
            SayText.transform.parent.gameObject.SetActive(false);
            Used_Object.transform.Find("simji").GetComponent<ParticleSystem>().Play();
            Used_Object.transform.Find("simji").GetComponent<AudioSource>().Play();
            StartCoroutine(WaitSimji(Used_Object));
        }
        else if (Object_Option.Is_Used[3] == 1)
        {
            Used_Object.transform.localRotation = Quaternion.Euler(Used_Object.GetComponent<Object_Option>().Action_Rotation);
            Used_Object.tag = "Action_Object";
            Object_Option.Is_Used[3] = 2;
        }
    }

    IEnumerator WaitSimji(GameObject Box_object)
    {
        Game_DB.Is_Coroutine = true;
        yield return new WaitForSeconds(3.0f);
        Destroy(Box_object.transform.Find("simji").gameObject);
        SayText.transform.parent.gameObject.SetActive(true);
        Box_object.GetComponent<AudioSource>().Play();
        SayText.GetComponent<Text>().text = "무슨 소리가 들린 것 같다";
        Game_DB.Is_Coroutine = false;
    }

    private void room3_Question_Box(GameObject Used_Object) //문제 철문
    {
        if (Object_Option.Is_Used[4] == 0)
        {
            Used_Object.transform.localRotation = Quaternion.Euler(Used_Object.GetComponent<Object_Option>().Action_Rotation);
            Used_Object.tag = "Action_Object";
            Used_Object.GetComponent<AudioSource>().Play();
            Object_Option.Is_Used[4] = 2;
        }
        else if (Object_Option.Is_Used[4] == 1)
        {
            Used_Object.transform.localRotation = Quaternion.Euler(Used_Object.GetComponent<Object_Option>().Action_Rotation);
            Used_Object.tag = "Action_Object";
            Used_Object.GetComponent<AudioSource>().Play();
            Object_Option.Is_Used[4] = 2;
        }
    }

    private void Use_Towel(GameObject Used_Object)    //힌트 벽 지우기
    {
        if (Object_Option.Is_Used[8] == 0)
        {
            StartCoroutine(Wash_Wall(Used_Object));
        }
    }
    IEnumerator Wash_Wall(GameObject Used_Object)
    {
        for (float f = 1f; f >= 0; f -= 0.02f)
        {
            Color c = Used_Object.GetComponent<Renderer>().material.color;
            c.a = f;
            Used_Object.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(.03f);
        }
        Destroy(Used_Object);
    }

    private void Change_scene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
}
