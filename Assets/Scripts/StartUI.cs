using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

// Show off all the Debug UI components.
public class StartUI : FashionController
{
    Design_Select_UI design_selectUI;
    UIBuilder uiMenu;
	UIBuilder uiTerm;

    public override void Start ()
    {
        StartTutorial();
    }

    public override void StartTutorial()
    {
        base.StartTutorial();

        Init_Data();
        uiMenu = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiMenu.AddLabel("�޴�");
        uiMenu.AddDivider();
        uiMenu.AddButton("����", ButtonTerms);
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

	public void ButtonTutorial()    //Ʃ�丮�� ��ư
	{
        uiMenu.Hide();
        Data.PM = Play_Mode.tutorial;
        OnTutorialEnd();

    }

	public void ButtonTest()        //�׽�Ʈ ��ư
	{
        uiMenu.Hide();
        Data.PM = Play_Mode.test;
        OnTutorialEnd();
    }

	public void ButtonExit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

    public override void OnTutorialEnd()
    {
        base.OnTutorialEnd();
    }

    public void Init_Data()    //�ʱ�ȭ �Լ�
    {
        Data.Score = 100;
        Data.PM = Play_Mode.start;
        Data.CS = ClothType.t_shirts;
        Data.MS = Making_State.start;
    }
}
