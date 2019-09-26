using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

[Serializable]
public class Parts
{
	[Tooltip("배열용 파츠 지정")]
	public GameObject[] arrangeParts = null;
	[Tooltip("점선 재단선 파츠 지정")]
	public GameObject[] dotParts = null;
	[Tooltip("실선 재단선 파츠 지정")]
	public GameObject[] cutParts = null;
	[Tooltip("재단 작업할 때 레이케스트를 감지하기 위한 콜라이더 객체들의 그룹(부모)객체")]
	public GameObject[] pointGroup;

}

public class Tutorial_Arrange_and_Cut : FashionController
{
	[Header("배경 이미지")]
	[Tooltip("작업에 사용할 배경 이미지")]
	[SerializeField]
	GameObject background = null;
	[Header("의상 종류")]
	[Tooltip("작업에 사용할 의상의 종류 갯수와 파츠 지정")]
	[SerializeField]
	Parts[] parts = null;

	UIBuilder uiArrangement;

	RectTransform[] imageBtns;
	RectTransform btnOk;
	Rect imgBtnRect = new Rect(0, 0, 150, 100);
	int curCountPointSelected = 0;

	List<bool>[] PointSelected;
	int[] countPointSelected;

	Fashion.UIManager.LaserPointer lp;

	bool hasLaser = false;

	public override void Start()
	{
		base.Start();
		int i, j;
		for (i=0; i<parts.Length; i++)
		{
			for (j = 0; j < parts[i].arrangeParts.Length; j++)
			{
				parts[i].arrangeParts[j].SetActive(false);
			}
			for (j = 0; j < parts[i].dotParts.Length; j++)
			{
				InitImageFilled(parts[i].dotParts[j]);
			}
			for (j = 0; j < parts[i].cutParts.Length; j++)
			{
				InitImageFilled(parts[i].cutParts[j]);
			}
			for (j = 0; j < parts[i].pointGroup.Length; j++)
			{
				parts[i].pointGroup[j].SetActive(false);
			}
		}
		background.SetActive(false);
		StartTutorial();
	}

	void LateUpdate()
	{
		if (!hasLaser)
		{
			lp = FindObjectOfType<Fashion.UIManager.LaserPointer>();
			if (lp)
			{
				GetComponent<OVRRaycaster>().pointer = lp.gameObject;
				hasLaser = true;
			}
		}
	}

	/// <summary>
	/// 이미지를 Filled 형으로 초기화하고 게임오브젝트를 비활성화
	/// </summary>
	/// <param name="go">이미지를 포함하고 있는 게임오브젝트</param>
	void InitImageFilled(GameObject go)
	{
		Image img = go.GetComponent<Image>();

		img.type = Image.Type.Filled;
		img.fillMethod = Image.FillMethod.Radial360;
		img.fillOrigin = (int)Image.Origin360.Top;
		img.fillClockwise = false;
		img.fillAmount = 0;
		img.preserveAspect = true;

		go.SetActive(false);
	}

