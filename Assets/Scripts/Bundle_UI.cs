using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion.UIManager;

public class Bundle_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    //[SerializeField]
    //Transform bundlePos = null;

    UIBuilder uiBundle;

    void Start()
    {
        uiBundle = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiBundle.AddLabel("마름질");
        uiBundle.AddDivider();
        uiBundle.AddButton("확인", BundleButton);
    }

    public void BundleButton()
    {
        //player.transform.position = bundlePos.transform.position;
        //player.transform.rotation = bundlePos.transform.rotation;
        uiBundle.Hide();
        StartUI.instance.MS = Making_State.seam;
        Data.isCheck = true;
    }

    void Update()
    {
        if (StartUI.instance.MS == Making_State.bundle && Data.isCheck == true)
        {
            uiBundle.Show();
            Data.isCheck = false;
        }
    }
}
