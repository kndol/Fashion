using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Origin_Form_UI : FashionController
{
    [SerializeField]
    string designselectSceneName = null;
    [SerializeField]
    string dartSceneName = null;
    [SerializeField]
    Sprite []bodyoriginSprite = null;
    [SerializeField]
    Sprite mudartSprite = null;
    [SerializeField]
    Sprite bandartSprite = null;
    [SerializeField]
    Sprite banmudartSprite = null;
    [SerializeField]
    Sprite []SleeveSprite = null;
    [SerializeField]
    Sprite []tshirtsSpite = null;

    UIBuilder uiOrigin;

    Text inputText;
    Text firstValue;
    Text secondValue;
    Text thirdValue;
    Text forthValue;

    bool isCheck = true;

	void Start()
    {
        uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);

        StartTutorial();
    }

    public void StartTutorial()
    {
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                uiOrigin.AddLabel("티셔츠 기초선", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
                uiOrigin.AddDivider(UIBuilder.PANE_LEFT);
                uiOrigin.AddImage(tshirtsSpite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);  //기초선
                uiOrigin.AddLabel("치수입력", TextAnchor.MiddleCenter);
                uiOrigin.AddDivider();
                uiOrigin.AddInputNumberField(0, "치수를 입력하세요.", Bust_Size_Input, null);
                var size_tshirts_B_Text = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                inputText = size_tshirts_B_Text.GetComponentInChildren<Text>();
                var calculate_tshirts_B_1 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                firstValue = calculate_tshirts_B_1.GetComponentInChildren<Text>();
                var calculate_tshirts_B_2 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                secondValue = calculate_tshirts_B_2.GetComponentInChildren<Text>();
                var calculate_tshirts_B_3 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                thirdValue = calculate_tshirts_B_3.GetComponentInChildren<Text>();
                var calculate_tshirts_B_4 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                forthValue = calculate_tshirts_B_4.GetComponentInChildren<Text>();
                break;
            case ClothType.shirts:
                break;
            case ClothType.pants:
                break;
            case ClothType.skirt:
                break;
            case ClothType.body:
                uiOrigin.AddLabel("몸판 기초선", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
                uiOrigin.AddLabel("치수 입력", TextAnchor.MiddleCenter);
                uiOrigin.AddDivider(UIBuilder.PANE_LEFT);
                uiOrigin.AddDivider();
                uiOrigin.AddImage(tshirtsSpite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);
                uiOrigin.AddInputNumberField(0, "B를 입력하세요.", Bust_Size_Input, null);
                var size_B_Text = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                inputText = size_B_Text.GetComponentInChildren<Text>();
                var calculate_B_1 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                firstValue = calculate_B_1.GetComponentInChildren<Text>();
                var calculate_B_2 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                secondValue = calculate_B_2.GetComponentInChildren<Text>();
                var calculate_B_3 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                thirdValue = calculate_B_3.GetComponentInChildren<Text>();
                var calculate_B_4 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                forthValue = calculate_B_4.GetComponentInChildren<Text>();
                break;
            case ClothType.sleeve:
                uiOrigin.AddLabel("소매 기초선", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
                uiOrigin.AddDivider(UIBuilder.PANE_LEFT);
                uiOrigin.AddImage(SleeveSprite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);
                uiOrigin.AddInputNumberField(0, "소매치수를 입력하세요.", Sleeve_Size_Input, null);
                var size_S_Text = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                inputText = size_S_Text.GetComponentInChildren<Text>();
                var calculate_S_1 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                firstValue = calculate_S_1.GetComponentInChildren<Text>();
                var calculate_S_2 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                secondValue = calculate_S_2.GetComponentInChildren<Text>();
                break;
        }
        uiOrigin.AddButton("다음으로", Next_Button);
        uiOrigin.Show();
    }

    public void Next_Button()
    {
        Destroy(uiOrigin.gameObject);
        uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                uiOrigin.AddLabel("패턴제도");
                uiOrigin.AddDivider();
                uiOrigin.AddImage(tshirtsSpite[1], new Rect(0, 0, 350, 350));
                uiOrigin.AddButton("다음으로", OriginButton);
                break;
            case ClothType.body:
                uiOrigin.AddLabel("Description", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
                uiOrigin.AddDivider(UIBuilder.PANE_RIGHT);
                uiOrigin.AddButton("길원형", BodyoriginPattern);
                uiOrigin.AddButton("무다트", MudartPattern);
                uiOrigin.AddButton("반다트", BandartPattern);
                uiOrigin.AddButton("반무다트", BanmudartPattern);
                uiOrigin.AddButton("소매만들기", SleeveButton);
                break;
            case ClothType.sleeve:  //가로 배치 및 폭수정
                uiOrigin.AddLabel("Description");
                uiOrigin.AddDivider();
                uiOrigin.AddImage(SleeveSprite[1], new Rect(0, 0, 450, 350));
                uiOrigin.AddLabel("티셔츠 소매 설명");
                uiOrigin.AddImage(SleeveSprite[2], new Rect(0, 0, 450, 350));
                uiOrigin.AddLabel("블라우스 소매 설명");
                uiOrigin.AddButton("확인", OriginButton);
                break;
        }
        uiOrigin.Show();
    }

    public void OriginButton()
    {
        nextSceneName = Data.clothType == ClothType.t_shirts ? dartSceneName : designselectSceneName;
        OnTutorialEnd();
    }

    public void SleeveButton()  //소매 만들기 버튼
    {
        Data.clothType = ClothType.sleeve;
        StartTutorial();
    }

    public void BodyoriginPattern()
    {
        if (isCheck)
        { 
            for (int i = 0; i < 4; i++)
            {
                uiOrigin.AddImage(bodyoriginSprite[i], new Rect(0, 0, 300, 150), UIBuilder.PANE_RIGHT);
            }
            uiOrigin.AddLabel("길원형 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
            isCheck = false;
        }
        else
        {
            Description_Back();
            isCheck = true;
        }
    }

    public void MudartPattern()
    {
        if (isCheck)
        {
            uiOrigin.AddImage(mudartSprite, new Rect(0, 0, 300, 150), UIBuilder.PANE_RIGHT);
            uiOrigin.AddLabel("무다트 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
            isCheck = false;
        }
        else
        {
            Description_Back();
            isCheck = true;
        }
    }

    public void BandartPattern()
    {
        if (isCheck)
        {
            uiOrigin.AddImage(bandartSprite, new Rect(0, 0, 300, 150), UIBuilder.PANE_RIGHT);
            uiOrigin.AddLabel("반다트 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
            isCheck = false;
        }
        else
        {
            Description_Back();
            isCheck = true;
        }
    }

    public void BanmudartPattern()
    {
        if (isCheck)
        {
            uiOrigin.AddImage(banmudartSprite, new Rect(0, 0, 300, 150), UIBuilder.PANE_RIGHT);
            uiOrigin.AddLabel("반무다트 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
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
        Destroy(uiOrigin.gameObject);
        uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);
        Next_Button();
    }

    public void Bust_Size_Input(int i)    //몸판치수 계산
    {
        inputText.text = string.Format("B = {0}", i);
        firstValue.text = string.Format("B / 4 = {0}", i / 4);
        secondValue.text = string.Format("(B / 2) + 3 = {0}", (i / 2) + 3);
        thirdValue.text = string.Format("(B / 6) + 4 = {0}", (i / 6) + 4);
        forthValue.text = string.Format("(B / 6) + 4 - 1 = {0}", (i / 6) + 4 - 1);
    }

    public void Sleeve_Size_Input(int i)   //소매치수 계산
    {
        inputText.text = string.Format("B = {0}", i);
        firstValue.text = string.Format("(AH / 2) - 0.5 = {0}", (i / 2) - 0.5);
        secondValue.text = string.Format("(AH / 4) + 3 = {0}",  (i / 4) + 3);
    }

    public override void OnTutorialEnd()
    {
        base.OnTutorialEnd();
    }
}