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
		uIBuilder.AddButton("버튼", LogButtonPressed);
        var labelPrefab = uIBuilder.AddLabel("레이블");
        var sliderPrefab = uIBuilder.AddSlider("슬라이더", 1.0f, 10.0f, SliderPressed, true);
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
		uIBuilder.AddToggle("토글", TogglePressed);
		uIBuilder.AddRadio("라디오1", "group", delegate(Toggle t) { RadioPressed("라디오1", "group", t); }) ;
		uIBuilder.AddRadio("라디오2", "group", delegate(Toggle t) { RadioPressed("라디오2", "group", t); }) ;

		//====================================================
		uIBuilder.AddLabel("오른쪽 탭", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
		uIBuilder.AddDivider(UIBuilder.PANE_RIGHT);
		uIBuilder.AddRadio("사이드 라디오 1", "group2", delegate(Toggle t) { RadioPressed("사이드 라디오 1", "group2", t); }, UIBuilder.PANE_RIGHT);
		uIBuilder.AddRadio("사이드 라디오 2", "group2", delegate(Toggle t) { RadioPressed("사이드 라디오 2", "group2", t); }, UIBuilder.PANE_RIGHT);
		uIBuilder.AddInputField("", "문자 넣어", OnEndEdit, null, UIBuilder.PANE_RIGHT);//

		uIBuilder.AddInputNumberField(10, "숫자 넣어", OnEndEditNumber, OnEndEditNumber, UIBuilder.PANE_RIGHT);

		var labelPrefabRight = uIBuilder.AddLabel("오른쪽 레이블", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);//
		labelTextRight = labelPrefabRight.GetComponentInChildren<Text>();//

		//====================================================
		uIBuilder.AddLabel("왼쪽 탭", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
		uIBuilder.AddDivider(UIBuilder.PANE_LEFT);
		uIBuilder.AddButton("왼쪽 패널 버튼", LeftButtonPressed, UIBuilder.PANE_LEFT);
		uIBuilder.AddYesNoButtons("", "", OnYesNoClicked, 30, UIBuilder.PANE_LEFT);
		uIBuilder.AddYesNoCancelButtons("", "", "", OnYesNoClicked, 30, UIBuilder.PANE_LEFT);
		var labelPrefabLeft = uIBuilder.AddLabel("왼쪽 레이블", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
		labelTextLeft = labelPrefabLeft.GetComponentInChildren<Text>();

		uIBuilder.Show();
        inMenu = true;
	}

	public void OnYesNoClicked(Reply reply)
	{
		switch(reply)
		{
			case Reply.Yes:
				labelTextLeft.text = "예";
				break;
			case Reply.No:
				labelTextLeft.text = "아니오";
				break;
			default:
				labelTextLeft.text = "취소";
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
		Debug.Log("Radio value changed: 그룹"+grp_idx + " 버튼" + idx + ", 값: " + t.isOn);
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
		labelText.text = "버튼 누름";
		StartCoroutine(ChangeLabel(true));
    }

	IEnumerator ChangeLabel(bool isCenter)
	{
		yield return new WaitForSeconds(2);
		if (isCenter)
			labelText.text = "레이블";
		else
			labelTextLeft.text = "왼쪽 레이블";
	}

	void LeftButtonPressed()
	{
		labelTextLeft.text = "왼쪽 버튼 누름";
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
