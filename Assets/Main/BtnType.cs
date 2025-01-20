using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler

{
    public BTNType currentType;
    public Transform buttonScale;
    private Vector3 defaultScale;
    public CanvasGroup mainGroup;
    public CanvasGroup OptionGroup;

    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void HoverSound()
    {
        myFx.PlayOneShot(hoverFx);
    }
    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }





    private void Start()
    {
        defaultScale = buttonScale.localScale;

    }


    bool isSound;
    public void OnBtnClick()
    {
        switch(currentType)
        {
            case BTNType.START:
                Debug.Log("시작");
                break;
            case BTNType.TITLE:
                CanvasGroupOn(OptionGroup);
                CanvasGroupOFF(mainGroup);
                Debug.Log("타이틀");
                break;
            case BTNType.SOUND:

                MutePressed();

                break;
            case BTNType.OPTION:
                CanvasGroupOn(OptionGroup);
                CanvasGroupOFF(mainGroup);
                Debug.Log("옵션");
                break;
            case BTNType.BACK:
                CanvasGroupOFF(OptionGroup);
                CanvasGroupOn(mainGroup);
                Debug.Log("뒤로가기");
                break;
            case BTNType.EXIT:
                Application.Quit();
                Debug.Log("종료");
                break;

        }

    }

   
    public void MutePressed()
    {
        isSound = !isSound;
        AudioListener.pause = isSound;
    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOFF(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

   
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
