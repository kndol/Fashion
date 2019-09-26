using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Origin_Form_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
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
    Sprite []tshirtsSpite = null;         //원형 이미지 0 : 티셔츠, 1 : 셔츠, 2 : 바지, 3 : 치마

    UIBuilder uiOrigin;

    Text inputText;
    Text firstValue;
    Text secondValue;
    Text thirdValue;
    Text forthValue;

    bool isCheck = true;

    void Start()
    {
        if (Data.MS == Making_State.start)
        {
            uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);
        }
    }

    public void OriginButton()
    {
        switch (Data.CS)
        {
            case ClothType.t_shirts:
                uiOrigin.Hide();
                Data.MS = Making_State.dart_pos;
                break;
            case ClothType.Sleeve:
                Data.MS = Making_State.Design_Select;
                Destroy(uiOrigin.gameObject);
                break;
        }
        uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);
        Data.isCheck = true;
    }

    public void Bust_Size_Input(int i)
    {
        inputText.text = string.Format("B = {0}", i);
        firstValue.text = string.Format("B / 4 = {0}", i / 4);
        secondValue.text = string.Format("(B / 2) + 3 = {0}", (i / 2) + 3);
        thirdValue.text = string.Format("(B / 6) + 4 = {0}", (i / 6) + 4);
        forthValue.text = string.Format("(B / 6) + 4 - 1 = {0}", (i / 6) + 4 - 1);
    }

    public void Sleeve_Size_Input(int i)
    {
        inputText.text = string.Format("B = {0}", i);
        
    }

    public void Bodyorigin_Pattern()
    {
        if (isCheck)
        { 
            for (int i = 0; i < 4; i++)
            {
                uiOrigin.AddImage(bodyoriginSprite[i], new Rect(0, 0, 300, 150), UIBuilder.PANE_RIGHT);
            }
            uiOrigin.AddLabel("길원형 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);   //적어줘
            isCheck = false;
        }
        else
        {
            Description_Back();
            isCheck = true;
        }
    }

    public void mudart_Pattern()
    {
        if (isCheck)
        {
            uiOrigin.AddImage(mudartSprite, new Rect(0, 0, 300, 150), UIBuilder.PANE_RIGHT);
            uiOrigin.AddLabel("무다트 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);  //적어줘
            isCheck = false;
        }
        else
        {
            Description_Back();
            isCheck = true;
        }
    }

    public void bandart_Pattern()
    {
        if (isCheck)
        {
            uiOrigin.AddImage(bandartSprite, new Rect(0, 0, 300, 150), UIBuilder.PANE_RIGHT);
            uiOrigin.AddLabel("반다트 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);  //적어
            isCheck = false;
        }
        else
        {
            Description_Back();
            isCheck = true;
        }
    }

    public void banmudart_Pattern()
    {
        if (isCheck)
        {
            uiOrigin.AddImage(banmudartSprite, new Rect(0, 0, 300, 150), UIBuilder.PANE_RIGHT);
            uiOrigin.AddLabel("반무다트 설명", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);  //여기도
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

    public void Sleeve_Button()  //소매 만들기 버튼
    {
        Destroy(uiOrigin.gameObject);
        uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);
        Data.CS = ClothType.Sleeve;
        Show_Confirm();
    }

    public void Next_Button()
    {
        switch (Data.CS)
        {
            case ClothType.t_shirts:
                Destroy(uiOrigin.gameObject);
                uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);
                uiOrigin.AddLabel("패턴제도");
                uiOrigin.AddDivider();
                uiOrigin.AddImage(tshirtsSpite[1], new Rect(0, 0, 350, 350));
                uiOrigin.AddButton("다음으로", OriginButton);
                break;
            case ClothType.Body:
                Destroy(uiOrigin.gameObject);
                uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);
                uiOrigin.AddLabel("Description", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
                uiOrigin.AddDivider(UIBuilder.PANE_RIGHT);
                uiOrigin.AddButton("길원형", Bodyorigin_Pattern, UIBuilder.PANE_CENTER);
                uiOrigin.AddButton("무다트", mudart_Pattern, UIBuilder.PANE_CENTER);
                uiOrigin.AddButton("반다트", bandart_Pattern, UIBuilder.PANE_CENTER);
                uiOrigin.AddButton("반무다트", banmudart_Pattern, UIBuilder.PANE_CENTER);
                uiOrigin.AddButton("소매만들기", Sleeve_Button, UIBuilder.PANE_CENTER);
                break;
            case ClothType.Sleeve:  //가로 배치 및 폭수정
                Destroy(uiOrigin.gameObject);
                uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);
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

    public void Show_Confirm()
    {
        switch (Data.CS)
        {
            case ClothType.t_shirts:
                uiOrigin.AddLabel("티셔츠 기초선", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
                uiOrigin.AddDivider(UIBuilder.PANE_LEFT);
                uiOrigin.AddImage(tshirtsSpite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);  //기초선
                uiOrigin.AddLabel("치수입력", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                uiOrigin.AddDivider(UIBuilder.PANE_CENTER);
                uiOrigin.AddInputNumberField(0, "치수를 입력하세요.", Bust_Size_Input, null, UIBuilder.PANE_CENTER);
                var size_tshirts_B_Text = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                inputText = size_tshirts_B_Text.GetComponentInChildren<Text>();
                var calculate_tshirts_B_1 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                firstValue = calculate_tshirts_B_1.GetComponentInChildren<Text>();
                var calculate_tshirts_B_2 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                secondValue = calculate_tshirts_B_2.GetComponentInChildren<Text>();
                var calculate_tshirts_B_3 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                thirdValue = calculate_tshirts_B_3.GetComponentInChildren<Text>();
                var calculate_tshirts_B_4 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                forthValue = calculate_tshirts_B_4.GetComponentInChildren<Text>();
                uiOrigin.AddButton("다음으로", Next_Button, UIBuilder.PANE_CENTER);
                break;
            case ClothType.shirts:
                break;
            case ClothType.pants:
                break;
            case ClothType.skirt:
                break;
            case ClothType.Body:
                uiOrigin.AddLabel("몸판 기초선", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
                uiOrigin.AddLabel("치수 입력", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                uiOrigin.AddDivider(UIBuilder.PANE_LEFT);
                uiOrigin.AddDivider(UIBuilder.PANE_CENTER);
                uiOrigin.AddImage(tshirtsSpite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);
                uiOrigin.AddInputNumberField(0, "B를 입력하세요.", Bust_Size_Input, null, UIBuilder.PANE_CENTER);
                var size_B_Text = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                inputText = size_B_Text.GetComponentInChildren<Text>();
                var calculate_B_1 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                firstValue = calculate_B_1.GetComponentInChildren<Text>();
                var calculate_B_2 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                secondValue = calculate_B_2.GetComponentInChildren<Text>();
                var calculate_B_3 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                thirdValue = calculate_B_3.GetComponentInChildren<Text>();
                var calculate_B_4 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                forthValue = calculate_B_4.GetComponentInChildren<Text>();
                uiOrigin.AddButton("다음으로", Next_Button, UIBuilder.PANE_CENTER);
                break;
            case ClothType.Sleeve:
                uiOrigin.AddLabel("소매 기초선", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
                uiOrigin.AddDivider(UIBuilder.PANE_LEFT);
                uiOrigin.AddImage(SleeveSprite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);
                uiOrigin.AddInputNumberField(0, "소매치수를 입력하세요.", Sleeve_Size_Input, null, UIBuilder.PANE_CENTER);
                var size_S_Text = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                inputText = size_S_Text.GetComponentInChildren<Text>();
                var calculate_S_1 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                firstValue = calculate_S_1.GetComponentInChildren<Text>();
                var calculate_S_2 = uiOrigin.AddLabel("", TextAnchor.MiddleCenter, UIBuilder.PANE_CENTER);
                secondValue = calculate_S_2.GetComponentInChildren<Text>();
                uiOrigin.AddButton("다음으로", Next_Button, UIBuilder.PANE_CENTER);
                break;
        }
        uiOrigin.Show();
    }
    
    void Update()
    {
        if (Data.MS == Making_State.original_form && Data.isCheck == true)
        {
            Data.isCheck = false;
            Show_Confirm();
            
        }
    }
}