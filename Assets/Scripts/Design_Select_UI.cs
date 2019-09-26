using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

public class Design_Select_UI : FashionController
{
	[SerializeField]
	string tutorialSceneName = null;
	[SerializeField]
	string testSceneName = null;
	[SerializeField]
	Sprite[] spriteClothes = null;

	UIBuilder uiClothes;
    UIBuilder uiSelect;

	RectTransform imgBtn;

	private void Start()
	{
        uiClothes = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);

        Rect rc = new Rect(0, 0, 200, 350);

        uiClothes.SetPaneWidth(1300);
        uiClothes.AddLabel("디자인을 선택하세요.");
        uiClothes.AddDivider();
        uiClothes.StartHorizontalSection(5);
		for (int i = 0; i< spriteClothes.Length; i++)
		{
			ClothType ct = (ClothType)i; // 위임하기 위해 루프의 변수를 지역 변수로 할당
			uiClothes.AddImageButton(spriteClothes[i], rc, delegate { SelCloth(ct); });
		}
		uiClothes.EndHorizontalSection();
        uiClothes.Show();

		uiSelect.AddLabel("선택한 옷을 만드시겠습니까?");
		uiSelect.AddDivider();
		imgBtn = uiSelect.AddImage(spriteClothes[0], new Rect(0, 0, 450, 350));
		uiSelect.AddDivider();
		uiSelect.AddYesNoButtons("", "", OnYesNoButton);
	}

	public void SelCloth(ClothType clothType)
	{
		Data.clothType = clothType;

		uiClothes.Hide();
		imgBtn.GetComponent<Image>().sprite = spriteClothes[(int)Data.clothType];
		uiSelect.Show();
    }

    public void OnYesNoButton(Reply reply)
    {
		if (reply == Reply.Yes)
		{
			nextSceneName = Data.playType == PlayType.tutorial ? tutorialSceneName : testSceneName;
			OnTutorialEnd();
		}
		else
		{
			uiSelect.Hide();
			uiClothes.Show();
		}
	}

	public override void OnTutorialEnd()
    {
		base.OnTutorialEnd();
    }
}
