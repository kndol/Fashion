using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion.UIManager;

public class Origin_Form_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform dartPos = null;

    UIBuilder uiOrigin;

    void Start()
    {
        uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiOrigin.AddLabel("패턴 제도 원형");
        uiOrigin.AddDivider();
        uiOrigin.AddButton("확인", OriginButton);
    }

    public void OriginButton()
    {
        player.transform.position = dartPos.transform.position;
        player.transform.rotation = dartPos.transform.rotation;
        uiOrigin.Hide();
        StartUI.instance.MS = Making_State.dart_pos;
        Data.isCheck = true;
    }

    void Update()
    {
        if (StartUI.instance.MS == Making_State.original_form && Data.isCheck == true)
        {
            uiOrigin.Show();
            Data.isCheck = false;
        }
    }
}
