using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;
using UnityEngine.SceneManagement;

public class Rand_List   //난수생성
{
    public static void Rand_Get<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randValue = UnityEngine.Random.Range(0, list.Count);
            T temp;
            temp = list[i];
            list[i] = list[randValue];
            list[randValue] = temp;
        }
    }
}

[Serializable]
public class Question
{
    [SerializeField]
    public Sprite[] example;
}

public class Test_Mode_UI : FashionController
{
    [SerializeField]
    Sprite[] body_questionSprite = null;
    [SerializeField]
    Question[] bodyQuestion = null;

    UIBuilder uiTest;
    UIBuilder uiYesNo;


    List<int> bodyQuestion_rand = new List<int>() { 1, 2, 3 };   //몸판문제
    List<int> sleeveQuestion_rand = new List<int>() { 1, 2 };
    List<int> example_rand = new List<int> { 1, 2, 3 };
    //Question bodyQuest = new Question();

    int i = 0;
    bool rightCheck = false;
    bool randgetCheck = false;

    public override void StartTutorial()
    {
        base.StartTutorial();

        uiTest = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiYesNo = Instantiate<UIBuilder>(uiCanvasPrefab);

        Question_Show();
    }

    public void Question_Show()
    {
        if (!randgetCheck)
        {
            Rand_List.Rand_Get(bodyQuestion_rand);
            randgetCheck = true;
        }
        
        if (i > bodyQuestion_rand.Count - 1) End_Test();

        switch (Data.CS)
        {
            case ClothType.t_shirts:
                break;
            case ClothType.shirts:
                break;
            case ClothType.pants:
                break;
            case ClothType.skirt:
                break;
            case Cloth_State.Body:      //몸판문제
                Body_Question(i);
                break;
            case ClothType.Sleeve:    //소매 문제
                Sleeve_Question(i);
                break;
        }
        i++;
        uiTest.Show();
    }

    public void Body_Question(int num)   //몸판문제
    {
        Rand_List.Rand_Get(example_rand);
        switch (bodyQuestion_rand[num])
        {
            case 1:
                uiTest.AddLabel("문제");
                uiTest.AddDivider();
                uiTest.StartHorizontalSection(10);               
                for (int i = 0; i < example_rand.Count; i++)
                {
                    var bodyQ1 = uiTest.AddImageButton(bodyQuestion[bodyQuestion_rand[num]].example[example_rand[i]], Wrong_Answer);
                    bodyQuestion[bodyQuestion_rand[num]].example[i] = bodyQ1.GetComponent<Sprite>();
                }
                break;
            case 2:
                uiTest.AddLabel("문제");
                uiTest.AddDivider();
                uiTest.StartHorizontalSection(10);
                for (int i = 0; i < example_rand.Count; i++)
                {
                    var bodyQ1 = uiTest.AddImageButton(bodyQuestion[bodyQuestion_rand[num]].example[example_rand[i]], Wrong_Answer);
                    bodyQuestion[bodyQuestion_rand[num]].example[i] = bodyQ1.GetComponent<Sprite>();
                }
                break;
            case 3:
                uiTest.AddLabel("문제");
                uiTest.AddDivider();
                uiTest.StartHorizontalSection(10);
                for (int i = 0; i < example_rand.Count; i++)
                {
                    var bodyQ1 = uiTest.AddImageButton(bodyQuestion[bodyQuestion_rand[num]].example[example_rand[i]], Wrong_Answer);
                    bodyQuestion[bodyQuestion_rand[num]].example[i] = bodyQ1.GetComponent<Sprite>();
                }
                break;
        }
        uiTest.EndHorizontalSection();
    }

    public void Sleeve_Question(int num)    //소매문제
    {
        switch (bodyQuestion_rand[num])
        {
            case 1:
                uiTest.AddLabel("문제");
                uiTest.AddDivider();
                uiTest.StartHorizontalSection(10);
                for (int i = 0; i < example_rand.Count; i++)
                {
                    var bodyQ1 = uiTest.AddImageButton(bodyQuestion[bodyQuestion_rand[num]].example[example_rand[i]], Wrong_Answer);
                    bodyQuestion[bodyQuestion_rand[num]].example[i] = bodyQ1.GetComponent<Sprite>();
                }
                break;
            case 2:
                uiTest.AddLabel("문제");
                uiTest.AddDivider();
                uiTest.StartHorizontalSection(10);
                for (int i = 0; i < example_rand.Count; i++)
                {
                    var bodyQ1 = uiTest.AddImageButton(bodyQuestion[bodyQuestion_rand[num]].example[example_rand[i]], Wrong_Answer);
                    bodyQuestion[bodyQuestion_rand[num]].example[i] = bodyQ1.GetComponent<Sprite>();
                }
                break;
        }
        uiTest.EndHorizontalSection();
    }

    public void Answer()   //정답
    {
        for(int i = 0; i < bodyQuestion.Length; i++)
        {
            for(int j = 0; j < bodyQuestion[i].example.Length; j++)
            {
                //if(bodyQuestion[i].example[j] == )
            }
        }

        uiTest.Hide();
        uiYesNo.Show();
    }

    public void Wrong_Answer()   //오답
    {
        uiTest.Hide();
        uiYesNo.Show();
        rightCheck = true;
    }

    public void YesNo_Button(Reply reply)
    {
        switch (reply)
        {
            case Reply.Yes:
                if (rightCheck) Data.Score -= 5;
                rightCheck = false;
                uiYesNo.Hide();
                Destroy(uiTest.gameObject);
                uiTest = Instantiate<UIBuilder>(uiCanvasPrefab);
                Question_Show();
                break;
            case Reply.No:
                uiYesNo.Hide();
                uiTest.Show();
                break;
        }
    }

    public void End_Test()
    {
        //시험 종료
    }

    public override void OnTutorialEnd()
    {
        base.OnTutorialEnd();
    }
}
