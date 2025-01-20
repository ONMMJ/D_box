using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Game_DB : MonoBehaviour
{
    //휘발성 게임 정보
    public static int life; //남은 목숨
    public static float time; //남은 시간
    public static int use_life; //목숨을 사용하여 지나간 횟수
    public static bool Is_Game_Start;
    public static int Ending_num;

    //저장용 게임 정보
    public static int Play_Count = 0;   //게임한 횟수
    public static int[] Ending_Clear = { 0, 0, 0, 0, 0, 0 };   //엔딩 클리어 여부(노말1, 노말2, 노말3, 히든, 진, 배드)


    public static bool Is_Coroutine;
    public static GameObject Background_color;
    private static GameObject SayText;

    private static Game_Director Game_Director;

    // Start is called before the first frame update
    void Awake()
    {
        Background_color = GameObject.Find("Never_Destroy").transform.Find("Canvas").transform.Find("Background_color").gameObject;
        SayText = GameObject.Find("Never_Destroy").transform.Find("Canvas").transform.Find("SayText").transform.Find("SayText").gameObject;
        Game_Director = GameObject.Find("Never_Destroy").transform.Find("Game_Director").GetComponent<Game_Director>();
    }

    void Start()
    {
        default_DB();
        File_Load();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void File_Save()    //저장
    {
        StreamWriter sw = new StreamWriter(Application.dataPath + "/Save.txt");
        Debug.Log(Application.dataPath + "/Save.txt");
        sw.WriteLine(Play_Count.ToString()); // 줄단위로 파일에 입력
        for (int i = 0; i < Ending_Clear.Length; i++)
        {
            sw.WriteLine(Ending_Clear[i].ToString());
        }
        sw.Flush(); // 파일 쓰기 반드시 해준다.
        sw.Close(); // 파일 쓰기 반드시 해준다.
    }
    public static void File_Load()    //처음 게임 켤 때 정보 불러오기
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/Save.txt");
        string input = "";
        // 파일을 줄단위로 읽는다.
        input = sr.ReadLine();
        if (input != null)
            Play_Count = int.Parse(input);
        for (int i = 0; i < Ending_Clear.Length; i++)
        {
            input = sr.ReadLine();
            if (input != null)
                Ending_Clear[i] = int.Parse(input);
        }
        sr.Close(); // 파일 읽기후 반드시 해준다.
    }

    private void OnApplicationQuit()        //게임 종료 시 동작
    {
        
    }

    public static void Game_Start()
    {
        time = 60;
        Is_Game_Start = true;
    }

    private static void default_DB()
    {
        life = 5;
        time = 60;
        use_life = 0;
        Is_Game_Start = false;

        Is_Coroutine = false;
    }

    public static void reset()
    {
        GameObject Never_delete = GameObject.Find("Never_Destroy");
        if (Never_delete != null)
        {
            Never_destory.instance = null;
            Destroy(Never_delete);
        }
        default_DB();
    }

    public static IEnumerator Red_to_Black_Fade_OUT(string text)    //목숨 사용 죽음
    {
        Is_Coroutine = true;
        SayText.transform.parent.gameObject.SetActive(false);
        Color c = Background_color.GetComponent<Image>().color;
        Background_color.GetComponent<Image>().raycastTarget = true;
        Background_color.GetComponent<Image>().color = new Color(1, 0, 0, 0.6f);
        yield return new WaitForSeconds(0.2f);
        Background_color.GetComponent<Image>().color = new Color(1, 0, 0, 0.4f);
        yield return new WaitForSeconds(0.2f);
        Background_color.GetComponent<Image>().color = new Color(1, 0, 0, 0.6f);
        yield return new WaitForSeconds(0.5f);
        SayText.transform.parent.gameObject.SetActive(true);
        SayText.GetComponent<Text>().text = text;
        yield return new WaitForSeconds(0.5f);
        for (float f = 0.4f; f >= 0; f -= 0.01f)
        {
            c.a = 1-f;
            c.r = f*2.5f;
            Background_color.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(.06f);
        }
        SayText.transform.parent.gameObject.SetActive(false);
        life -= 1;
        use_life += 1;
        Game_Director.setLife(life);
        if (Play_Count == 0 && life == 4)   //처음 죽었을때
        {
            SceneManager.LoadScene("First_Room");
            for (float f = 1f; f >= 0; f -= 0.02f)
            {
                c.a = f;
                Background_color.GetComponent<Image>().color = c;
                yield return new WaitForSeconds(.02f);
            }
            Background_color.GetComponent<Image>().raycastTarget = false;
            SayText.transform.parent.gameObject.SetActive(true);
            SayText.GetComponent<Text>().text = "죽은 것 같은데.. 다시 살아난건가?";
            Is_Coroutine = false;
        }
        else if (life > 0)   //목숨이 남았을 때 첫번째 방으로 이동
        {
            SceneManager.LoadScene("First_Room");
            for (float f = 1f; f >= 0; f -= 0.02f)
            {
                c.a = f;
                Background_color.GetComponent<Image>().color = c;
                yield return new WaitForSeconds(.02f);
            }
            Background_color.GetComponent<Image>().raycastTarget = false;
            Game_Start();
            Is_Coroutine = false;
        }
        else if (life == 0)
        {
            Play_Count += 1;
            Game_DB.Ending_Clear[5] = 1;
            Ending_num = 6;
            Background_color.GetComponent<Image>().raycastTarget = false;
            Is_Coroutine = false;
            Destroy(GameObject.Find("Never_Destroy"));
            Never_destory.instance = null;
            SceneManager.LoadScene("Ending");
        }
    }

    public static IEnumerator Time_OUT()     //시간초과 죽음
    {
        Is_Coroutine = true;
        SayText.transform.parent.gameObject.SetActive(false);
        Color c = Background_color.GetComponent<Image>().color;
        Background_color.GetComponent<Image>().raycastTarget = true;
        Background_color.GetComponent<Image>().color = new Color(0, 1, 0, 0.6f);
        yield return new WaitForSeconds(0.2f);
        Background_color.GetComponent<Image>().color = new Color(0, 1, 0, 0.4f);
        yield return new WaitForSeconds(0.2f);
        Background_color.GetComponent<Image>().color = new Color(0, 1, 0, 0.6f);
        yield return new WaitForSeconds(0.5f);
        SayText.transform.parent.gameObject.SetActive(true);
        SayText.GetComponent<Text>().text = "콜록..ㅋㅗ..록.. 독.. 가스..  인가?";
        yield return new WaitForSeconds(0.5f);
        for (float f = 0.4f; f >= 0; f -= 0.01f)
        {
            c.a = 1 - f;
            c.g = f * 2.5f;
            Background_color.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(.06f);
        }
        SayText.transform.parent.gameObject.SetActive(false);
        life -= 1;
        Game_Director.setLife(life);
        if (Play_Count == 0 && life == 4)   //처음 죽었을때
        {
            SceneManager.LoadScene("First_Room");
            for (float f = 1f; f >= 0; f -= 0.02f)
            {
                c.a = f;
                Background_color.GetComponent<Image>().color = c;
                yield return new WaitForSeconds(.02f);
            }
            Background_color.GetComponent<Image>().raycastTarget = false;
            SayText.transform.parent.gameObject.SetActive(true);
            SayText.GetComponent<Text>().text = "죽은 것 같은데.. 다시 살아난건가?";
            Is_Coroutine = false;
        }
        else if (life > 0)   //목숨이 남았을 때 첫번째 방으로 이동
        {
            SceneManager.LoadScene("First_Room");
            for (float f = 1f; f >= 0; f -= 0.02f)
            {
                c.a = f;
                Background_color.GetComponent<Image>().color = c;
                yield return new WaitForSeconds(.02f);
            }
            Background_color.GetComponent<Image>().raycastTarget = false;
            Game_Start();
            Is_Coroutine = false;
        }
        else if (life == 0)
        {
            Play_Count += 1;
            Game_DB.Ending_Clear[5] = 1;
            Ending_num = 6;
            Background_color.GetComponent<Image>().raycastTarget = false;
            Is_Coroutine = false;
            Destroy(GameObject.Find("Never_Destroy"));
            Never_destory.instance = null;
            SceneManager.LoadScene("Ending");
        }
    }

    public static IEnumerator Fade_IN()
    {
        for (float f = 1f; f >= 0; f -= 0.02f)
        {
            Color c = Background_color.GetComponent<Image>().color;
            c.a = f;
            Background_color.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(.02f);
        }
    }
    public static IEnumerator Ending_Fade_Out()
    {
        Is_Coroutine = true;
        SayText.transform.parent.gameObject.SetActive(false);
        Background_color.GetComponent<Image>().raycastTarget = true;
        Color c1 = Background_color.GetComponent<Image>().color;
        c1.r = 0;
        Background_color.GetComponent<Image>().color = c1;
        for (float f = 0f; f <= 1; f += 0.02f)
        {
            Color c = Background_color.GetComponent<Image>().color;
            c.a = f;
            Background_color.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(.02f);
        }
        Background_color.GetComponent<Image>().raycastTarget = false;
        Is_Coroutine = false;
        Destroy(GameObject.Find("Never_Destroy"));
        Never_destory.instance = null;
        SceneManager.LoadScene("Ending");
    }
}
