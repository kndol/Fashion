using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Dart_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform arrangePos = null;
    [SerializeField]
    Sprite[] completionSpite = null;  //0:티셔츠  1:블라우스  2:바지  3:치마

    UIBuilder uiDart;

    Text inputSize;
    Text firstSize;
    Text secondSize;
    Text thirdSize;
    Text forthSize;

    bool isCheck = true;

    void Start()
    {
        if (Data.MS == Making_State.start)
        {
            uiDart = Instantiate<UIBuilder>(uiCanvasPrefab);
        }
    }

    public void DartButton()
    {
        player.transform.position = arrangePos.transform.position;
        player.transform.rotation = arrangePos.transform.rotation;
        uiDart.Hide();
        Data.MS = Making_State.arrangement;
        Data.isCheck = true;
    }

    public void Front_Back_Button()   //앞뒤구분 설명 버튼  //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("앞/뒤 구분 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);   //적어
            isCheck = false;
        }
        else
        {
            Description_Back();
            isCheck = true;
        }
    }

    public void Ford_Button()         //골선 설명 버튼      //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("골선 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);    //적어
            isCheck = false;
        }
        else
        {
            Description_Back();
            isCheck = true;
        }
    }

    public void Selvage_Button()      //식서 설명 버튼      //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("식서 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);  //적어
            isCheck = false;
        }
        else
        {
            Description_Back();
            isCheck = true;
        }
    }

    public void ArmHole_Pos_Button()  //암홀위치 설명 버튼  //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("암홀위치 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);   //적어
            isCheck = false;
        }
        else
        {
            Description_Back();
            isCheck = true;
        }
    }

    public void Description_Back()
    {
        Destroy(uiDart.gameObject);
        uiDart = Instantiate<UIBuilder>(uiCanvasPrefab);
        Show_Confirm();
    }

    public void Size_Input(int i)
    {
        
    }

    public void Show_Confirm()
    {
        uiDart.AddLabel("패턴완성", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        uiDart.AddLabel("Description", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
        uiDart.AddDivider(UIBuilder.PANE_LEFT);
        uiDart.AddDivider(UIBuilder.PANE_RIGHT);
        switch (Data.CS)
        {
            case Cloth_State.t_shirts:
                uiDart.AddImage(completionSpite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);
                uiDart.AddButton("앞/뒤 구분", Front_Back_Button, UIBuilder.PANE_CENTER);
                uiDart.AddButton("골선", Ford_Button, UIBuilder.PANE_CENTER);
                uiDart.AddButton("식서방향", Selvage_Button, UIBuilder.PANE_CENTER);
                uiDart.AddButton("암홀위치", ArmHole_Pos_Button, UIBuilder.PANE_CENTER);
                break;
            case Cloth_State.shirts:
                break;
            case Cloth_State.pants:
                break;
            case Cloth_State.skirt:
                break;
        }
        uiDart.AddButton("다음으로", DartButton, UIBuilder.PANE_CENTER);
        uiDart.Show();
    }

    void Update()
    {
        if (Data.MS == Making_State.dart_pos && Data.isCheck == true)
        {
            Data.isCheck = false;
            Show_Confirm();
        }
    }
}