	void StartTutorial()
	{
		//base.StartTutorial();
		// 일단 base.StartTutorial() 호출한 뒤에 작업 시작

		background.SetActive(true);

		uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);
		uiArrangement.AddLabel("<B>티셔츠 마름질 - 배치</B>");
		uiArrangement.AddDivider();
		uiArrangement.AddScrollView("식서방향과 골선의 위치와 뱡향에 따라 배치\n\n" +
			"<b>*</b> <i>식서방향</i> : 원단 가장자리의 구멍이 뚫려있는 부분.원단이 제일 늘어나지 않는 방향.\n" +
			"			 식서방향에 맞추는 이유: 옷의 늘어짐과 뒤틀림이 최대한 없게 하기 위해서.\n" +
			"<b>*</b> 골선이라고 표시된 부분의 선을 중심축으로 하여 본을 바로 해서 올려 놓고 1장 그리고, 중심축에서 본을 넘기듯" +
			" 뒤집어서 좌, 우 대칭이 되도록 패턴을 한번 더 그려주는 것.\n" +
			"  원단이 접히는 위치에 배치를 하며 이때, 원단의 시접방향과도 맞아야 한다.\n" +
			"  원단의 시접방향과 다른 경우에는 옷이 늘어지며 틀어지는 경우가 있다.\n" +
			"<b>*</b> 배치할 때 시접량에 유의해서 위치를 옮긴다.\n" +
			"  시접량이 많아 원단의 끝에 닿아 면적이 좁거나, 시접량이 서로 맞물리는 경우에는 시접량이 부족하여 재봉 시 어려움이 있다.");
		btnOk = uiArrangement.AddButton("확인", DoArrange);
		uiArrangement.Show();
	}

	public void DoArrange()
	{
		btnOk.GetComponent<Button>().interactable = false;
		imageBtns = new RectTransform[parts[(int)Data.CS].arrangeParts.Length];
		curCountPointSelected = 0;

		uiArrangement.AddLabel("배치", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
		uiArrangement.AddDivider(UIBuilder.PANE_RIGHT);
		uiArrangement.AddLabel("원단을 클릭하여 배치하세요.", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
		for (int i = 0; i < parts[(int)Data.CS].arrangeParts.Length; i++)
		{
			Sprite sprite = parts[(int)Data.CS].arrangeParts[i].GetComponent<Image>().sprite;
			imageBtns[i] = uiArrangement.AddImageButton(sprite, imgBtnRect, delegate { arrange(i); }, UIBuilder.PANE_RIGHT); // 배치하기
		}
		uiArrangement.Show();
	}

	public void arrange(int partNum)
	{
		imageBtns[partNum].GetComponent<Button>().interactable = false;

		parts[(int)Data.CS].arrangeParts[partNum].SetActive(true);
		if (++curCountPointSelected == parts[(int)Data.CS].arrangeParts.Length)
			uiArrangement.AddButton("다음으로", ShowCutDesc, UIBuilder.PANE_RIGHT);
	}

	public void ShowCutDesc()
	{
		Destroy(uiArrangement.gameObject);
		uiArrangement = Instantiate<UIBuilder>(uiCanvasPrefab);
		uiArrangement.AddLabel("<B>티셔츠 마름질 - 시접 및 재단</B>");
		uiArrangement.AddDivider();
		uiArrangement.AddScrollView("<li><b>시접</b>: 패턴을 원단에 맞게 직선은 1.5cm, 곡선은 1cm, 소매산은 2cm, 밑단은 4cm 의 여유분을 준다.</li>\n" +
			"<li><b>마킹</b>: 시접의 모서리 부분은 최대한의 각을 살려 마킹.\n" +
			"마킹할때 다트, MP, 소매와 암홀의 맞춤 표시를 시접분량 안에서 직각으로 마킹을 한다.\n" +
			"골선을 마킹할 때 원단의 끝부분과 골선의 패턴 중심 부분을 맞추어 표시헤야 한다.\n"+
			"<li><b>재단</b>: 시접선을 따라 원단을 자른다.");
		btnOk = uiArrangement.AddButton("확인", DoCut);
		uiArrangement.Show();

	}

	IEnumerator DrawImage(GameObject go, float duration)
	{
		yield return null;
		float t = 0;
		Image img = go.GetComponent<Image>();
		img.fillAmount = 0;
		while (img.fillAmount < 1)
		{
			yield return new WaitForEndOfFrame();
			t += Time.deltaTime;
			img.fillAmount = t / duration;
		}
	}

	public void DoCut()
	{
		int clothType = (int)Data.CS;
		int groupCount = parts[clothType].pointGroup.Length;
		PointSelected = new List<bool>[groupCount];
		countPointSelected = new int[groupCount];
		btnOk.GetComponent<Button>().interactable = false;

		for (int group = 0; group < parts[clothType].dotParts.Length; group++)
		{
			StartCoroutine(DrawImage(parts[clothType].dotParts[group], 1)); // 점선 이미지
			StartCoroutine(DrawImage(parts[clothType].dotParts[group].transform.GetChild(0).gameObject, 0.5f)); // 화살표
		}

		for (int group = 0; group < groupCount; group++)
		{
			int count = parts[clothType].pointGroup[group].transform.childCount;
			PointerOverHandler[] handlers = parts[clothType].pointGroup[group].GetComponentsInChildren<PointerOverHandler>();
			MeshRenderer[] rends = parts[clothType].pointGroup[group].GetComponentsInChildren<MeshRenderer>();
			PointSelected[group] = new List<bool>();
			countPointSelected[group] = 0;
			for (int id = 0; id < count; id++)
			{
				rends[id].enabled = false;
				handlers[id].group = group;
				handlers[id].id = id;
				handlers[id].OnPointerOver += OnOverHander;
				PointSelected[group].Add(false);
			}
			parts[clothType].pointGroup[group].SetActive(true);
		}

		uiArrangement.AddLabel("시접", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
		uiArrangement.AddDivider(UIBuilder.PANE_RIGHT);
		uiArrangement.AddLabel("원단 도안의 점선을 따라 포인터를 움직여 .", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
		for (int i = 0; i < parts[(int)Data.CS].arrangeParts.Length; i++)
		{
			Sprite sprite = parts[(int)Data.CS].arrangeParts[i].GetComponent<Image>().sprite;
			imageBtns[i] = uiArrangement.AddImageButton(sprite, imgBtnRect, delegate { arrange(i); }, UIBuilder.PANE_RIGHT); // 배치하기
		}
		uiArrangement.Show();
	}

	public void OnOverHander(int group, int id)
	{
		if (!PointSelected[group][id])
		{
			print("선택 group: " + group + ", id: " + id);
			PointSelected[group][id] = true;
			++countPointSelected[group];
			if (countPointSelected[group] > parts[(int)Data.CS].pointGroup[group].transform.childCount * 2 / 3)
			{
				print("성공");
			}
		}
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