using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 生産　UI　クラス
/// </summary>
public class ProductionCanvas : SelectCanvasInterface
{

    //[SerializeField] CreateRobotData setRobot;

    bool endToggle = false;

    //GameObject originalRobot = null;

    public bool createStart = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (endToggle) return;

        //アニメ終了時
        if (startAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
            endToggle = true;
            startAnimationCanvas.SetActive(false);

            createStart = true;

            if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Production)
            {

                TutorialManager.Instance.ProductionTimerSet(TutorialProductionStart);
               
            }
            else
            {
                cameraController.ActionEnd();
            }


            this.gameObject.SetActive(false);

            Camera.main.GetComponent<CameraController>().GetCenterIsland.GetComponent<IslandBase>().timer.GetComponent<Canvas>().enabled = true;

        }
        else if (tagAnimationCanvas.GetComponent<AnimationCallBack>().GetCallBack == true)
        {
            tagAnimationCanvas.SetActive(false);
            startAnimationCanvas.SetActive(true);
        }
    }


    public override void StartButton()
    {

        mainCanvas.SetActive(false);
        cameraController.backButton.SetActive(false);


        //研究値から生産できるロボットを計算
        CharacterManager.Instance.selectedCharacter[0] = selectChara[0].GetComponent<SelectCharacterDataInterface>().originalGameObject;
        CharacterManager.Instance.selectedCharacter[1] = selectChara[1].GetComponent<SelectCharacterDataInterface>().originalGameObject;

        //タッグ演出
        //if (CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tagName == CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().tagName
        //    &&
        //   CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tagName != "なし")
        //{
        //    tagAnimationCanvas.SetActive(true);

        //}
        //else
        //{
        //    startAnimationCanvas.SetActive(true);
        //}



        bool tagCheck = false;

        CharacterBase.TAG_LIST tag1, tag2;
        tag1 = CharacterManager.Instance.selectedCharacter[0].GetComponent<CharacterBase>().tag;
        tag2 = CharacterManager.Instance.selectedCharacter[1].GetComponent<CharacterBase>().tag;

        if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_CLEANING || tag1 == CharacterBase.TAG_LIST.TAG_CLEANING && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)                               // ロボット工学 & 清掃
        {
            tagCheck = true;
        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION || tag1 == CharacterBase.TAG_LIST.TAG_FUELOIL_COLLECTION && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)      // ロボット工学 & 重油回収
        {
            tagCheck = true;

        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_PLASTIC_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)          // ロボット工学 & プラスチック研究
        {
            tagCheck = true;

        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_ROBOTICS && tag2 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE || tag1 == CharacterBase.TAG_LIST.TAG_BATTERY_MANUFACTURE && tag2 == CharacterBase.TAG_LIST.TAG_ROBOTICS)    // ロボット工学 & バッテリー製造
        {
            tagCheck = true;

        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH)                                                                                                          // 自然調査員 & 自然調査員
        {
            tagCheck = true;

        }
        else if (tag1 == CharacterBase.TAG_LIST.TAG_CLEANING && tag2 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH || tag1 == CharacterBase.TAG_LIST.TAG_NATURE_RESEARCH && tag2 == CharacterBase.TAG_LIST.TAG_CLEANING)    // ロボット工学 & バッテリー製造
        {
            tagCheck = true;

        }
        else
        {
          
        }

        if(tagCheck)
        {
            tagAnimationCanvas.SetActive(true);
        }
        else
        {
            startAnimationCanvas.SetActive(true);
        }





        TutorialCursor.Instance.SetActive(false);

        audioSource.PlayOneShot(startButtonSound);

    }


    public void TutorialProductionStart()
    {
        GameObject obj = GameObject.Find("Production");
        obj.GetComponent<Production>().TutorialSetCanvas(this.gameObject);
        obj.GetComponent<Production>().ActionEnd();
    }
    
}
