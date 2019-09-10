using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion.UIManager;

public class Dart_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform arrangePos = null;

    UIBuilder uiDart;

    void Start()
    {
        uiDart = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiDart.AddLabel("위치지정");
        uiDart.AddDivider();
        uiDart.AddButton("확인", DartButton);
    }

    public void DartButton()
    {
        player.transform.position = arrangePos.transform.position;
        player.transform.rotation = arrangePos.transform.rotation;
        uiDart.Hide();
        StartUI.instance.MS = Making_State.arrangement;
        Data.isCheck = true;
    }

    void Update()
    {
        if (StartUI.instance.MS == Making_State.dart_pos && Data.isCheck == true)
        {
            uiDart.Show();
            Data.isCheck = false;
        }
    }
}
