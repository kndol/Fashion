using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion.UIManager;

// Show off all the Debug UI components.
public class StartUI : MonoBehaviour
{
	[SerializeField]
	UIBuilder uiCanvasPrefab = null;
	[SerializeField]
	Camera worldCamera = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform selectPos = null;
    [SerializeField]
    Transform originPos = null;

    UIBuilder uiMenu;
	UIBuilder uiTerm;
    UIBuilder uiSelect;
    UIBuilder uiOrigin;

    void Start ()
    {
		uiMenu = Instantiate<UIBuilder>(uiCanvasPrefab);

		uiMenu.AddLabel("<B>패션 디자이너</B>");
		uiMenu.AddDivider();
		uiMenu.AddButton("용어 설명", ButtonTerms);
		uiMenu.AddButton("튜토리얼", ButtonTutorial);
		uiMenu.AddButton("테스트", ButtonTest);
		uiMenu.AddButton("끝내기", ButtonExit);
		uiMenu.Show();

		uiTerm = Instantiate<UIBuilder>(uiCanvasPrefab);

		uiTerm.SetPaneWidth(980);
		uiTerm.AddLabel("<b>용어 설명</b>");
		uiTerm.AddDivider();
		uiTerm.AddLabel("항목\t\t내용", TextAnchor.MiddleLeft);
		uiTerm.AddScrollView("<b>완성선</b>\t\t패턴의 완성선\n" +
			"<b>안내선</b>\t\t완성선을 그리기 위한 보조선\n" +
			"<b>안단선</b>\t\t안단 패턴을 표시하기 위한 선\n" +
			"<b>절개선</b>\t\t가위질을 해야함을 나타내는 표시\n" +
			"<b>꺾임선</b>\t\t접는 선 표시\n" +
			"<b>스티치선</b>\t스티치의 시작 부분과 끝부분을 나타낸다.\n" +
			"<b>등분선</b>\t\t등분을 표시하며 부호를 붙이는 경우도 있음", TextAnchor.UpperLeft, 320);
		uiTerm.AddButton("메뉴로", ButtonMenu);

        //uiTerm.Show();

        uiSelect = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiSelect.AddLabel("디자인을 선택하세요.");
        uiSelect.AddDivider();
        uiSelect.AddButton("확인", SelectButton);
        //uiSelect.AddButton("확인", ButtonTutorial);

        //uiSelect.Show();
        uiOrigin = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiOrigin.AddLabel("패턴 제도 원형");
        uiOrigin.AddDivider();
        //uiOrigin.AddButton("확인", SelectButton);
    }

	public void ButtonMenu()
	{
		uiTerm.Hide();
		uiMenu.Show();
	}
	public void ButtonTerms()     //용어설명
    {
		uiMenu.Hide();
		uiTerm.Show();
    }

	public void ButtonTutorial()  //튜토리얼
	{
        player.transform.position = selectPos.transform.position;
        player.transform.rotation = selectPos.transform.rotation;
        uiMenu.Hide();
        uiSelect.Show();
        Debug.Log("튜토리얼");
	}

	public void ButtonTest()      //테스트
	{
        player.transform.position = selectPos.transform.position;
        player.transform.rotation = selectPos.transform.rotation;
        uiMenu.Hide();
        uiSelect.Show();
        Debug.Log("테스트");
	}

    public void SelectButton()
    {
        player.transform.position = originPos.transform.position;
        player.transform.rotation = originPos.transform.rotation;
        uiSelect.Hide();
        uiOrigin.Show();
    }

	public void ButtonExit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	void Update()
    {
/*
		if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Start))
		{
			if (inMenu) uIBuilder.Hide();
			else uIBuilder.Show();
			inMenu = !inMenu;
		}
*/
	}
}
