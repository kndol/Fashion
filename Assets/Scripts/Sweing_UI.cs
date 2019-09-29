using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

[Serializable]
public class Sweing_Parts
{
    [Tooltip("재봉용 파츠 지정")]
    public GameObject[] sweingParts = null;
    [Tooltip("재봉 작업할 때 레이케스트를 감지하기 위한 콜라이더 객체들의 그룹(부모)객체")]
    public GameObject[] pointGroup;
}

public class Sweing_UI : FashionController
{
    [Header("의상 종류")]
    [Tooltip("작업에 사용할 의상의 종류 갯수와 파츠 지정")]
    [SerializeField]
    Sweing_Parts[] parts = null;

    RectTransform[] imageBtns;
    RectTransform btnOk;
    Rect imgBtnRect = new Rect(0, 0, 150, 100);

    List<bool>[] PointSelected;
    int[] countPointSelected;

    Fashion.UIManager.LaserPointer lp;

    bool hasLaser = false;

    UIBuilder uiSweing;

    void Start()
    {
        uiSweing = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiSweing.SetPosition(menuPosition);
        uiSweing.AddLabel("<B>티셔츠 봉제_ 몸판/소매 봉제</B>");
        uiSweing.AddDivider();
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                uiSweing.AddScrollView("티셔츠의 몸판과 소매의 앞과 뒤를 겉과 겉을 마주대고 몸판의\n" +
                    "어깨점과 소매의 S.P 와 몸판의 겨드랑점과 소매의 겨드랑점을 맞추어 재봉한다.\n\n" +
                    "점선을 따라 원단을 재봉하세요.");
                break;
            case ClothType.skirt:
                uiSweing.AddScrollView("티셔츠의 몸판과 소매의 앞과 뒤를 겉과 겉을 마주대고 몸판의\n" +//
                    "어깨점과 소매의 S.P 와 몸판의 겨드랑점과 소매의 겨드랑점을 맞추어 재봉한다.\n\n" +//
                    "점선을 따라 원단을 재봉하세요.");
                break;
        }
        uiSweing.Show();

        int i, j;
        for (i = 0; i < parts.Length; i++)
        {
            for (j = 1; j < parts[i].sweingParts.Length; j++)
            {
                parts[i].sweingParts[j].SetActive(false);
            }                                 
        }
        DoSweing();
    }

    public void DoSweing()
    {
        int clothType = (int)Data.clothType;
        int groupCount = parts[clothType].pointGroup.Length;
        PointSelected = new List<bool>[groupCount];
        countPointSelected = new int[groupCount];

        for (int group = 0; group < groupCount; group++)
        {
            int count = parts[clothType].pointGroup[group].transform.childCount;
            PointerOverHandler[] handlers = parts[clothType].pointGroup[group].GetComponentsInChildren<PointerOverHandler>();
            MeshRenderer[] rends = parts[clothType].pointGroup[group].GetComponentsInChildren<MeshRenderer>();
            PointSelected[group] = new List<bool>();
            countPointSelected[group] = 0;
            for (int id = 0; id < count; id++)
            {
                rends[id].enabled = false;
                handlers[id].group = group;
                handlers[id].id = id;
                handlers[id].OnPointerOver += OnOverHander;
                PointSelected[group].Add(false);
            }
        }
    }

    public void OnOverHander(int group, int id)
    {
        if (!PointSelected[group][id])
        {
            PointSelected[group][id] = true;
            ++countPointSelected[group];
            //if (countPointSelected[group] > parts[(int)Data.clothType].pointGroup[group].transform.childCount * 9 / 10)
            if (countPointSelected[group] > 3)   //테스트용
            {
                uiSweing.AddButton("재봉완료", OnTutorialEnd);
                int i, j;
                for (i = 0; i < parts.Length; i++)
                {
                    for (j = 0; j < parts[i].sweingParts.Length; j++)
                    {
                        if(i == 0 && j == 0)
                        {
                            parts[i].sweingParts[j].SetActive(false);
                        }
                        else parts[i].sweingParts[j].SetActive(true);
                    }
                }
            }
        }
    }

    void LateUpdate()
    {
        if (!hasLaser)
        {
            lp = FindObjectOfType<Fashion.UIManager.LaserPointer>();
            if (lp)
            {
                GetComponent<OVRRaycaster>().pointer = lp.gameObject;
                hasLaser = true;
            }
        }
    }

    public override void OnTutorialEnd()
    {
        base.OnTutorialEnd();
    }
}