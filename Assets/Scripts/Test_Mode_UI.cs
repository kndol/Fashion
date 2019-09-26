using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;
using UnityEngine.SceneManagement;

public class Rand_List
{
    public static void Rand_Get<T>(List<T> list)
    {
        for(int i = 0; i < list.Count; i++)
        {           
            int randValue = UnityEngine.Random.Range(0, list.Count);
            T temp;
            temp = list[i];
            list[i] = list[randValue];
            list[randValue] = temp;
        }
    }
}

public class Test_Mode_UI : MonoBehaviour
{
    [SerializeField]
    UIBuilder uiCanvasPrefab = null;
    [SerializeField]
    Transform player = null;
    [SerializeField]
    Sprite[] body_questionSprite = null;

    UIBuilder uiTest;
    UIBuilder uiYesNo;

    List<int> bodyQuestion = new List<int>() { 1, 2, 3 };
    List<int> sleeveQues = new List<int>() { 1, 2, 3 };

    int i = 0;
    bool rightCheck = false;
    bool randgetCheck = false;


    void Start()
    {
        uiTest = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiYesNo = Instantiate<UIBuilder>(uiCanvasPrefab);
    }

    public void Question_Show()
    {
        if (!randgetCheck)
        {
            Rand_List.Rand_Get(bodyQuestion);
            randgetCheck = true;
        }
        if (i > bodyQuestion.Count - 1) End_Test();

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
            case ClothType.Body:   //몸판문제
                Body_Question(i);
                break;
            case ClothType.Sleeve:    //소매 문제
                Sleeve_Question(i);
                break;
        }
        i++;
        uiTest.Show();
    }

    public void Body_Question(int num)
    {   
        switch (bodyQuestion[num])
        {
            case 1:
                uiTest.AddLabel("문제");
                uiTest.AddDivider();
                uiTest.StartHorizontalSection(10);
                uiTest.AddImageButton(body_questionSprite[0], Wrong_Answer);
                uiTest.AddImageButton(body_questionSprite[1], Wrong_Answer);
                uiTest.AddImageButton(body_questionSprite[2], Right_Answer);
                break;
            case 2:
                uiTest.AddLabel("문제");
                uiTest.AddDivider();
                uiTest.StartHorizontalSection(10);
                uiTest.AddImageButton(body_questionSprite[0], Wrong_Answer);
                uiTest.AddImageButton(body_questionSprite[1], Wrong_Answer);
                uiTest.AddImageButton(body_questionSprite[2], Right_Answer);
                break;
            case 3:
                uiTest.AddLabel("문제");
                uiTest.AddDivider();
                uiTest.StartHorizontalSection(10);
                uiTest.AddImageButton(body_questionSprite[0], Wrong_Answer);
                uiTest.AddImageButton(body_questionSprite[1], Wrong_Answer);
                uiTest.AddImageButton(body_questionSprite[2], Right_Answer);
                break;
        }
        uiTest.EndHorizontalSection();
    }

    public void Sleeve_Question(int num)
    {

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

    public void Right_Answer()   //정답
    {
        uiTest.Hide();
        uiYesNo.Show();
    }

    public void Wrong_Answer()   //오답
    {
        uiTest.Hide();
        uiYesNo.Show();
        rightCheck = true;
    }

    public void End_Test()
    {
        //시험 종료
    }
}
