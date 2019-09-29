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
		uiTest = Instantiate<UIBuilder>(uiCanvasPrefab);
		uiTest.SetPosition(menuPosition);
		uiTest.AddLabel("<color=darkgreen>시험 문제를 출제 중입니다.</color>");
		uiTest.Show();

		TextAsset textAsset = Resources.Load<TextAsset>(PATH + "examination");
		string[] lines = textAsset.text.Split(Environment.NewLine.ToCharArray(),
			StringSplitOptions.RemoveEmptyEntries);
		QuestionSet quest = null;
		foreach (string line in lines)
		{
			print(line);
			if (!line.StartsWith("- "))
			{
				// 문제
				if (quest != null)
					questions.Add(quest);
				quest = new QuestionSet();
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
		print(questions);
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
		selectedNum = 0;

		print((examNum + 1) + "번 문제");
		// 보기 섞기
		Shuffle(quest.exams);
		print(quest.exams);

		if (uiTest)
		{
			print("uiTest 제거");
			Destroy(uiTest.gameObject);
		}

		print("uiTest 생성");
		uiTest = Instantiate<UIBuilder>(uiCanvasPrefab);
		uiTest.SetPosition(menuPosition);
		uiTest.SetPaneWidth(800);
		uiTest.AddLabel("<B>문제 #" + (examNum + 1) + "</B>");
		uiTest.AddDivider();
		uiTest.AddLabel(quest.question);
		print(quest.question);
		uiTest.StartHorizontalSection(5);
		foreach (string exam in quest.exams)
		{
			print("이미지 " + exam);
			Sprite sprite = Resources.Load<Sprite>(PATH + exam);
			print("로딩 " + PATH + exam);
			uiTest.AddImage(sprite);
			print("추가 완료");
		}
		uiTest.EndHorizontalSection();
		print("이미지 끝");
		uiTest.StartHorizontalSection(5);
		for (int i = 0; i< quest.exams.Count; i++)
		{
			int sel = i;
			uiTest.AddRadio((i + 1).ToString(), "quest" + examNum, delegate { selectedNum = sel; });
		}
		uiTest.EndHorizontalSection();
		print("라디오버튼 끝");
		uiTest.StartHorizontalSection(30);
		RectTransform btn = uiTest.AddButton("이전 문제", delegate { Solve(examNum - 1); });
		if (examNum == 0)
		{
			btn.GetComponent<Button>().interactable = false;
		}
		if (examNum == questions.Count - 1)
		{
			// 마지막 문제
			uiTest.AddButton("정답 제출", delegate 
			{
				answer[examNum] = quest.exams[selectedNum][4] == 'O';
				SubmitAnswer();
			});
		}
		else
		{
			uiTest.AddButton("다음 문제", delegate
			{
				answer[examNum] = quest.exams[selectedNum][4] == 'O';
				Solve(examNum + 1);
			});
		}
		uiTest.EndHorizontalSection();
		print("버튼 끝");
		uiTest.Show();
		print("uiTest.Show()");
	}

	void SubmitAnswer()
	{
		int score = 100;
		int subtract = 100 / questions.Count;
		foreach(bool ans in answer)
		{
			if (!ans) score -= subtract;
		}
		Data.Score = score;

		Destroy(uiTest.gameObject);
		uiTest = Instantiate<UIBuilder>(uiCanvasPrefab);
		uiTest.SetPosition(menuPosition);
		uiTest.AddLabel("<B>이름을 입력해 주세요.</B>");
		uiTest.AddDivider();
		uiTest.AddInputField("", "이름", InputName, InputName);
		uiTest.AddButton("확인", showScore);
		uiTest.Show();
	}

	void InputName(string name) {
		Data.UserName = name;
	}

	void showScore()
	{
		// 이름을 입력하지 않았으면 [확인] 버튼 무시
		if (string.IsNullOrEmpty(Data.UserName))
			return;

		Destroy(uiTest.gameObject);

		uiTest = Instantiate<UIBuilder>(uiCanvasPrefab);
		uiTest.SetPosition(menuPosition);
		uiTest.AddLabel("<B>" + Data.UserName + " 님의 점수는</B>");
		uiTest.AddDivider();
		uiTest.AddLabel(Data.Score + "/100 점입니다.");
		uiTest.AddButton("확인", OnTutorialEnd);
		uiTest.Show();
	}
}
