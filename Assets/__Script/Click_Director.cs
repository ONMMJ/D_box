using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Click_Director : MonoBehaviour
{
    public Camera first_camera;
    private Camera now_camera;

    [SerializeField]
    private GameObject Grid;
    private Transform[] slots;

    bool Item_select;
    bool Is_Inven_open;
    bool Is_Click;
    bool Inven_stop;
    public static bool Is_No_Item_Use_Click;

    private GameObject First_Item;
    private GameObject Second_Item;
    public Sprite Com1;
    public Sprite Com2;
    public Sprite Com3;
    public Sprite Corkstopper;
    string[,] Item_Combi = new string[6, 3];

    bool Use_Item;
    GameObject used_Item;

    GameObject SayText;
    void Awake()
    {
        SayText = GameObject.Find("Never_Destroy").transform.Find("Canvas").transform.Find("SayText").transform.Find("SayText").gameObject;
    }

// Start is called before the first frame update
    void Start()
    {
        Item_Combi[0, 0] = "Match";
        Item_Combi[0, 1] = "toothpaste";
        Item_Combi[0, 2] = "Combi1";
        Item_Combi[1, 0] = "toothpaste";
        Item_Combi[1, 1] = "Match";
        Item_Combi[1, 2] = "Combi1";
        Item_Combi[2, 0] = "Match";
        Item_Combi[2, 1] = "manicure";
        Item_Combi[2, 2] = "Combi2";
        Item_Combi[3, 0] = "manicure";
        Item_Combi[3, 1] = "Match";
        Item_Combi[3, 2] = "Combi2";
        Item_Combi[4, 0] = "room2key";
        Item_Combi[4, 1] = "Corkstopper";
        Item_Combi[4, 2] = "Combi3";
        Item_Combi[5, 0] = "Corkstopper";
        Item_Combi[5, 1] = "room2key";
        Item_Combi[5, 2] = "Combi3";
        now_camera = first_camera;
        slots = Grid.gameObject.GetComponentsInChildren<Transform>();
        Item_select = false;
        Use_Item = false;
        Is_Inven_open = false;
        Is_Click = false;
        Is_No_Item_Use_Click = false;
        Inven_stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && SayText.transform.parent.gameObject.activeSelf == true && Use_Director.Y_N_Button.gameObject.activeSelf == false && Game_DB.Is_Coroutine == false)
        {
            if (Is_Click)
            {
                Is_Click = false;
            }
            else
            {
                SayText.transform.parent.gameObject.SetActive(false);
                Game_DB.Background_color.GetComponent<Image>().raycastTarget = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.G) && now_camera != first_camera && SceneManager.GetActiveScene().name != "Second_Room")
        {
            first_camera.enabled = true;
            now_camera.enabled = false;
            now_camera = first_camera;
            SayText.transform.parent.gameObject.SetActive(false);
            Game_DB.Background_color.GetComponent<Image>().raycastTarget = false;
        }
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            Save_scene.Save(1, FindObjectsOfType<GameObject>());
            SceneManager.LoadScene("Third_Room");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Save_scene.Save(3, FindObjectsOfType<GameObject>());
            SceneManager.LoadScene("First_Room");
        }*/
        RaycastHit hit;
        if (Physics.Raycast(now_camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity) && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !Is_No_Item_Use_Click)
        {
            GameObject hit_Object = hit.transform.gameObject;
            if (hit_Object.tag != "Untagged")
            {
                Game_DB.Background_color.GetComponent<Image>().raycastTarget = true;
                Is_Click = true;
                Inven_OFF();
                Camera Applied_camera = hit_Object.GetComponent<Object_Option>().Applied_camera;
                if (hit_Object.tag == "Pick_Object" && now_camera == Applied_camera)
                {
                    Sprite save_image = hit_Object.GetComponent<Object_Option>().image as Sprite;
                    if (hit_Object.GetComponent<Object_Option>().Text != null)
                    {
                        SayText.transform.parent.gameObject.SetActive(true);
                        SayText.GetComponent<Text>().text = hit.transform.GetComponent<Object_Option>().Text;
                    }
                    for (int i = 1; i < slots.Length; i = i + 2)
                    {
                        Transform slot_child = slots[i].transform.Find("ItemSlot");
                        if (slot_child.GetComponent<Image>().sprite == null)
                        {
                            slot_child.GetComponent<Image>().sprite = save_image;
                            hit_Object.GetComponent<Object_Option>().Is_Pick_Item(hit_Object.GetComponent<Object_Option>().Pick_Item_ID);
                            Destroy(hit.transform.gameObject);
                            Use_Item = false;
                            used_Item = null;
                            break;
                        }
                    }
                }
                else if (hit_Object.tag == "Object" && now_camera == Applied_camera)
                {
                    if (hit_Object.GetComponent<Object_Option>().Text != null)
                    {
                        SayText.transform.parent.gameObject.SetActive(true);
                        SayText.GetComponent<Text>().text = hit.transform.GetComponent<Object_Option>().Text;
                        Use_Item = false;
                        used_Item = null;
                    }
                }
                else if (hit_Object.tag == "Use_Object" && now_camera == Applied_camera)
                {
                    if (Use_Item || Object_Option.Is_Used[hit_Object.GetComponent<Object_Option>().Used_Item_ID - 1] >= 1)
                    {
                        SayText.transform.parent.gameObject.SetActive(true);
                        hit_Object.GetComponent<Object_Option>().Used_Item(SayText, used_Item);
                        Use_Item = false;
                        used_Item = null;
                    }
                    else if(!Use_Item)
                    {
                        if (hit_Object.GetComponent<Object_Option>().Text != null)
                        {
                            SayText.transform.parent.gameObject.SetActive(true);
                            SayText.GetComponent<Text>().text = hit.transform.GetComponent<Object_Option>().Text;
                        }
                    }
                }
                else if (hit_Object.tag == "No_Item_Use_Object" && now_camera == Applied_camera)
                {
                    SayText.transform.parent.gameObject.SetActive(true);
                    GameObject.Find("Canvas").transform.Find("On_Inven").GetComponent<Button>().enabled = false;
                    hit_Object.GetComponent<Object_Option>().No_Item_Used(SayText);
                    Use_Item = false;
                    used_Item = null;
                    Is_No_Item_Use_Click = true;
                    Game_DB.Background_color.GetComponent<Image>().raycastTarget = false;
                    Is_Click = false;
                }
                else if (hit_Object.tag == "Action_Object" && now_camera == Applied_camera)
                {
                    hit_Object.GetComponent<Object_Option>().Action_Object();
                    AudioSource Audio = hit_Object.GetComponent<AudioSource>();
                    if(Audio != null)
                    {
                        Audio.Play();
                    }
                    Game_DB.Background_color.GetComponent<Image>().raycastTarget = false;
                    Is_Click = false;
                }
                else if (now_camera != Applied_camera)
                {
                    Applied_camera.enabled = true;
                    now_camera.enabled = false;
                    now_camera = Applied_camera;
                    Use_Item = false;
                    used_Item = null;
                    Game_DB.Background_color.GetComponent<Image>().raycastTarget = false;
                    Is_Click = false;
                }
            }
        }
    }

    public void Inven_ON_OFF()
    {
        if (!Inven_stop)
        {
            if (Is_Inven_open)
            {
                GameObject.Find("Canvas").transform.Find("Inventory").gameObject.SetActive(false);
                GameObject.Find("Canvas").transform.Find("Use_Button").gameObject.SetActive(false);
                if (First_Item != null)
                {
                    First_Item.transform.parent.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
                    First_Item = null;
                    Item_select = false;
                }
                Is_Inven_open = false;
            }
            else
            {
                GameObject.Find("Canvas").transform.Find("Inventory").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("Use_Button").gameObject.SetActive(true);
                Is_Inven_open = true;
            }
        }
    }

    private void Inven_OFF()
    {
        GameObject.Find("Canvas").transform.Find("Inventory").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("Use_Button").gameObject.SetActive(false);
        if (First_Item != null)
        {
            First_Item.transform.parent.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
            First_Item = null;
            Item_select = false;
        }
        Is_Inven_open = false;
    }

    public void onClick_Item_Use()
    {
        if (Item_select && First_Item != null && Second_Item == null)
        {
            used_Item = First_Item;
            First_Item.transform.parent.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
            GameObject.Find("Canvas").transform.Find("Inventory").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Use_Button").gameObject.SetActive(false);
            First_Item = null;
            Item_select = false;
            Use_Item = true;
            Is_Inven_open = false;
        }
    }

    public void onClick_Item(GameObject target)
    {
        if (target.gameObject.GetComponent<Image>().sprite == null)
            return;
        Debug.Log(target.gameObject.GetComponent<Image>().sprite.name);
        if (Item_select && First_Item != target && Second_Item == null)
        {
            target.transform.parent.gameObject.GetComponent<Image>().color = new Color(0, 144, 60);
            Second_Item = target;
            Item_Combination();
        }
        else if (Item_select && First_Item == target && Second_Item == null)
        {
            if (First_Item.GetComponent<Image>().sprite.name == "wine")
            {
                GameObject C_Text = GameObject.Find("Canvas").transform.Find("Combination_Text").gameObject;
                C_Text.SetActive(true);
                First_Item.GetComponent<Image>().sprite = Corkstopper;
                C_Text.GetComponent<Text>().color = Color.blue;
                C_Text.GetComponent<Text>().text = "분해 성공";
                StartCoroutine(Waittime(C_Text));
            }
            First_Item = null;
            target.transform.parent.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
            Item_select = false;
        }
        else if (!Item_select)
        {
            target.transform.parent.gameObject.GetComponent<Image>().color = new Color(0, 144, 60);
            First_Item = target;
            Item_select = true;
        }
    }
    IEnumerator Waittime(GameObject C_Text)
    {
        yield return new WaitForSeconds(1.0f);
        C_Text.SetActive(false);
    }

    private void Item_Combination()
    {
        int index = 0;
        bool success = false;

        for (int i = 0; i < 6; i++)
        {
            if (Item_Combi[i, 0] == First_Item.gameObject.GetComponent<Image>().sprite.name)
            {
                index = i;
                if (Item_Combi[index, 1] == Second_Item.gameObject.GetComponent<Image>().sprite.name)
                {
                    if (Item_Combi[index, 2] == "Combi1")
                        Second_Item.gameObject.GetComponent<Image>().sprite = Com1;
                    else if (Item_Combi[index, 2] == "Combi2")
                        Second_Item.gameObject.GetComponent<Image>().sprite = Com2;
                    else if (Item_Combi[index, 2] == "Combi3")
                        Second_Item.gameObject.GetComponent<Image>().sprite = Com3;
                    First_Item.gameObject.GetComponent<Image>().sprite = null;
                    success = true;
                    break;
                }
            }
        }

        GameObject C_Text = GameObject.Find("Canvas").transform.Find("Combination_Text").gameObject;
        C_Text.SetActive(true);
        if (success)
        {
            C_Text.GetComponent<Text>().color = Color.green;
            C_Text.GetComponent<Text>().text = "조합 성공";
        }
        else
        {
            C_Text.GetComponent<Text>().color = Color.red;
            C_Text.GetComponent<Text>().text = "조합 실패";
        }

        Inven_stop = true;
        StartCoroutine(WaitForIt(C_Text));


    }
    IEnumerator WaitForIt(GameObject C_Text)
    {
        yield return new WaitForSeconds(1.0f);
        C_Text.SetActive(false);
        First_Item.transform.parent.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
        Second_Item.transform.parent.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
        First_Item = null;
        Second_Item = null;
        Item_select = false;
        Inven_stop = false;
    }

    public void Change_Scene(Camera Main)
    {

        first_camera = Main;
        now_camera = first_camera;
        SayText.transform.parent.gameObject.SetActive(false);
        Game_DB.Background_color.GetComponent<Image>().raycastTarget = false;
    }

    public void Set_Now_Camera(Camera camera)
    {
        now_camera = camera;
    }

    public void delete_room2key()
    {
        for (int i = 1; i < slots.Length; i = i + 2)
        {
            Transform slot_child = slots[i].transform.Find("ItemSlot");
            if(slot_child.GetComponent<Image>().sprite != null)
                if (slot_child.GetComponent<Image>().sprite.name == "room2key" || slot_child.GetComponent<Image>().sprite.name == "Combi3")
                {
                
                    slot_child.GetComponent<Image>().sprite = null;
                    break;
                }
        }
    }
}
