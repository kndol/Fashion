using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Bundle_Cut_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform sweingPos = null;
    [SerializeField]
    Sprite[] bundleSpite = null;  //마름질 이미지
    [SerializeField]
    Sprite[] cutSpite = null;  //재단 이미지

    LaserPointer lp;
    UIBuilder uiBundle;
    UIBuilder uiCutting;

    void Start()
    {
        if (Data.MS == Making_State.start)
        {
            //uiBundle = Instantiate<UIBuilder>(uiCanvasPrefab);

            //uiBundle.SetPaneWidth(900);
            //uiBundle.AddLabel("마름질");
            //uiBundle.AddDivider();

            //uiCutting = Instantiate<UIBuilder>(uiCanvasPrefab);

            //uiCutting.SetPaneWidth(900);
            //uiCutting.AddLabel("재단");
            //uiCutting.AddDivider();
        }
    }

    public void BundleButton()
    {
        uiBundle.Hide();
        Data.MS = Making_State.cutting;
        Show_Confirm();
    }

    public void CutButton()
    {
        player.transform.position = sweingPos.transform.position;
        player.transform.rotation = sweingPos.transform.rotation;
        uiCutting.Hide();
        Data.MS = Making_State.sweing_sheet;
        Data.isCheck = true;
    }

    public void Show_Confirm()
    {
        switch (Data.MS)
        {
            case Making_State.bundle:
                switch (Data.CS)
                {
                    case Cloth_State.t_shirts:
                        //uiBundle.AddImage(bundleSpite[0], new Rect(0, 0, 450, 350));
                        break;
                    case Cloth_State.shirts:
                        break;
                    case Cloth_State.pants:
                        break;
                    case Cloth_State.skirt:
                        break;
                }
                //uiBundle.Show();
                Data.isCheck = false;
                break;

            case Making_State.cutting:
                switch (Data.CS)
                {
                    case Cloth_State.t_shirts:
                        //uiCutting.AddImage(cutSpite[0], new Rect(0, 0, 450, 350));
                        break;
                    case Cloth_State.shirts:
                        break;
                    case Cloth_State.pants:
                        break;
                    case Cloth_State.skirt:
                        break;
                }
                //uiCutting.Show();
                break;
        }
    }

    void Update()
    {
        if (Data.isCheck == true)
        {
            //Show_Confirm();
        }
    }
}
