using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Design_Select_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform originPos = null;
    [SerializeField]
    Sprite spriteTshirt = null;
    [SerializeField]
    Sprite spriteShirt = null;
    [SerializeField]
    Sprite spritePants = null;
    [SerializeField]
    Sprite spriteSkirt = null;

    UIBuilder uiClothes;
    UIBuilder uiSelect;

    Sprite selectSprite;

    void Start()
    {
        if (Data.CS == Cloth_State.start)
        {
            uiClothes = Instantiate<UIBuilder>(uiCanvasPrefab);
            uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);
        }
    }

    public void Tshirt_Button()    //티셔트 버튼
    {
        Data.CS = Cloth_State.t_shirts;
        uiClothes.Hide();
        YesNo_Show_Confirm();
    }

    public void Shirts_Button()      //셔츠 버튼
    {
        Data.CS = Cloth_State.shirts;
        uiClothes.Hide();
        YesNo_Show_Confirm();
    }

    public void Pants_Button()       //바지 버튼
    {
        Data.CS = Cloth_State.pants;
        uiClothes.Hide();
        YesNo_Show_Confirm();
    }

    public void Skirt_Button()       //치마 버튼
    {
        Data.CS = Cloth_State.skirt;
        uiClothes.Hide();
        YesNo_Show_Confirm();
    }

    public void Body_Button()        //몸판 버튼
    {
        Data.CS = Cloth_State.Body;
        uiClothes.Hide();
        YesNo_Show_Confirm();
    }

    public void Sleeve_Button()       //소매 버튼
    {
        Data.CS = Cloth_State.Sleeve;
        uiClothes.Hide();
        YesNo_Show_Confirm();
    }

    public void Yes_Button()
    {
        player.transform.position = originPos.transform.position;
        player.transform.rotation = originPos.transform.rotation;
        Destroy(uiClothes.gameObject);
        Destroy(uiSelect.gameObject);
        uiClothes = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);
        Data.MS = Making_State.original_form;
        Data.isCheck = true;
    }

    public void No_Button()
    {
        Destroy(uiSelect.gameObject);
        uiClothes.Show();
        uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);
    }

    public void YesNo_Show_Confirm()
    {
        uiSelect.AddLabel("선택한 옷을 만드시겠습니까?");
        uiSelect.AddDivider();
        switch (Data.CS)
        {
            case Cloth_State.t_shirts:
                selectSprite = spriteTshirt;
                break;
            case Cloth_State.shirts:
                selectSprite = spriteShirt;
                break;
            case Cloth_State.pants:
                selectSprite = spritePants;
                break;
            case Cloth_State.skirt:
                selectSprite = spriteSkirt;
                break;
            case Cloth_State.Body:
                selectSprite = spriteSkirt;
                break;
            default:   //소매
                selectSprite = spriteSkirt;
                break;        
        }
        uiSelect.AddImage(selectSprite, new Rect(0, 0, 450, 350));
        uiSelect.AddDivider();
        uiSelect.AddButton("Yes", Yes_Button);
        uiSelect.AddButton("No", No_Button);
        uiSelect.Show();
    }
    public void Cloth_Select_Show_Confirm()
    {
        Rect rc = new Rect(0, 0, 200, 350);

        uiClothes.SetPaneWidth(1300);
        uiClothes.AddLabel("디자인을 선택하세요.");
        uiClothes.AddDivider();
        uiClothes.StartHorizontalSection(5);
        uiClothes.AddImageButton(spriteTshirt, rc, Tshirt_Button);
        uiClothes.AddImageButton(spriteShirt, rc, Shirts_Button);
        uiClothes.AddImageButton(spritePants, rc, Pants_Button);
        uiClothes.AddImageButton(spriteSkirt, rc, Skirt_Button);
        if (Data.PM == Play_Mode.tutorial)
        {
            uiClothes.AddImageButton(spriteSkirt, rc, Body_Button);       //몸판 버튼
            uiClothes.AddImageButton(spriteSkirt, rc, Sleeve_Button);     //소매 버튼
        }
        uiClothes.EndHorizontalSection();
        uiClothes.Show();
    }

    void Update()
    {
        if (Data.MS == Making_State.Design_Select && Data.isCheck == true)
        {
            Data.isCheck = false;
            Cloth_Select_Show_Confirm();
            /*switch(Data.PM)
            {
                case Play_Mode.tutorial:
                    break;
                case Play_Mode.test:
                    break;
            }*/
        }
    }
}
