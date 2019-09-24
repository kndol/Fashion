using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

enum Tshirts_State { start, front, back, sleeve };
public class Arrangement_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    //[SerializeField]
    //Transform arrangePos = null;
    //[SerializeField]
    //RectTransform arrangeUI = null;
    [SerializeField]
    RectTransform []arrangeUI_child = null;
    [SerializeField]
    Transform bundlePos = null;
    [SerializeField]
    Sprite bankgroundSprite = null;
    [SerializeField]
    Sprite[] tshirts_btnSprite = null;     //티셔츠 버튼 이미지
    [SerializeField]
    Sprite[] arrangeSprite = null;  //배치 완성 이미지

    UIBuilder uiArrangement;

    Rect rc = new Rect(0, 0, 150, 100);
    Color color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

    Tshirts_State TS = Tshirts_State.start;

    static int count = 0;

    void Start()
    {
        if (Data.MS == Making_State.start)
        {
            uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);
            for(int i = 0; i < 3; i++)
            {
                arrangeUI_child[i].gameObject.SetActive(false);
            }
        }
    }

    public void ArrangeButton()
    {
        player.transform.position = bundlePos.transform.position;
        player.transform.rotation = bundlePos.transform.rotation;
        uiArrangement.Hide();
        Data.MS = Making_State.bundle;
        Data.isCheck = true;
    }

    public void Next_Button()
    {
        Destroy(uiArrangement.gameObject);
        uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiArrangement.AddLabel("배치완료", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        uiArrangement.AddDivider(UIBuilder.PANE_LEFT);
        uiArrangement.AddImage(arrangeSprite[0], new Rect(0, 0, 450, 350), UIBuilder.PANE_LEFT);
        uiArrangement.AddLabel("Description", UIBuilder.PANE_CENTER);
        uiArrangement.AddDivider(UIBuilder.PANE_CENTER);
        uiArrangement.AddLabel("배치설명", UIBuilder.PANE_CENTER);
        uiArrangement.AddButton("확인", ArrangeButton, UIBuilder.PANE_CENTER);
        uiArrangement.Show();
    }

    public void Tshirts_Front_Btn()
    {
        count++;
        Destroy(uiArrangement.gameObject);
        uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);
        arrangeUI_child[0].gameObject.SetActive(true);
        uiArrangement.AddLabel("배치", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        uiArrangement.AddDivider(UIBuilder.PANE_LEFT);
        uiArrangement.AddLabel("원단을 클릭하여 배치하세요.", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        switch (count)
        {
            case 1:
                switch (Data.CS)
                {
                    case Cloth_State.t_shirts:
                        var frontBtn = uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                        frontBtn.GetComponent<Button>().enabled = false;
                        frontBtn.GetComponent<Image>().color = color;
                        uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                        uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                        break;
                    case Cloth_State.shirts:
                        break;
                    case Cloth_State.pants:
                        break;
                    case Cloth_State.skirt:
                        break;
                }
                break;
            case 2:
                switch (TS)
                {
                    case Tshirts_State.back:
                        switch (Data.CS)
                        {
                            case Cloth_State.t_shirts:
                                var frontBtn = uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                                frontBtn.GetComponent<Button>().enabled = false;
                                frontBtn.GetComponent<Image>().color = color;
                                var backBtn = uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                                backBtn.GetComponent<Button>().enabled = false;
                                backBtn.GetComponent<Image>().color = color;
                                uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                                break;
                            case Cloth_State.shirts:
                                break;
                            case Cloth_State.pants:
                                break;
                            case Cloth_State.skirt:
                                break;
                        }
                        break;
                    case Tshirts_State.sleeve:
                        switch (Data.CS)
                        {
                            case Cloth_State.t_shirts:
                                var frontBtn = uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                                frontBtn.GetComponent<Button>().enabled = false;
                                frontBtn.GetComponent<Image>().color = color;
                                uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                                var sleeveBtn = uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                                sleeveBtn.GetComponent<Button>().enabled = false;
                                sleeveBtn.GetComponent<Image>().color = color;
                                break;
                            case Cloth_State.shirts:
                                break;
                            case Cloth_State.pants:
                                break;
                            case Cloth_State.skirt:
                                break;
                        }
                        break;
                }
                break;
            case 3:
                switch (Data.CS)
                {
                    case Cloth_State.t_shirts:
                        var frontBtn = uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                        frontBtn.GetComponent<Button>().enabled = false;
                        frontBtn.GetComponent<Image>().color = color;
                        var backBtn = uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                        backBtn.GetComponent<Button>().enabled = false;
                        backBtn.GetComponent<Image>().color = color;
                        var sleeveBtn = uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                        sleeveBtn.GetComponent<Button>().enabled = false;
                        sleeveBtn.GetComponent<Image>().color = color;
                        break;
                    case Cloth_State.shirts:
                        break;
                    case Cloth_State.pants:
                        break;
                    case Cloth_State.skirt:
                        break;
                }
                uiArrangement.AddButton("다음으로", Next_Button, UIBuilder.PANE_LEFT);
                break;
        }
        uiArrangement.Show();
        TS = Tshirts_State.front;
    }

    public void Tshirts_Back_Btn()
    {
        count++;
        Destroy(uiArrangement.gameObject);
        uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);
        arrangeUI_child[1].gameObject.SetActive(true);
        uiArrangement.AddLabel("배치", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        uiArrangement.AddDivider(UIBuilder.PANE_LEFT);
        uiArrangement.AddLabel("원단을 클릭하여 배치하세요.", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        switch (count)
        {
            case 1:
                switch (Data.CS)
                {
                    case Cloth_State.t_shirts:
                        uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                        var backBtn = uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                        backBtn.GetComponent<Button>().enabled = false;
                        backBtn.GetComponent<Image>().color = color;
                        uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                        break;
                    case Cloth_State.shirts:
                        break;
                    case Cloth_State.pants:
                        break;
                    case Cloth_State.skirt:
                        break;
                }
                break;
            case 2:
                switch (TS)
                {
                    case Tshirts_State.front:
                        switch (Data.CS)
                        {
                            case Cloth_State.t_shirts:
                                var frontBtn = uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                                frontBtn.GetComponent<Button>().enabled = false;
                                frontBtn.GetComponent<Image>().color = color;
                                var backBtn = uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                                backBtn.GetComponent<Button>().enabled = false;
                                backBtn.GetComponent<Image>().color = color;
                                uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                                break;
                            case Cloth_State.shirts:
                                break;
                            case Cloth_State.pants:
                                break;
                            case Cloth_State.skirt:
                                break;
                        }
                        break;
                    case Tshirts_State.sleeve:
                        switch (Data.CS)
                        {
                            case Cloth_State.t_shirts:
                                uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                                var backBtn = uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                                backBtn.GetComponent<Button>().enabled = false;
                                backBtn.GetComponent<Image>().color = color;
                                var sleeveBtn = uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                                sleeveBtn.GetComponent<Button>().enabled = false;
                                sleeveBtn.GetComponent<Image>().color = color;
                                break;
                            case Cloth_State.shirts:
                                break;
                            case Cloth_State.pants:
                                break;
                            case Cloth_State.skirt:
                                break;
                        }
                        break;
                }
                break;
            case 3:
                switch (Data.CS)
                {
                    case Cloth_State.t_shirts:
                        var frontBtn = uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                        frontBtn.GetComponent<Button>().enabled = false;
                        frontBtn.GetComponent<Image>().color = color;
                        var backBtn = uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                        backBtn.GetComponent<Button>().enabled = false;
                        backBtn.GetComponent<Image>().color = color;
                        var sleeveBtn = uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                        sleeveBtn.GetComponent<Button>().enabled = false;
                        sleeveBtn.GetComponent<Image>().color = color;
                        break;
                    case Cloth_State.shirts:
                        break;
                    case Cloth_State.pants:
                        break;
                    case Cloth_State.skirt:
                        break;
                }
                uiArrangement.AddButton("다음으로", Next_Button, UIBuilder.PANE_LEFT);
                break;
        }
        uiArrangement.Show();
        TS = Tshirts_State.back;
    }

    public void Tshirts_Sleeve_Btn()
    {
        count++;
        Destroy(uiArrangement.gameObject);
        uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);
        arrangeUI_child[2].gameObject.SetActive(true);
        uiArrangement.AddLabel("배치", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        uiArrangement.AddDivider(UIBuilder.PANE_LEFT);
        uiArrangement.AddLabel("원단을 클릭하여 배치하세요.", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        switch (count)
        {
            case 1:
                switch (Data.CS)
                {
                    case Cloth_State.t_shirts:
                        uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                        uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼                    
                        var sleeveBtn = uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                        sleeveBtn.GetComponent<Button>().enabled = false;
                        sleeveBtn.GetComponent<Image>().color = color;
                        break;
                    case Cloth_State.shirts:
                        break;
                    case Cloth_State.pants:
                        break;
                    case Cloth_State.skirt:
                        break;
                }
                break;
            case 2:
                switch (TS)
                {
                    case Tshirts_State.front:
                        switch (Data.CS)
                        {
                            case Cloth_State.t_shirts:
                                var frontBtn = uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                                frontBtn.GetComponent<Button>().enabled = false;
                                frontBtn.GetComponent<Image>().color = color;
                                uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼                            
                                var sleeveBtn = uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                                sleeveBtn.GetComponent<Button>().enabled = false;
                                sleeveBtn.GetComponent<Image>().color = color;
                                break;
                            case Cloth_State.shirts:
                                break;
                            case Cloth_State.pants:
                                break;
                            case Cloth_State.skirt:
                                break;
                        }
                        break;
                    case Tshirts_State.back:
                        switch (Data.CS)
                        {
                            case Cloth_State.t_shirts:
                                uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼                               
                                var backBtn = uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                                backBtn.GetComponent<Button>().enabled = false;
                                backBtn.GetComponent<Image>().color = color;
                                var sleeveBtn = uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                                sleeveBtn.GetComponent<Button>().enabled = false;
                                sleeveBtn.GetComponent<Image>().color = color;
                                break;
                            case Cloth_State.shirts:
                                break;
                            case Cloth_State.pants:
                                break;
                            case Cloth_State.skirt:
                                break;
                        }
                        break;
                }
                break;
            case 3:
                switch (Data.CS)
                {
                    case Cloth_State.t_shirts:
                        var frontBtn = uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                        frontBtn.GetComponent<Button>().enabled = false;
                        frontBtn.GetComponent<Image>().color = color;
                        var backBtn = uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                        backBtn.GetComponent<Button>().enabled = false;
                        backBtn.GetComponent<Image>().color = color;
                        var sleeveBtn = uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                        sleeveBtn.GetComponent<Button>().enabled = false;
                        sleeveBtn.GetComponent<Image>().color = color;
                        break;
                    case Cloth_State.shirts:
                        break;
                    case Cloth_State.pants:
                        break;
                    case Cloth_State.skirt:
                        break;
                }
                uiArrangement.AddButton("다음으로", Next_Button, UIBuilder.PANE_LEFT);
                break;
        }
        uiArrangement.Show();
        TS = Tshirts_State.sleeve;
    }

    public void Show_Confirm()
    {
        uiArrangement.AddLabel("배치", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        uiArrangement.AddDivider(UIBuilder.PANE_LEFT);
        uiArrangement.AddLabel("원단을 클릭하여 배치하세요.", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
        switch (Data.CS)
        {
            case Cloth_State.t_shirts:               
                uiArrangement.AddImageButton(tshirts_btnSprite[0], rc, Tshirts_Front_Btn, UIBuilder.PANE_LEFT);   //티셔츠 앞면 버튼
                uiArrangement.AddImageButton(tshirts_btnSprite[1], rc, Tshirts_Back_Btn, UIBuilder.PANE_LEFT);    //티셔츠 뒷면 버튼
                uiArrangement.AddImageButton(tshirts_btnSprite[2], rc, Tshirts_Sleeve_Btn, UIBuilder.PANE_LEFT);  //티셔트 소매 버튼
                break;
            case Cloth_State.shirts:
                break;
            case Cloth_State.pants:
                break;
            case Cloth_State.skirt:
                break;
        }
        uiArrangement.Show();
    }

    void Update()
    {
        if (Data.MS == Making_State.arrangement && Data.isCheck == true)
        {
            Data.isCheck = false;
            Show_Confirm();
        }
    }
}