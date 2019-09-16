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
	Sprite spriteTshirt;
	[SerializeField]
	Sprite spriteShirt;
	[SerializeField]
	Sprite spritePants;
	[SerializeField]
	Sprite spriteSkirt;

	UIBuilder uiCloths;
    UIBuilder uiSelect;

    void Start()
    {
        if (Data.MS == Making_State.start)
        {
            uiCloths = Instantiate<UIBuilder>(uiCanvasPrefab);

            uiCloths.AddLabel("디자인을 선택하세요.");
            uiCloths.AddDivider();
            uiCloths.AddImageButton(spriteTshirt, Tshirt_Button);
            uiCloths.AddImageButton(spriteShirt, Shirts_Button);
            uiCloths.AddImageButton(spritePants, Pants_Button);
            uiCloths.AddImageButton(spriteSkirt, Skirt_Button);

            uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);

            uiSelect.AddLabel("선택한 옷을 만드시겠습니까?");
            uiSelect.AddDivider();
            uiSelect.AddButton("Yes", Yes_Button);
            uiSelect.AddButton("No", No_Button);
        }
    }

    public void Tshirt_Button()    //티셔트 버튼
    {
        uiCloths.Hide();
        uiSelect.Show();
        Data.CS = Cloth_State.t_shirts;
    }

    public void Shirts_Button()      //셔츠 버튼
    {
        uiCloths.Hide();
        uiSelect.Show();
        Data.CS = Cloth_State.shirts;
    }

    public void Pants_Button()       //바지 버튼
    {
        uiCloths.Hide();
        uiSelect.Show();
        Data.CS = Cloth_State.pants;
    }

    public void Skirt_Button()       //치마 버튼
    {
        uiCloths.Hide();
        uiSelect.Show();
        Data.CS = Cloth_State.skirt;
    }

    public void Yes_Button()
    {
        player.transform.position = originPos.transform.position;
        player.transform.rotation = originPos.transform.rotation;
        uiCloths.Hide();
        uiSelect.Hide();
        Data.MS = Making_State.original_form;
        Data.isCheck = true;
    }

    public void No_Button()
    {
        uiSelect.Hide();
        uiCloths.Show();
        Data.CS = Cloth_State.start;
    }

    void Update()
    {
        if (Data.MS == Making_State.Design_Select && Data.isCheck == true)
        {
            uiCloths.Show();
            Data.isCheck = false;
        }
    }
}
