using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;
using UnityEngine.SceneManagement;

public class Test_Mode_UI : FashionController
{
	const string PATH = "examination/";

	UIBuilder uiTest;

	public class QuestionSet
	{
		public string question;
		public List<string> exams = new List<string>();
	}
	List<QuestionSet> questions = new List<QuestionSet>();
	bool[] answer;
	int selectedNum;

	private void Start()
	{
		LoadExams();
		Solve(0);
	}

	void LoadExams()
	{
		TextAsset textAsset = Resources.Load<TextAsset>(PATH + "examination");
		string[] lines = textAsset.text.Split(Environment.NewLine.ToCharArray(),
			StringSplitOptions.RemoveEmptyEntries);
		QuestionSet quest = null;
		foreach (string line in lines)
		{
			if (!line.StartsWith("- "))
			{
				// 문제
				if (quest != null)
					questions.Add(quest);
				quest = new QuestionSet();
				quest.exams = new List<string>();
				quest.question = line;
			}
			else
			{
				// 보기
				quest.exams.Add(line.Substring(2));
			}
		}
		if (quest != null)
			questions.Add(quest);
		answer = new bool[questions.Count];
	}

	public void Shuffle(List<string> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			int randValue = UnityEngine.Random.Range(0, list.Count);
			string temp;
			temp = list[i];
			list[i] = list[randValue];
			list[randValue] = temp;
		}
	}

	void Solve(int examNum)
	{
		QuestionSet quest = questions[examNum];
		selectedNum = -1;

		// 보기 섞기
		Shuffle(quest.exams);

		if (uiTest) Destroy(uiTest.gameObject);

		uiTest = Instantiate<UIBuilder>(uiCanvasPrefab);
		uiTest.SetPosition(menuPosition);
		uiTest.SetPaneWidth(800);
		uiTest.AddLabel("<B>문제 #" + (examNum + 1) + "</B>");
		uiTest.AddDivider();
		uiTest.AddLabel(questions[examNum].question);
		uiTest.StartHorizontalSection(5);
		foreach (string exam in quest.exams)
		{
			var sprite = Resources.Load<Sprite>(PATH + exam);
			uiTest.AddImage(sprite);
		}
		uiTest.EndHorizontalSection();
		uiTest.StartHorizontalSection(5);
		for (int i = 0; i< quest.exams.Count; i++)
		{
			int sel = i;
			uiTest.AddRadio((i + 1).ToString(), "quest" + examNum, delegate { selectedNum = sel; });
		}
		uiTest.EndHorizontalSection();
		uiTest.StartHorizontalSection(30);
		RectTransform btn = uiTest.AddButton("이전 문제", delegate { Solve(examNum - 1); });
		if (examNum == 0)
		{
			btn.GetComponent<Button>().interactable = false;
		}
		if (examNum == questions.Count - 1)
		{
			// 마지막 문제
			uiTest.AddButton("정답 제출", SubmitAnswer);
		}
		else
		{
			uiTest.AddButton("다음 문제", delegate
			{
				answer[examNum] = quest.exams[selectedNum][6] == 'O';
				Solve(examNum + 1);
			});
		}
		uiTest.EndHorizontalSection();
		uiTest.Show();
	}

	void SubmitAnswer()
	{
		int score = 100;
		int subtract = 100 / questions.Count;
		foreach(bool ans in answer)
		{
			if (!ans) score -= subtract;
		}
		Destroy(uiTest.gameObject);
		uiTest = Instantiate<UIBuilder>(uiCanvasPrefab);
		uiTest.AddLabel("<B>" + /*Data.UserName*/"회원" + "님의 점수는</B>");
		uiTest.AddDivider();
		uiTest.AddLabel(score + "/100 점입니다.");
		uiTest.AddButton("확인", OnTutorialEnd);
		uiTest.Show();
	}
}
