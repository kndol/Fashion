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

		uiMenu.AddLabel("�м� �����̳�");
		uiMenu.AddDivider();
		uiMenu.AddButton("��� ����", ButtonTerms);
		uiMenu.AddButton("Ʃ�丮��", ButtonTutorial);
		uiMenu.AddButton("�׽�Ʈ", ButtonTest);
		uiMenu.AddButton("������", ButtonExit);
		uiMenu.Show();

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

        //uiTerm.Show();
        //		StartCoroutine(AlignUI());
        uiSelect = Instantiate<UIBuilder>(uiCanvas);

        Canvas cSelect = uiSelect.gameObject.GetComponent<Canvas>();
        cSelect.renderMode = RenderMode.WorldSpace;
        cSelect.worldCamera = worldCamera;

        uiSelect.AddLabel("�������� �����ϼ���.");
        uiSelect.AddDivider();
        uiSelect.AddButton("Ȯ��", SelectButton);
        //uiSelect.AddButton("Ȯ��", ButtonTutorial);

        //uiSelect.Show();
        uiOrigin = Instantiate<UIBuilder>(uiCanvas);

        Canvas cOrigin = uiOrigin.gameObject.GetComponent<Canvas>();
        cOrigin.renderMode = RenderMode.WorldSpace;
        cOrigin.worldCamera = worldCamera;

        uiOrigin.AddLabel("���� ���� ����");
        uiOrigin.AddDivider();
        //uiOrigin.AddButton("Ȯ��", SelectButton);
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
	public void ButtonTerms()     //����
    {
		uiMenu.Hide();
		uiTerm.Show();
    }

	public void ButtonTutorial()  //Ʃ�丮��
	{
        player.transform.position = selectPos.transform.position;
        player.transform.rotation = selectPos.transform.rotation;
        uiMenu.Hide();
        uiSelect.Show();
        Debug.Log("Ʃ�丮��");
	}

	public void ButtonTest()      //�׽�Ʈ
	{
        player.transform.position = selectPos.transform.position;
        player.transform.rotation = selectPos.transform.rotation;
        uiMenu.Hide();
        uiSelect.Show();
        Debug.Log("�׽�Ʈ");
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
