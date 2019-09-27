using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Dart_UI : FashionController
{ 
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
        uiDart = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiDart.AddLabel("패턴완성", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        uiDart.AddLabel("Description", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
        uiDart.AddDivider(UIBuilder.PANE_LEFT);
        uiDart.AddDivider(UIBuilder.PANE_RIGHT);
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                uiDart.AddImage(completionSpite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);
                uiDart.AddButton("앞/뒤 구분", FrontBackButton);
                uiDart.AddButton("골선", FordButton);
                uiDart.AddButton("식서방향", SelvageButton);
                uiDart.AddButton("암홀위치", ArmHolePosButton);
                break;
            case ClothType.shirts:
                break;
            case ClothType.pants:
                break;
            case ClothType.skirt:
                break;
        }
        uiDart.AddButton("다음으로", DartButton);
        uiDart.Show();
    }

    public void DartButton()
    {
        OnTutorialEnd();
    }

    public void FrontBackButton()   //앞뒤구분 설명 버튼  //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("앞/뒤 구분 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);   //적어
            isCheck = false;
        }
        else
        {
            DescriptionBack();
            isCheck = true;
        }
    }

    public void FordButton()         //골선 설명 버튼      //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("골선 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);    //적어
            isCheck = false;
        }
        else
        {
            DescriptionBack();
            isCheck = true;
        }
    }

    public void SelvageButton()      //식서 설명 버튼      //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("식서 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);  //적어
            isCheck = false;
        }
        else
        {
            DescriptionBack();
            isCheck = true;
        }
    }

    public void ArmHolePosButton()  //암홀위치 설명 버튼  //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("암홀위치 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);   //적어
            isCheck = false;
        }
        else
        {
            DescriptionBack();
            isCheck = true;
        }
    }

    public void DescriptionBack()
    {
        Destroy(uiDart.gameObject);
        uiDart = Instantiate<UIBuilder>(uiCanvasPrefab);
    }

    public override void OnTutorialEnd()
    {
        base.OnTutorialEnd();
    }
}