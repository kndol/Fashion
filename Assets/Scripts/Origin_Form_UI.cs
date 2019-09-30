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
        uiOrigin.AddLabel("Description", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
        uiOrigin.AddDivider(UIBuilder.PANE_RIGHT);
        uiOrigin.AddLabel("도면", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        uiOrigin.AddDivider(UIBuilder.PANE_LEFT);
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
                uiOrigin.AddButton("길원형", BodyoriginPattern, UIBuilder.PANE_CENTER);
                uiOrigin.AddButton("무다트", MudartPattern, UIBuilder.PANE_CENTER);
                uiOrigin.AddButton("반다트", BandartPattern, UIBuilder.PANE_CENTER);
                uiOrigin.AddButton("반무다트", BanmudartPattern, UIBuilder.PANE_CENTER);
                break;
            case ClothType.sleeve:
                uiOrigin.AddLabel("소매도면");
                uiOrigin.AddDivider();
                uiOrigin.AddImage(SleeveSprite[1], new Rect(0, 0, 450, 300));
                break;
        }
        uiOrigin.AddButton("다음으로", OriginButton, UIBuilder.PANE_CENTER);
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
            for(int i =0;i < 4; i++)
            {
                uiOrigin.AddImage(bodyoriginSprite[i], new Rect(0, 0, 120, 50), UIBuilder.PANE_LEFT);
            }
            uiOrigin.AddScrollView("(1) 뒤판" +
                "① 뒷목둘레: A에서 뒤중심에 직각을 유지하며 곡선을 그린다.\n" +
                "② 어깨경사: 목옆점(N)에서 수평으로 18cm, 수직으로 6cm 내린 점을 N과 연결하여 어깨경사를 그린다.\n" +
                "③ 뒤어깨다트: 목옆점(N)에서 5cm 나간 점과 뒤품선(A₁-C₁)의 1 / 2점에서 1cm 중심쪽으로 이동한 점과 연결한다.\n" +
                "다트길이는 8cm, 다트폭은 1cm로 한다.뒤어깨다트폭 1cm를 어깨점에서 나가 암홀선을 다시 그린다.\n" +
                "④ 맞춤표시(Bn) : 진동깊이의 1 / 4위치를 뒤소매둘레선에 가져와서 맞춤표시를 넣는다.\n\n" +
                "(2) 앞판" +
                "① 앞네크라인 그리기 : (A'N' = N'₂N'₁)하여 N'₂-N'₁을 3등분하여 1/3선과 N'를 연결한 안내선을 기본으로 하여 앞네크라인을 그린다.\n" +
                "② 어깨점 : N'S' = NS\n" +
                "③ 맞춤표시(Fn) : 앞몸판과 앞소매쪽의 진동둘레를 일치시키는 점. 앞품선에서 3cm 위치의 진동둘레선\n" +
                "④ 앞처짐 : B'-B₂=B'' - D₂= 앞길이와 등길이의 차이(3cm)\n" +
                "⑤ A'-C'₁ : 유장(24cm),C'₁-B.P : 유폭/2(9cm) \n" +
                "⑥ C''₁-C''₂ : 다트의 앞 처짐은 BP점에서 시작하며 분량은 3cm 이며 MP 라고 한다.", TextAnchor.MiddleCenter, 300, UIBuilder.PANE_RIGHT);
            isCheck = false;
        }
        else
        {
            Next_Button();
            isCheck = true;
        }
    }

    public void MudartPattern()
    {
        if (isCheck)
        {
            uiOrigin.AddImage(mudartSprite, new Rect(0, 0, 300, 150), UIBuilder.PANE_LEFT);
            uiOrigin.AddScrollView("(1) 뒤판" +
                "① 뒷목너비(B / 12) 에서 뒷못둘레는 뒤중심에서 직각을 유지하며 곡선을 그린다.\n" +
                "② 어깨경사는 목옆점(N)에서 수평으로 18cm, 수직으로 6cm 내린 점을 N과 연결하여 어깨경사로 하고 어깨끝점은 품선에서 1.5cm나간 점이다.\n" +
                "③ 암홀선그리기, Bn(맞춤표시)는 뒷몸판과 뒷소매쪽의 진동둘레를 일치시키는 점으로 뒤품선에서 5cm 위치의 진동둘레선\n\n" +
                "(2) 앞판" +
                "① 옆목점에서 3cm  올린 곳에서  B/12를 내려간곳이 앞목깊이, 앞목너비는 B/12-0.5cm\n" +
                "② 앞어깨경사 : N'에서 수평으로 18cm, 수직으로 6cm 점을 잡아서 N'와 연결하여 어깨경사를 그린다.\n" +
                "③ 앞 어깨점과 뒤 어깨점의 간격은 동일\n" +
                "④ 암홀선 그리기, Fn(맞춤표시)는 앞몸판과 앞소매쪽의 진동둘레를 일치시키는 점으로 앞품선에서 3cm 위치의 진동둘레선\n" +
                "⑤ 앞 뒤의 옆선길이를 맞춘다.\n", TextAnchor.MiddleCenter, 300, UIBuilder.PANE_RIGHT);
            isCheck = false;
        }
        else
        {
            Next_Button();
            isCheck = true;
        }
    }

    public void BandartPattern()
    {
        if (isCheck)
        {
            uiOrigin.AddImage(mudartSprite, new Rect(0, 0, 300, 150), UIBuilder.PANE_LEFT);
            uiOrigin.AddScrollView("(1) 뒤판" +
                "① 어깨경사는 목옆점(N)에서 수평으로 18cm, 수직으로 6cm 내린 점을 N과 연결하여 어깨경사로 하고 어깨끝점은 품선에서 1.5cm나간 점이다.\n" +
                "② 암홀선그리기, Bn(맞춤표시)는 뒷몸판과 뒷소매쪽의 진동둘레를 일치시키는 점으로 뒤품선에서 5cm 위치의 진동둘레선\n\n" +
                "(2) 앞판" +
                "① 옆목점에서 1.5cm  올린 곳에서  B/12를 내려간곳이 앞목깊이, 앞목너비는 B/12-0.5cm\n" +
                "② 암홀선 그리기, Fn(맞춤표시)는 앞몸판과 앞소매쪽의 진동둘레를 일치시키는 점으로 앞품선에서 3cm 위치의 진동둘레선\n" +
                "③ 유장은 24cm 를 내려오며 유폭/2 지점을 BP점이라고 한다.\n" +
                "④ 앞 처짐은 BP점에서 시작하며 분량은 1.5cm 이며 MP라고 한다. \n" +
                "⑤ 앞 뒤의 옆선길이를 맞춘다.\n" +
                "Fn : 앞몸판과 앞소매쪽의 진동둘레를 일치시키는 점\n" +
                "Bn : 뒤몸판과 뒤소매쪽의 진동둘레를 일치시키는 점", TextAnchor.MiddleCenter, 300, UIBuilder.PANE_RIGHT);
            isCheck = false;
        }
        else
        {
            Next_Button();
            isCheck = true;
        }
    }

    public void BanmudartPattern()
    {
        if (isCheck)
        {
            uiOrigin.AddImage(mudartSprite, new Rect(0, 0, 300, 150), UIBuilder.PANE_LEFT);
            uiOrigin.AddScrollView("(1) 뒤판" +
                "① 뒷목너비( B/12) 에서 뒷못둘레는 뒤중심에서 직각을 유지하며 곡선으로 그린다.\n" +
                "② 어깨경사는 목옆점(N)에서 수평으로 18cm, 수직으로 6cm 내린 점을 N과 연결하여 어깨경사로 하고 어깨끝점은 품선에서 1.5cm나간 점이다.\n" +
                "③ 암홀선그리기, Bn(맞춤표시)는 뒷몸판과 뒷소매쪽의 진동둘레를 일치시키는 점으로 뒤품선에서 5cm 위치의 진동둘레선\n\n" +
                "(2) 앞판" +
                "① 앞목깊이( B/12)에서 앞목너비는 B/12-0.5cm\n" +
                "② 앞어깨경사 : N'에서 수평으로 18cm, 수직으로 6cm 점을 잡아서 N'와 연결하여 어깨경사로 한다.\n" +
                "③ 앞 어깨점과 뒤 어깨점의 간격은 동일하다.\n" +
                "④ 암홀선 그리기, Fn(맞춤표시)는 앞몸판과 앞소매쪽의 진동둘레를 일치시키는 점으로 앞품선에서 3cm 위치의 진동둘레선\n" +
                "⑤ 앞 처짐은 BP점에서 시작하며 분량은 1.5cm 이며 MP 라고 한다.\n" +
                "⑥ 앞 뒤의 옆선길이를 맞춘다.\n" +
                "Fn : 앞몸판과 앞소매쪽의 진동둘레를 일치시키는 점\n" +
                "Bn : 뒤몸판과 뒤소매쪽의 진동둘레를 일치시키는 점", TextAnchor.MiddleCenter, 300, UIBuilder.PANE_RIGHT);
            isCheck = false;
        }
        else
        {
            Next_Button();
            isCheck = true;
        }
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