using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

enum Tshirts_State { start, front, back, sleeve };
public class Arrangement_UI : FashionController
{
	[SerializeField]
	GameObject[] arrangeParts = null;
	[SerializeField]
	GameObject[] dotParts = null;
	[SerializeField]
	GameObject[] cutParts = null;
	[SerializeField]
	GameObject background = null;
	
	UIBuilder uiArrangement;

	Transform[] imageBtns;
	Rect imgBtnRect = new Rect(0, 0, 150, 100);

	int count = 0;

	void Start()
	{
		for (int i = 0; i < arrangeParts.Length; i++)
		{
			arrangeParts[i].SetActive(false);
		}
	}

	public override void StartTutorial()
	{
		base.StartTutorial();
		// 일단 base.StartTutorial() 호출한 뒤에 작업 시작

		uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);
		background.SetActive(true);
		uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);
		uiArrangement.AddLabel("티셔츠 마름질 배치");
		uiArrangement.AddDivider();
		uiArrangement.AddScrollView("배치에 관한 설명 넣기");
		uiArrangement.AddButton("확인", DoArrange);
		uiArrangement.Show();
	}

	public void DoArrange()
	{
		imageBtns = new Transform[arrangeParts.Length];
		count = 0;

		uiArrangement.AddLabel("배치");
		uiArrangement.AddDivider();
		uiArrangement.AddLabel("원단을 클릭하여 배치하세요.");
		switch (Data.CS)
		{
			case Cloth_State.t_shirts:
				for (int i = 0; i < arrangeParts.Length; i++)
				{
					Sprite sprite = arrangeParts[i].GetComponent<Image>().sprite;
					imageBtns[i] = uiArrangement.AddImageButton(sprite, imgBtnRect, delegate { arrange(i); });   //티셔츠 배치하기
				}
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

	public void arrange(int partNum)
	{
		imageBtns[partNum].GetComponent<Button>().interactable = false;

		switch (Data.CS)
		{
			case Cloth_State.t_shirts:
				arrangeParts[partNum].SetActive(true);
				break;
			case Cloth_State.shirts:
				break;
			case Cloth_State.pants:
				break;
			case Cloth_State.skirt:
				break;
		}
		if (++count == arrangeParts.Length)
			uiArrangement.AddButton("다음으로", GoNext);
	}

	public void GoNext()
	{
		Destroy(uiArrangement.gameObject);
	}

	public void OnArrangeFinished()
	{

	}

	public override void OnTutorialEnd()
	{
		uiArrangement.Hide();

		// 뭔가 한 뒤 마지막에 base.OnAllDone() 호출
		base.OnTutorialEnd();
	}
}    