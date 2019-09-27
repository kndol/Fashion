﻿using System;
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

public class SetQuestion
{
    public Sprite sprite { get; set; }
    public bool rightCheck { get; set; }
    public SetQuestion(Sprite sprite, bool rightCheck)
    {
        this.sprite = sprite;
        this.rightCheck = rightCheck;
    }
}

public class bodyQ1
{
    
}

//3지선다
//몸판3문제   소매2문제   티셔츠5 + a문제
public class Test_Mode_UI : FashionController
{
    [SerializeField]
    Sprite[] body_questionSprite = null;  //

    UIBuilder uiTest;
    UIBuilder uiYesNo;

    List<SetQuestion> setQuestion = new List<SetQuestion>();
    //List<int> bodyQuestion_rand = new List<int>() { 1, 2, 3 };   //몸판문제
    //List<int> sleeveQuestion_rand = new List<int>() { 1, 2 };
    //List<int> example_rand = new List<int> { 1, 2, 3 };

    //int i = 0;
    //bool rightCheck = false;
    //bool randgetCheck = false;
    Sprite[] bodySprite;

	private void Start()
	{
        uiTest = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiYesNo = Instantiate<UIBuilder>(uiCanvasPrefab);

        setQuestion.Add(new SetQuestion(bodySprite[0], false));
        setQuestion.Add(new SetQuestion(bodySprite[1], false));
        setQuestion.Add(new SetQuestion(bodySprite[2], true));


        //Question_Show();
    }

    void BodyQuestion()   //몸판 3문제
    {
        uiTest.AddLabel("문제");
        uiTest.AddDivider();
        uiTest.StartHorizontalSection(10);
        //uiTest.AddImageButton(body_questionSprite[0], );
        //uiTest.AddImageButton(body_questionSprite[1], );
        //uiTest.AddImageButton(body_questionSprite[2], );
        //uiTest.EndHorizontalSection();
    }
    /*public void Question_Show()
    {
        if (!randgetCheck)
        {
            Rand_List.Rand_Get(bodyQuestion_rand);
            randgetCheck = true;
        }
        
        if (i > bodyQuestion_rand.Count - 1) End_Test();

        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                break;
            case ClothType.shirts:
                break;
            case ClothType.pants:
                break;
            case ClothType.skirt:
                break;
            case ClothType.body:      //몸판문제
                Body_Question(i);
                break;
            case ClothType.sleeve:    //소매 문제
                Sleeve_Question(i);
                break;

                if (!randgetCheck)
                {
                    Rand_List.Rand_Get(bodyQuestion_rand);
                    randgetCheck = true;
                }

                if (i > bodyQuestion_rand.Count - 1) End_Test();

                switch (Data.CS)
                {
                    case Cloth_State.t_shirts:
                        break;
                    case Cloth_State.shirts:
                        break;
                    case Cloth_State.pants:
                        break;
                    case Cloth_State.skirt:
                        break;
                    case Cloth_State.Body:      //몸판문제
                        Body_Question(i);
                        break;
                    case Cloth_State.Sleeve:    //소매 문제
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
    }*/
}
