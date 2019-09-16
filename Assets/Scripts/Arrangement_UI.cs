using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Arrangement_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform bundlePos = null;
    [SerializeField]
    Sprite[] arrangeSpite = null;  //배치 이미지

    UIBuilder uiArrangement;

    void Start()
    {
        uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiArrangement.AddLabel("배치");
        uiArrangement.AddDivider();
    }

    public void ArrangeButton()
    {
        player.transform.position = bundlePos.transform.position;
        player.transform.rotation = bundlePos.transform.rotation;
        uiArrangement.Hide();
        Data.MS = Making_State.bundle;
        Data.isCheck = true;
    }

    void Update()
    {
        if (Data.MS == Making_State.arrangement && Data.isCheck == true)
        {
            switch (Data.CS)
            {
                case Cloth_State.t_shirts:
                    uiArrangement.AddImage(arrangeSpite[0]);
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
            uiArrangement.AddButton("확인", ArrangeButton);
            uiArrangement.Show();
            Data.isCheck = false;
        }
    }
}
