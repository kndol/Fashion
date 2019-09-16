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
	[SerializeField]
	UIBuilder uiCanvasPrefab = null;
	[SerializeField]
	Sprite[] sprites;

	UIBuilder uIBuilder;

	void Start ()
    {
		uIBuilder = Instantiate<UIBuilder>(uiCanvasPrefab);

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
        uIBuilder.AddToggle("토글", TogglePressed);
		uIBuilder.AddRadio("라디오1", "group", delegate(Toggle t) { RadioPressed("라디오1", "group", t); }) ;
		uIBuilder.AddRadio("라디오2", "group", delegate(Toggle t) { RadioPressed("라디오2", "group", t); }) ;

		uIBuilder.StartHorizontalSection(5);
		for (int i = 0; i < sprites.Length; i++)
			uIBuilder.AddImageButton(sprites[i], new Rect(0, 0, 100, 200), delegate () { OnClickHor(i); });
		uIBuilder.EndHorizontalSection();

		//====================================================
		uIBuilder.AddLabel("오른쪽 탭", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
		uIBuilder.AddDivider(UIBuilder.PANE_RIGHT);
		uIBuilder.AddRadio("사이드 라디오 1", "group2", delegate(Toggle t) { RadioPressed("사이드 라디오 1", "group2", t); }, UIBuilder.PANE_RIGHT);
		uIBuilder.AddRadio("사이드 라디오 2", "group2", delegate(Toggle t) { RadioPressed("사이드 라디오 2", "group2", t); }, UIBuilder.PANE_RIGHT);
		//====================================================
		uIBuilder.AddLabel("왼쪽 탭", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
		uIBuilder.AddDivider(UIBuilder.PANE_LEFT);
		uIBuilder.AddButton("왼쪽 패널 버튼", LeftButtonPressed, UIBuilder.PANE_LEFT);
		var labelPrefabLeft = uIBuilder.AddLabel("왼쪽 레이블", TextAnchor.MiddleCenter, UIBuilder.PANE_LEFT);
		labelTextLeft = labelPrefabLeft.GetComponentInChildren<Text>();

		uIBuilder.Show();
        inMenu = true;
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

	public void OnClickHor(int num)
	{

	}
}
