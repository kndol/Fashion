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
    [SerializeField]
    Sprite[] skirtSpite = null;

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
        uiOrigin.SetPosition(menuPosition);
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                uiOrigin.AddLabel("티셔츠 기초선", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
                uiOrigin.AddDivider(UIBuilder.PANE_LEFT);
                uiOrigin.AddImage(tshirtsSpite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);  //기초선
                uiOrigin.AddLabel("치수입력", TextAnchor.MiddleCenter);
                uiOrigin.AddDivider();
                uiOrigin.AddInputNumberField(0, "치수를 입력하세요.", SizeCalculate, null);
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
            case ClothType.skirt:
                uiOrigin.AddLabel("치마 기초선", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
                uiOrigin.AddDivider(UIBuilder.PANE_LEFT);
                uiOrigin.AddImage(skirtSpite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);  //기초선
                uiOrigin.AddLabel("치수입력", TextAnchor.MiddleCenter);
                uiOrigin.AddDivider();
                uiOrigin.AddInputNumberField(0, "치수를 입력하세요.", SizeCalculate, null);
                var size_skirt_Text = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                inputText = size_skirt_Text.GetComponentInChildren<Text>();
                var calculate_skirt_1 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                firstValue = calculate_skirt_1.GetComponentInChildren<Text>();
                var calculate_skirt_2 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                secondValue = calculate_skirt_2.GetComponentInChildren<Text>();
                var calculate_skirt_3 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                thirdValue = calculate_skirt_3.GetComponentInChildren<Text>();
                var calculate_skirt_4 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter);
                forthValue = calculate_skirt_4.GetComponentInChildren<Text>();
                break;
            case ClothType.body:
                uiOrigin.AddLabel("몸판 기초선", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
                uiOrigin.AddDivider(UIBuilder.PANE_LEFT);
                uiOrigin.AddImage(tshirtsSpite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);
                uiOrigin.AddLabel("치수 입력", TextAnchor.MiddleCenter);
                uiOrigin.AddDivider();
                uiOrigin.AddInputNumberField(0, "치수를 입력하세요.", SizeCalculate, null);
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
                uiOrigin.AddLabel("치수 입력", TextAnchor.MiddleCenter);
                uiOrigin.AddDivider();
                uiOrigin.AddInputNumberField(0, "소매치수를 입력하세요.", SizeCalculate, null);
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
        uiOrigin.SetPosition(menuPosition);
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                uiOrigin.AddLabel("패턴제도");
                uiOrigin.AddDivider();
                uiOrigin.AddImage(tshirtsSpite[1], new Rect(0, 0, 350, 300));
                break;
            case ClothType.skirt:
                uiOrigin.AddLabel("패턴제도");
                uiOrigin.AddDivider();
                uiOrigin.AddImage(skirtSpite[1], new Rect(0, 0, 350, 300));
                break;
            case ClothType.body:
                uiOrigin.AddLabel("Description", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
                uiOrigin.AddDivider(UIBuilder.PANE_RIGHT);
                uiOrigin.AddButton("길원형", BodyoriginPattern);
                uiOrigin.AddButton("무다트", MudartPattern);
                uiOrigin.AddButton("반다트", BandartPattern);
                uiOrigin.AddButton("반무다트", BanmudartPattern);
                break;
            case ClothType.sleeve:  //가로 배치 및 폭수정
                uiOrigin.AddLabel("Description");
                uiOrigin.AddDivider();
                uiOrigin.AddImage(SleeveSprite[1], new Rect(0, 0, 450, 300));
                uiOrigin.AddLabel("티셔츠 소매 설명");
                break;
        }
        uiOrigin.AddButton("다음으로", OriginButton);
        uiOrigin.Show();
    }

    public void OriginButton()
    {
        nextSceneName = (Data.clothType == ClothType.t_shirts) || (Data.clothType == ClothType.skirt) ? dartSceneName : designselectSceneName;
        OnTutorialEnd();
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
        StartTutorial();
    }

    public void SizeCalculate(int i)    //몸판치수 계산
    {
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                inputText.text = string.Format("가슴둘레(B) = {0}", i);
                firstValue.text = string.Format("진동깊이(AC) = B / 4 = {0}", i / 4);
                secondValue.text = string.Format("가슴둘레(C-C') = (B / 2) + 3 = {0}", (i / 2) + 3);
                thirdValue.text = string.Format("뒤품선(A1-C1) = (B / 6) + 4 = {0}", (i / 6) + 4);
                forthValue.text = string.Format("앞품선(A'1-C'1) =  뒤품선(A1-C1) - 1 = {0}", (i / 6) + 3);
                break;
            case ClothType.skirt:
                inputText.text = string.Format("엉덩이둘레(H) = {0}", i);
                firstValue.text = string.Format("스커트폭(AA') = (H / 2) + 2 = {0}", (i / 2) + 2);
                secondValue.text = string.Format("스커트길이(AB) = 60");
                thirdValue.text = string.Format("엉덩이길이(AC) = 18");
                break;
            case ClothType.body:
                inputText.text = string.Format("가슴둘레(B) = {0}", i);
                firstValue.text = string.Format("진동깊이(AC) = B / 4 = {0}", i / 4);
                secondValue.text = string.Format("가슴둘레(C-C') = (B / 2) + 3 = {0}", (i / 2) + 3);
                thirdValue.text = string.Format("뒤품선(A1-C1) = (B / 6) + 4 = {0}", (i / 6) + 4);
                forthValue.text = string.Format("앞품선(A'1-C'1) =  뒤품선(A1-C1) - 1 = {0}", (i / 6) + 3);
                break;
            case ClothType.sleeve:
                inputText.text = string.Format("소매길이(AC) = {0}", i);
                firstValue.text = string.Format("(AH / 2) - 0.5 = {0}", (i / 2) - 0.5);
                secondValue.text = string.Format("(AH / 4) + 3 = {0}", (i / 4) + 3);
                thirdValue.text = string.Format("(AH / 2) - 0.5 = {0}", (i / 2) - 0.5);
                break;
        }
    }

    public override void OnTutorialEnd()
    {
        base.OnTutorialEnd();
    }
}