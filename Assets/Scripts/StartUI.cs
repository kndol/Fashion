using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

// Show off all the Debug UI components.
public class StartUI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Transform selectPos = null;

    Design_Select_UI design_selectUI;
    UIBuilder uiMenu;
	UIBuilder uiTerm;

    void Start ()
    {
        Init_Data();
        uiMenu = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiMenu.AddLabel("메뉴");
        uiMenu.AddDivider();
        uiMenu.AddButton("용어설명", ButtonTerms);
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
    }

	public void ButtonMenu()
	{
		uiTerm.Hide();
		uiMenu.Show();
	}

	public void ButtonTerms()
    {
		uiMenu.Hide();
		uiTerm.Show();
    }

	public void ButtonTutorial()    //튜토리얼 버튼
	{
        player.transform.position = selectPos.transform.position;
        player.transform.rotation = selectPos.transform.rotation;
        uiMenu.Hide();
        Data.MS = Making_State.Design_Select;
        Data.PM = Play_Mode.tutorial;
        design_selectUI.StartTutorial();
    }

	public void ButtonTest()        //테스트 버튼
	{
        player.transform.position = selectPos.transform.position;
        player.transform.rotation = selectPos.transform.rotation;
        uiMenu.Hide();
        Data.MS = Making_State.Design_Select;
        Data.PM = Play_Mode.test;
        design_selectUI.StartTutorial();
    }

	public void ButtonExit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

    public void Init_Data()    //초기화 함수
    {
        Data.Score = 100;
        Data.isCheck = false;
        Data.PM = Play_Mode.start;
        Data.CS = Cloth_State.start;
        Data.MS = Making_State.start;
    }
}
