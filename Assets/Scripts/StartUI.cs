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

	UIBuilder uiMenu;
	UIBuilder uiTerm;

	void Start ()
    {
		uiMenu = Instantiate<UIBuilder>(uiCanvas);

		Canvas cStart = uiMenu.gameObject.GetComponent<Canvas>();
		cStart.renderMode = RenderMode.WorldSpace;
		cStart.worldCamera = worldCamera;

		uiMenu.AddLabel("�м� �����̳�");
		uiMenu.AddDivider();
		uiMenu.AddButton("��� ����", ButtonTerms);
		uiMenu.AddButton("Ʃ�丮��", ButtonTutorial);
		uiMenu.AddButton("�׽�Ʈ", ButtonTest);
		uiMenu.AddButton("������", ButtonExit);
//		uiMenu.Show();

		uiTerm = Instantiate<UIBuilder>(uiCanvas);

		Canvas cTerm = uiTerm.gameObject.GetComponent<Canvas>();
		cTerm.renderMode = RenderMode.WorldSpace;
		cTerm.worldCamera = worldCamera;

		uiTerm.SetPaneWidth(UIBuilder.PANE_CENTER, 950);
		uiTerm.AddLabel("��� ����");
		uiTerm.AddDivider();
		uiTerm.AddLabel("�׸�\t\t����", TextAnchor.MiddleLeft);
		uiTerm.AddLabel("�ϼ���\t\t������ �ϼ���\n" +
			"�ȳ���\t\t�ϼ����� �׸��� ���� ������\n" +
			"�ȴܼ�\t\t�ȴ� ������ ǥ���ϱ� ���� ��\n" +
			"������\t\t�������� �ؾ����� ��Ÿ���� ǥ��\n" +
			"���Ӽ�\t\t���� �� ǥ��\n" +
			"��Ƽġ��\t��Ƽġ�� ���� �κа� ���κ��� ��Ÿ����.\n" +
			"��м�\t\t����� ǥ���ϸ� ��ȣ�� ���̴� ��쵵 ����", TextAnchor.MiddleLeft);
		uiTerm.AddButton("�޴���", ButtonMenu);

		uiTerm.Show();
		//		StartCoroutine(AlignUI());
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
	public void ButtonTerms()
    {
		uiMenu.Hide();
		uiTerm.Show();
    }

	public void ButtonTutorial()
	{
		Debug.Log("Ʃ�丮��");
	}

	public void ButtonTest()
	{
		Debug.Log("�׽�Ʈ");
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
