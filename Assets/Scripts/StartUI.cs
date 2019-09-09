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
	UIBuilder uiCanvas = null;
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
		uiMenu = Instantiate<UIBuilder>(uiCanvas);

		Canvas cStart = uiMenu.gameObject.GetComponent<Canvas>();
		cStart.renderMode = RenderMode.WorldSpace;
		cStart.worldCamera = worldCamera;

		uiMenu.AddLabel("패션 디자이너");
		uiMenu.AddDivider();
		uiMenu.AddButton("용어 설명", ButtonTerms);
		uiMenu.AddButton("튜토리얼", ButtonTutorial);
		uiMenu.AddButton("테스트", ButtonTest);
		uiMenu.AddButton("끝내기", ButtonExit);
		uiMenu.Show();

		uiTerm = Instantiate<UIBuilder>(uiCanvas);

		Canvas cTerm = uiTerm.gameObject.GetComponent<Canvas>();
		cTerm.renderMode = RenderMode.WorldSpace;
		cTerm.worldCamera = worldCamera;

		uiTerm.SetPaneWidth(UIBuilder.PANE_CENTER, 950);
		uiTerm.AddLabel("용어 설명");
		uiTerm.AddDivider();
		uiTerm.AddLabel("항목\t\t내용", TextAnchor.MiddleLeft);
		uiTerm.AddLabel("완성선\t\t패턴의 완성선\n" +
			"안내선\t\t완성선을 그리기 위한 보조선\n" +
			"안단선\t\t안단 패턴을 표시하기 위한 선\n" +
			"절개선\t\t가위질을 해야함을 나타내는 표시\n" +
			"꺾임선\t\t접는 선 표시\n" +
			"스티치선\t스티치의 시작 부분과 끝부분을 나타낸다.\n" +
			"등분선\t\t등분을 표시하며 부호를 붙이는 경우도 있음", TextAnchor.MiddleLeft);
		uiTerm.AddButton("메뉴로", ButtonMenu);

        //uiTerm.Show();
        //		StartCoroutine(AlignUI());
        uiSelect = Instantiate<UIBuilder>(uiCanvas);

        Canvas cSelect = uiSelect.gameObject.GetComponent<Canvas>();
        cSelect.renderMode = RenderMode.WorldSpace;
        cSelect.worldCamera = worldCamera;

        uiSelect.AddLabel("디자인을 선택하세요.");
        uiSelect.AddDivider();
        uiSelect.AddButton("확인", SelectButton);
        //uiSelect.AddButton("확인", ButtonTutorial);

        //uiSelect.Show();
        uiOrigin = Instantiate<UIBuilder>(uiCanvas);

        Canvas cOrigin = uiOrigin.gameObject.GetComponent<Canvas>();
        cOrigin.renderMode = RenderMode.WorldSpace;
        cOrigin.worldCamera = worldCamera;

        uiOrigin.AddLabel("패턴 제도 원형");
        uiOrigin.AddDivider();
        //uiOrigin.AddButton("확인", SelectButton);
    }

    IEnumerator AlignUI()
	{
		uiTerm.Show();
		yield return new WaitForSeconds(0.2f);
		uiMenu.Hide();
		uiTerm.Hide();
 		yield return new WaitForSeconds(0.2f);
 		uiTerm.Show();
// 		uiTerm.Hide();
// 		yield return new WaitForSeconds(0.2f);
//		uiMenu.Show();
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
