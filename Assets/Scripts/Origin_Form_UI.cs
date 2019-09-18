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
    
    

    void Update()
    {
        if (Data.MS == Making_State.original_form && Data.isCheck == true)
        {

            Data.isCheck = false;
        }
    }
}

/*
    void Input_Value()
    {
        uiOrigin.AddLabel("");
        uiOrigin.AddLabel("");
        uiOrigin.AddLabel("");
    }

    void Tshirts_Calculate()
    {
        float A = 0;  
        float B = 0;  //입력값
        float C = 0;
        float D = 0;
        float E = 0;

        A = B / 4;
        C = B / 4 + 4;
        D = C - 1;
        E = B / 2 + 3;
    }
*/
