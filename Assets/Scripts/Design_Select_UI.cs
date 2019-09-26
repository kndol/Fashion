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
    Transform testmodePos = null;
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
    Test_Mode_UI test_mode;

    Sprite selectSprite;

    void Start()
    {
        uiClothes = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);
    }

    public void StartTutorial()
    {
        Rect rc = new Rect(0, 0, 200, 350);

        uiClothes.SetPaneWidth(1300);
        uiClothes.AddLabel("디자인을 선택하세요.");
        uiClothes.AddDivider();
        uiClothes.StartHorizontalSection(5);
        uiClothes.AddImageButton(spriteTshirt, rc, TshirtButton);
        uiClothes.AddImageButton(spriteShirt, rc, ShirtsButton);
        uiClothes.AddImageButton(spritePants, rc, PantsButton);
        uiClothes.AddImageButton(spriteSkirt, rc, SkirtButton);
        uiClothes.AddImageButton(spriteSkirt, rc, BodyButton);       //몸판 버튼
        uiClothes.AddImageButton(spriteSkirt, rc, SleeveButton);     //소매 버튼
        uiClothes.EndHorizontalSection();
        uiClothes.Show();
    }

    public void YesNoShow()
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
        uiSelect.AddButton("Yes", YesButton);
        uiSelect.AddButton("No", NoButton);
        uiSelect.Show();
    }

    public void TshirtButton()    //티셔트 버튼
    {
        Data.CS = Cloth_State.t_shirts;
        uiClothes.Hide();
        YesNoShow();
    }

    public void ShirtsButton()      //셔츠 버튼
    {
        Data.CS = Cloth_State.shirts;
        uiClothes.Hide();
        YesNoShow();
    }

    public void PantsButton()       //바지 버튼
    {
        Data.CS = Cloth_State.pants;
        uiClothes.Hide();
        YesNoShow();
    }

    public void SkirtButton()       //치마 버튼
    {
        Data.CS = Cloth_State.skirt;
        uiClothes.Hide();
        YesNoShow();
    }

    public void BodyButton()        //몸판 버튼
    {
        Data.CS = Cloth_State.Body;
        uiClothes.Hide();
        YesNoShow();
    }

    public void SleeveButton()       //소매 버튼
    {
        Data.CS = Cloth_State.Sleeve;
        uiClothes.Hide();
        YesNoShow();
    }

    public void YesButton()
    {
        player.transform.position = originPos.transform.position;
        player.transform.rotation = originPos.transform.rotation;
        Destroy(uiClothes.gameObject);
        Destroy(uiSelect.gameObject);
        uiClothes = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);
        Data.MS = Making_State.original_form;

        if(Data.PM == Play_Mode.test)
        {
            player.transform.position = testmodePos.transform.position;
            player.transform.rotation = testmodePos.transform.rotation;
            test_mode.Question_Show();
        }
        Data.isCheck = true;
    }

    public void NoButton()
    {
        Destroy(uiSelect.gameObject);
        uiClothes.Show();
        uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);
    }
}
