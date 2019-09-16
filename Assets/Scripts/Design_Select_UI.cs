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

    void Start()
    {
        if (Data.MS == Making_State.start)
        {
			Rect rc = new Rect(0, 0, 200, 350);
            uiClothes = Instantiate<UIBuilder>(uiCanvasPrefab);

			uiClothes.SetPaneWidth(880);
			uiClothes.AddLabel("디자인을 선택하세요.");
            uiClothes.AddDivider();
			uiClothes.StartHorizontalSection(5);
			uiClothes.AddImageButton(spriteTshirt, rc,Tshirt_Button);
            uiClothes.AddImageButton(spriteShirt, rc, Shirts_Button);
            uiClothes.AddImageButton(spritePants, rc, Pants_Button);
            uiClothes.AddImageButton(spriteSkirt, rc, Skirt_Button);

			uiClothes.EndHorizontalSection();

            uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);

            uiSelect.AddLabel("선택한 옷을 만드시겠습니까?");
            uiSelect.AddDivider();
            uiSelect.AddButton("Yes", Yes_Button);
            uiSelect.AddButton("No", No_Button);
        }
    }

    public void Tshirt_Button()    //티셔트 버튼
    {
        uiClothes.Hide();
        uiSelect.Show();
        Data.CS = Cloth_State.t_shirts;
    }

    public void Shirts_Button()      //셔츠 버튼
    {
        uiClothes.Hide();
        uiSelect.Show();
        Data.CS = Cloth_State.shirts;
    }

    public void Pants_Button()       //바지 버튼
    {
        uiClothes.Hide();
        uiSelect.Show();
        Data.CS = Cloth_State.pants;
    }

    public void Skirt_Button()       //치마 버튼
    {
        uiClothes.Hide();
        uiSelect.Show();
        Data.CS = Cloth_State.skirt;
    }

    public void Yes_Button()
    {
        player.transform.position = originPos.transform.position;
        player.transform.rotation = originPos.transform.rotation;
        uiClothes.Hide();
        uiSelect.Hide();
        Data.MS = Making_State.original_form;
        Data.isCheck = true;
    }

    public void No_Button()
    {
        uiSelect.Hide();
        uiClothes.Show();
        Data.CS = Cloth_State.start;
    }

    void Update()
    {
        if (Data.MS == Making_State.Design_Select && Data.isCheck == true)
        {
            uiClothes.Show();
            Data.isCheck = false;
        }
    }
}
