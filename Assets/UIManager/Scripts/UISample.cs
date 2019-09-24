using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion.UIManager;

// Show off all the Debug UI components.
public class UISample : MonoBehaviour
{
    bool inMenu;
    private Text sliderText;
	private Text labelText;
	private Text labelTextLeft;
	private Text labelTextRight;
	[SerializeField]
	UIBuilder uiCanvasPrefab = null;
	[SerializeField]
	Sprite[] sprites;

	UIBuilder uIBuilder;

	void Start ()
    {
		uIBuilder = Instantiate<UIBuilder>(uiCanvasPrefab);

		uIBuilder.SetPaneWidth(880);
		uIBuilder.AddButton("��ư", LogButtonPressed);
        var labelPrefab = uIBuilder.AddLabel("���̺�");
        var sliderPrefab = uIBuilder.AddSlider("�����̴�", 1.0f, 10.0f, SliderPressed, true);
		labelText = labelPrefab.GetComponentInChildren<Text>();
		var textElementsInSlider = sliderPrefab.GetComponentsInChildren<Text>();
        Assert.AreEqual(textElementsInSlider.Length, 2, "Slider prefab format requires 2 text components (label + value)");
        sliderText = textElementsInSlider[1];
        Assert.IsNotNull(sliderText, "No text component on slider prefab");
        sliderText.text = sliderPrefab.GetComponentInChildren<Slider>().value.ToString();
        uIBuilder.AddDivider();
		uIBuilder.StartHorizontalSection(5);
		Rect rc = new Rect(0, 0, 200, 350);
		for (int i = 0; i < sprites.Length; i++)
			uIBuilder.AddImageButton(sprites[i], rc, delegate () { OnClickHor(i); });
		uIBuilder.EndHorizontalSection();
		uIBuilder.AddYesNoButtons("", "", OnYesNoClicked, 40);
		uIBuilder.AddDivider();
		uIBuilder.AddToggle("���", TogglePressed);
		uIBuilder.AddRadio("����1", "group", delegate(Toggle t) { RadioPressed("����1", "group", t); }) ;
		uIBuilder.AddRadio("����2", "group", delegate(Toggle t) { RadioPressed("����2", "group", t); }) ;

		//====================================================
		uIBuilder.AddLabel("������ ��", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
		uIBuilder.AddDivider(UIBuilder.PANE_RIGHT);
		uIBuilder.AddRadio("���̵� ���� 1", "group2", delegate(Toggle t) { RadioPressed("���̵� ���� 1", "group2", t); }, UIBuilder.PANE_RIGHT);
		uIBuilder.AddRadio("���̵� ���� 2", "group2", delegate(Toggle t) { RadioPressed("���̵� ���� 2", "group2", t); }, UIBuilder.PANE_RIGHT);
		uIBuilder.AddInputField("", "���� �־�", OnEndEdit, null, UIBuilder.PANE_RIGHT);//

		uIBuilder.AddInputNumberField(10, "���� �־�", OnEndEditNumber, OnEndEditNumber, UIBuilder.PANE_RIGHT);

		var labelPrefabRight = uIBuilder.AddLabel("������ ���̺�", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);//
		labelTextRight = labelPrefabRight.GetComponentInChildren<Text>();//

		//====================================================
		uIBuilder.AddLabel("���� ��", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
		uIBuilder.AddDivider(UIBuilder.PANE_LEFT);
		uIBuilder.AddButton("���� �г� ��ư", LeftButtonPressed, UIBuilder.PANE_LEFT);
		uIBuilder.AddYesNoButtons("", "", OnYesNoClicked, 30, UIBuilder.PANE_LEFT);
		uIBuilder.AddYesNoCancelButtons("", "", "", OnYesNoClicked, 30, UIBuilder.PANE_LEFT);
		var labelPrefabLeft = uIBuilder.AddLabel("���� ���̺�", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
		labelTextLeft = labelPrefabLeft.GetComponentInChildren<Text>();

		uIBuilder.Show();
        inMenu = true;
	}

	public void OnYesNoClicked(Reply reply)
	{
		switch(reply)
		{
			case Reply.Yes:
				labelTextLeft.text = "��";
				break;
			case Reply.No:
				labelTextLeft.text = "�ƴϿ�";
				break;
			default:
				labelTextLeft.text = "���";
				break;
		}

		StartCoroutine(ChangeLabel(false));
	}

	public void TogglePressed(Toggle t)
    {
        Debug.Log("Toggle pressed. Is on? "+t.isOn);
    }

	public void RadioPressed(string radioLabel, string group, Toggle t)
    {
        Debug.Log("Radio value changed: "+radioLabel+", from group "+group+". New value: "+t.isOn);
    }
	public void RadioPressed(int grp_idx, int idx, Toggle t)
	{
		Debug.Log("Radio value changed: �׷�"+grp_idx + " ��ư" + idx + ", ��: " + t.isOn);
	}

	public void SliderPressed(float f)
    {
        Debug.Log("Slider: " + f);
        sliderText.text = f.ToString();
    }

    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (inMenu) uIBuilder.Hide();
            else uIBuilder.Show();
            inMenu = !inMenu;
        }
    }

    void LogButtonPressed()
    {
		//Debug.Log("Button pressed");
		labelText.text = "��ư ����";
		StartCoroutine(ChangeLabel(true));
    }

	IEnumerator ChangeLabel(bool isCenter)
	{
		yield return new WaitForSeconds(2);
		if (isCenter)
			labelText.text = "���̺�";
		else
			labelTextLeft.text = "���� ���̺�";
	}

	void LeftButtonPressed()
	{
		labelTextLeft.text = "���� ��ư ����";
		StartCoroutine(ChangeLabel(false));
	}

	void OnEndEdit(string s)
	{
		labelTextRight.text = s;
	}

	void OnEndEditNumber(int i)
	{
		labelTextRight.text = i.ToString();
	}

	public void OnClickHor(int num)
	{

	}
}
