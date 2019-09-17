using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Sweing_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform wearingPos = null;
    [SerializeField]
    Sprite[] s_sheetSpite = null;  //낱장재봉 이미지
    [SerializeField]
    Sprite[] s_fabSpite = null;  //합봉 이미지

    UIBuilder uiSweing_sheet;
    UIBuilder uiSweing_fabrication;

    void Start()
    {
        if (Data.MS == Making_State.start)
        {
            uiSweing_sheet = Instantiate<UIBuilder>(uiCanvasPrefab);

            uiSweing_sheet.SetPaneWidth(900);
            uiSweing_sheet.AddLabel("낱장 재봉");
            uiSweing_sheet.AddDivider();

            uiSweing_fabrication = Instantiate<UIBuilder>(uiCanvasPrefab);

            uiSweing_fabrication.SetPaneWidth(900);
            uiSweing_fabrication.AddLabel("합봉");
            uiSweing_fabrication.AddDivider();
        }
    }

    public void Sweing_Sheet_Button()
    {
        uiSweing_sheet.Hide();
        Data.MS = Making_State.sweing_fabrication;
        Data.isCheck = true;
    }

    public void Sweing_Fab_Button()
    {
        player.transform.position = wearingPos.transform.position;
        player.transform.rotation = wearingPos.transform.rotation;
        uiSweing_fabrication.Hide();
        Data.MS = Making_State.wearing;
        Data.isCheck = true;
    }

    void Update()
    {
        if (Data.isCheck == true)
        {
            switch (Data.MS)
            {
                case Making_State.sweing_sheet:
                    switch (Data.CS)
                    {
                        case Cloth_State.t_shirts:
                            uiSweing_sheet.AddImage(s_sheetSpite[0], new Rect(0, 0, 450, 350));
                            break;
                        case Cloth_State.shirts:
                            break;
                        case Cloth_State.pants:
                            break;
                        case Cloth_State.skirt:
                            break;
                    }
                    uiSweing_sheet.AddButton("확인", Sweing_Sheet_Button);
                    uiSweing_sheet.Show();
                    Data.isCheck = false;
                    break;

                case Making_State.sweing_fabrication:
                    switch (Data.CS)
                    {
                        case Cloth_State.t_shirts:
                            uiSweing_fabrication.AddImage(s_fabSpite[0], new Rect(0, 0, 450, 350));
                            break;
                        case Cloth_State.shirts:
                            break;
                        case Cloth_State.pants:
                            break;
                        case Cloth_State.skirt:
                            break;
                    }
                    uiSweing_fabrication.AddButton("확인", Sweing_Fab_Button);
                    uiSweing_fabrication.Show();
                    Data.isCheck = false;
                    break;
            }
        }    
    }
}
