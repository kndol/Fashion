using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Sweing_UI : FashionController
{
    [SerializeField]
    Sprite[] s_sheetSpite = null;  //낱장재봉 이미지
    [SerializeField]
    Sprite[] s_fabSpite = null;  //합봉 이미지

    UIBuilder uiSweing_sheet;
    UIBuilder uiSweing_fabrication;

	void StartTutorial()
	{
        // 일단 base.StartTutorial() 호출한 뒤에 작업 시작
        uiSweing_sheet = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiSweing_fabrication = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiSweing_sheet.SetPaneWidth(900);
        uiSweing_sheet.AddLabel("낱장 재봉");
        uiSweing_sheet.AddDivider();
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                //uiSweing_sheet.AddImage(s_sheetSpite[0], new Rect(0, 0, 450, 350));
                break;
            case ClothType.shirts:
                break;
            case ClothType.pants:
                break;
            case ClothType.skirt:
                break;
        }
        uiSweing_sheet.AddButton("확인", Sweing_Sheet_Button);
        uiSweing_sheet.Show();
    }

	public void Sweing_Sheet_Button()
    {
        uiSweing_sheet.Hide();
        uiSweing_fabrication.SetPaneWidth(900);
        uiSweing_fabrication.AddLabel("합봉");
        uiSweing_fabrication.AddDivider();
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                //uiSweing_fabrication.AddImage(s_fabSpite[0], new Rect(0, 0, 450, 350));
                break;
            case ClothType.shirts:
                break;
            case ClothType.pants:
                break;
            case ClothType.skirt:
                break;
        }
        uiSweing_fabrication.AddButton("확인", Sweing_Fab_Button);
        uiSweing_fabrication.Show();
    }

    public void Sweing_Fab_Button()
    {
        uiSweing_fabrication.Hide();
        OnTutorialEnd();
    }

    public override void OnTutorialEnd()
    {
        base.OnTutorialEnd();
    }
}
