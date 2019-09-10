using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion.UIManager;

public class Design_Select_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform originPos = null;

    UIBuilder uiSelect;

    void Start()
    {
        uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiSelect.AddLabel("디자인을 선택하세요.");
        uiSelect.AddDivider();
        uiSelect.AddButton("확인", SelectButton);
    }

    public void SelectButton()
    {
        player.transform.position = originPos.transform.position;
        player.transform.rotation = originPos.transform.rotation;
        uiSelect.Hide();
        StartUI.instance.MS = Making_State.original_form;
        Data.isCheck = true;
    }

    void Update()
    {
        if (StartUI.instance.MS == Making_State.Design_Select && Data.isCheck == true)
        {
            uiSelect.Show();
            Data.isCheck = false;
        }
    }
}
