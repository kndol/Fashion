using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Wearing_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;


    UIBuilder uiWearing;

    void Start()
    {
        uiWearing = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiWearing.AddLabel("착장");
        uiWearing.AddDivider();
        uiWearing.AddButton("확인", WearingButton);
    }

    public void WearingButton()
    {
        uiWearing.Hide();
        Data.isCheck = true;
    }

    void Update()
    {
        if (Data.MS == Making_State.wearing && Data.isCheck == true)
        {
            uiWearing.Show();
            Data.isCheck = false;
        }
    }
}
