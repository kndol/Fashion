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
    Sprite[] completionSpite = null;  //0:티셔츠  1:치마

    UIBuilder uiDart;

    bool isCheck = true;

    void Start()
    {
        uiDart = Instantiate<UIBuilder>(uiCanvasPrefab);
        StartTutorial();
    }

    public void StartTutorial()
    {
        uiDart.SetPosition(menuPosition);
        uiDart.AddLabel("패턴완성", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        uiDart.AddLabel("Description", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
        uiDart.AddDivider(UIBuilder.PANE_LEFT);
        uiDart.AddDivider(UIBuilder.PANE_RIGHT);
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                uiDart.AddImage(completionSpite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);
                uiDart.AddButton("골선", FordButton);
                uiDart.AddButton("식서방향", SelvageButton);
                uiDart.AddButton("암홀맞춤표시", ArmHolePosButton);
                break;
            case ClothType.skirt:
                uiDart.AddImage(completionSpite[1], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);
                uiDart.AddButton("골선", FordButton);
                uiDart.AddButton("식서방향", SelvageButton);
                break;
        }
        uiDart.AddButton("다음으로", DartButton);
        uiDart.Show();
    }

    public void DartButton()
    {
        OnTutorialEnd();
    }

    public void FordButton()         //골선 설명 버튼      //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("표시된 부분의 선을 중심축으로 하여 본을 바로해서 올려놓고 1장 그리고, 중심축에서 본을 넘기듯 뒤집어서 좌, 우 대칭이 되도록 패턴을 한번 더 그려주는것", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
            isCheck = false;
        }
        else
        {
            BackButton();
            isCheck = true;
        }
    }

    public void SelvageButton()      //식서 설명 버튼      //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("원단 가장자리의 구멍이 뚫려있는 부분. 원단이 제일 늘어나지않는 방향. 식서방향에 맞추는 이유: 옷의 늘어짐과 뒤틀림이 최대한 없게 하기 위해", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);  //적어
            isCheck = false;
        }
        else
        {
            BackButton();
            isCheck = true;
        }
    }

    public void ArmHolePosButton()  //암홀위치 설명 버튼  //설명 추가 예정
    {
        if (isCheck)
        {
            uiDart.AddLabel("맞춤표시 몸판과 소매쪽의 진동둘레를 일치시키는 점으로 앞품선에서 3cm. 뒤품선에서 5cm 위치의 진동둘레선", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);   //적어
            isCheck = false;
        }
        else
        {
            BackButton();
            isCheck = true;
        }
    }

    public void BackButton()
    {
        Destroy(uiDart.gameObject);
        uiDart = Instantiate<UIBuilder>(uiCanvasPrefab);
        StartTutorial();
    }
    public override void OnTutorialEnd()
    {
        base.OnTutorialEnd();
    }
}