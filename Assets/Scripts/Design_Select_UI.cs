using UnityEngine;
using UnityEngine.SceneManagement;
using Fashion;
using Fashion.UIManager;

public class Design_Select_UI : FashionController
{
	[SerializeField]
	Sprite[] spriteClothes = null;

	UIBuilder uiClothes = null;
    UIBuilder uiConfirm = null;

	void Start()
	{
		ShowClothList();
	}

	void ShowClothList()
	{
		if (uiConfirm != null)
			Destroy(uiConfirm.gameObject);

		uiClothes = Instantiate<UIBuilder>(uiCanvasPrefab);

		Rect rc = new Rect(0, 0, 200, 300);

		uiClothes.SetPosition(menuPosition);
        uiClothes.SetPaneWidth(920);
		uiClothes.AddLabel("디자인을 선택하세요.");
		uiClothes.AddDivider();
		uiClothes.StartHorizontalSection(30);
		for (int i = 0; i < spriteClothes.Length; i++)
		{
			ClothType ct = (ClothType)i;      // 위임하기 위해 루프의 변수를 지역 변수로 할당
			uiClothes.AddImageButton(spriteClothes[i], rc, delegate { ShowConfirm(ct); });
		}
		uiClothes.EndHorizontalSection();

		uiClothes.Show();
	}

	void ShowConfirm(ClothType clothType)
	{
		Data.clothType = clothType;

		Destroy(uiClothes.gameObject);

		uiConfirm = Instantiate<UIBuilder>(uiCanvasPrefab);

		uiConfirm.SetPosition(menuPosition);
		uiConfirm.AddLabel("선택한 옷을 만드시겠습니까?");
		uiConfirm.AddDivider();
		uiConfirm.AddImage(spriteClothes[(int)clothType], new Rect(0, 0, 450, 350));
		uiConfirm.AddDivider();
		uiConfirm.AddYesNoButtons("", "", OnTutorialEnd, ShowClothList);

		uiConfirm.Show();
	}

	public override void OnTutorialEnd()
    {
		base.OnTutorialEnd();
    }
}