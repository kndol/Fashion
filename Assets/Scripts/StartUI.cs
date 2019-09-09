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
    Transform player = null;
    [SerializeField]
    Transform selectPos = null;
    [SerializeField]
    Transform originPos = null;

	UIBuilder uiMenu;
	UIBuilder uiTerm;

	void Start ()
    {
		uiMenu = Instantiate<UIBuilder>(uiCanvasPrefab);

		uiMenu.AddLabel("<B>�м� �����̳�</B>");
		uiMenu.AddDivider();
		uiMenu.AddButton("��� ����", ButtonTerms);
		uiMenu.AddButton("Ʃ�丮��", ButtonTutorial);
		uiMenu.AddButton("�׽�Ʈ", ButtonTest);
		uiMenu.AddButton("������", ButtonExit);
		uiMenu.Show();

		uiTerm = Instantiate<UIBuilder>(uiCanvasPrefab);

		uiTerm.SetPaneWidth(980);
		uiTerm.AddLabel("<b>��� ����</b>");
		uiTerm.AddDivider();
		uiTerm.AddLabel("�׸�\t\t����", TextAnchor.MiddleLeft);
		uiTerm.AddScrollView("<b>�ϼ���</b>\t\t������ �ϼ���\n" +
			"<b>�ȳ���</b>\t\t�ϼ����� �׸��� ���� ������\n" +
			"<b>�ȴܼ�</b>\t\t�ȴ� ������ ǥ���ϱ� ���� ��\n" +
			"<b>������</b>\t\t�������� �ؾ����� ��Ÿ���� ǥ��\n" +
			"<b>���Ӽ�</b>\t\t���� �� ǥ��\n" +
			"<b>��Ƽġ��</b>\t��Ƽġ�� ���� �κа� ���κ��� ��Ÿ����.\n" +
			"<b>��м�</b>\t\t����� ǥ���ϸ� ��ȣ�� ���̴� ��쵵 ����", TextAnchor.UpperLeft, 320);
		uiTerm.AddButton("�޴���", ButtonMenu);
        //uiTerm.Show();

        uiSelect = Instantiate<UIBuilder>(uiCanvas);

        uiSelect.AddLabel("�������� �����ϼ���.");
        uiSelect.AddDivider();
        uiSelect.AddButton("Ȯ��", SelectButton);
        //uiSelect.AddButton("Ȯ��", ButtonTutorial);

        //uiSelect.Show();
        uiOrigin = Instantiate<UIBuilder>(uiCanvas);

        uiOrigin.AddLabel("���� ���� ����");
        uiOrigin.AddDivider();
        //uiOrigin.AddButton("Ȯ��", SelectButton);
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
