using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Dart_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform arrangePos = null;
    [SerializeField]
    Sprite[] dartSpite = null;  //dart 이미지

    UIBuilder uiDart;

    void Start()
    {
        uiDart = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiDart.AddLabel("위치지정");
        uiDart.AddDivider();
    }

    public void DartButton()
    {
        player.transform.position = arrangePos.transform.position;
        player.transform.rotation = arrangePos.transform.rotation;
        uiDart.Hide();
        Data.MS = Making_State.arrangement;
        Data.isCheck = true;
    }

    void Update()
    {
        if (Data.MS == Making_State.dart_pos && Data.isCheck == true)
        {
            switch (Data.CS)
            {
                case Cloth_State.t_shirts:
                    uiDart.AddImage(dartSpite[0]);
                    break;
                case Cloth_State.shirts:
                    break;
                case Cloth_State.pants:
                    break;
                case Cloth_State.skirt:
                    break;
                case Cloth_State.onepiece:
                    break;
            }
            uiDart.AddButton("확인", DartButton);
            uiDart.Show();
            Data.isCheck = false;
        }
    }
}
