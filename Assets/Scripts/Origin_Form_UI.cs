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
    Transform dartPos = null;
    [SerializeField]
    Sprite[] originSpite = null;  //원형 이미지 0 : 티셔츠, 1 : 셔츠, 2 : 바지, 3 : 치마

    UIBuilder uiOrigin;

    void Start()
    {
        if (Data.MS == Making_State.start)
        {
            uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);

            uiOrigin.SetPaneWidth(900);
            uiOrigin.AddLabel("패턴 제도 원형");
            uiOrigin.AddDivider();
        }
    }

    public void OriginButton()
    {
        player.transform.position = dartPos.transform.position;
        player.transform.rotation = dartPos.transform.rotation;
        uiOrigin.Hide();
        Data.MS = Making_State.dart_pos;
        Data.isCheck = true;
    }

    void Input_Value()
    {

    }
    
    void Update()
    {
        if (Data.MS == Making_State.original_form && Data.isCheck == true)
        {
            switch (Data.CS)
            {
                case Cloth_State.t_shirts:
                    uiOrigin.AddImage(originSpite[0], new Rect(0, 0, 400, 250));
                    break;
                case Cloth_State.shirts:
                    break;
                case Cloth_State.pants:
                    break;
                case Cloth_State.skirt:
                    break;
            }
            uiOrigin.AddButton("확인", OriginButton);
            uiOrigin.Show();
            Data.isCheck = false;
        }
    }
}
