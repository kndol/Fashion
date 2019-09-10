using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion.UIManager;

public class Arrangement_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform bundlePos = null;

    UIBuilder uiArrangement;

    void Start()
    {
        uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiArrangement.AddLabel("배치");
        uiArrangement.AddDivider();
        uiArrangement.AddButton("확인", ArrangeButton);
    }

    public void ArrangeButton()
    {
        player.transform.position = bundlePos.transform.position;
        player.transform.rotation = bundlePos.transform.rotation;
        uiArrangement.Hide();
        StartUI.instance.MS = Making_State.bundle;
        Data.isCheck = true;
    }

    void Update()
    {
        if (StartUI.instance.MS == Making_State.arrangement && Data.isCheck == true)
        {
            uiArrangement.Show();
            Data.isCheck = false;
        }
    }
}
