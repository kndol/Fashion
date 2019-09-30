using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;

[Serializable]
public class Parts
{
    [Header("배경 이미지")]
    [Tooltip("작업에 사용할 배경 이미지")]
    public GameObject[] background = null;
    [Tooltip("배열용 파츠 지정")]
    public GameObject[] arrangeParts = null;
    [Tooltip("점선 재단선 파츠 지정")]
    public GameObject[] dotParts = null;
    [Tooltip("실선 재단선 파츠 지정")]
    public GameObject[] cutParts = null;
    [Tooltip("재단 작업할 때 레이케스트를 감지하기 위한 콜라이더 객체들의 그룹(부모)객체")]
    public GameObject[] pointGroup;
}

public class Tutorial_Arrange_and_Cut : FashionController
{
    [Header("의상 종류")]
    [Tooltip("작업에 사용할 의상의 종류 갯수와 파츠 지정")]
    [SerializeField]
    Parts[] parts = null;

    UIBuilder uiMenu;
    UIBuilder uiMsg;

    RectTransform[] imageBtns;
    RectTransform btnOk;

    int counter = 0;
    List<bool>[] PointSelected;
    int curDrawLineItem = -1;
    int numDrawingCompleted = 0;

    Fashion.UIManager.LaserPointer lp;

    bool hasLaser = false;

    void Start()
    {
        int i, j;
        for (i = 0; i < parts.Length; i++)
        {
            for (j = 0; j < parts[i].arrangeParts.Length; j++)
            {
                InitImageFilled(parts[i].arrangeParts[j], Image.FillMethod.Vertical);
            }
            for (j = 0; j < parts[i].dotParts.Length; j++)
            {
                InitImageFilled(parts[i].dotParts[j], Image.FillMethod.Radial360);
            }
            for (j = 0; j < parts[i].cutParts.Length; j++)
            {
                InitImageFilled(parts[i].cutParts[j], Image.FillMethod.Radial360);
            }
            for (j = 0; j < parts[i].pointGroup.Length; j++)
            {
                parts[i].pointGroup[j].SetActive(false);
            }
            for(j = 0; j < parts[i].background.Length; j++)//
            {
                parts[i].background[j].SetActive(false);
            }
        }
        StartTutorial();
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

    /// <summary>
    /// 이미지를 Filled 형으로 초기화하고 게임오브젝트를 비활성화
    /// </summary>
    /// <param name="go">이미지를 포함하고 있는 게임오브젝트</param>
    void InitImageFilled(GameObject go, Image.FillMethod method)
    {
        Image img = go.GetComponent<Image>();

        img.type = Image.Type.Filled;
        img.fillMethod = method;
        switch (method)
        {
            case Image.FillMethod.Radial360:
                img.fillOrigin = (int)Image.Origin360.Top;
                img.fillClockwise = false;
                break;
            case Image.FillMethod.Vertical:
                img.fillOrigin = (int)Image.OriginVertical.Top;
                break;
        }
        img.fillAmount = 0;
        img.preserveAspect = true;

        go.SetActive(false);
    }

    IEnumerator FillImage(GameObject go, float duration)
    {
        yield return null;
        float t = 0;

        go.SetActive(true);
        Image img = go.GetComponent<Image>();
        img.fillAmount = 0;
        while (img.fillAmount < 1)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            img.fillAmount = t / duration;
        }
    }

    void StartTutorial()
    {
        // 일단 base.StartTutorial() 호출한 뒤에 작업 시작
        for (int i = 0; i < parts[(int)Data.clothType].background.Length; i++)//
        {
            parts[(int)Data.clothType].background[i].SetActive(true);
        }
        uiMenu = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiMenu.SetPosition(menuPosition);
        uiMenu.SetPaneWidth(900);
        uiMenu.AddLabel("<B>티셔츠 마름질 - 배치</B>");
        uiMenu.AddDivider();
        switch (Data.clothType)
        {
            case ClothType.t_shirts:
                uiMenu.AddScrollView("식서방향과 골선의 위치와 뱡향에 따라 배치\n\n" +
                "<b>*</b> <i>식서방향</i> : 원단 가장자리의 구멍이 뚫려있는 부분.원단이 제일 늘어나지 않는 방향.\n" +
            "			 식서방향에 맞추는 이유: 옷의 늘어짐과 뒤틀림이 최대한 없게 하기 위해서.\n" +
            "<b>*</b> 골선이라고 표시된 부분의 선을 중심축으로 하여 본을 바로 해서 올려 놓고 1장 그리고, 중심축에서 본을 넘기듯" +
            " 뒤집어서 좌, 우 대칭이 되도록 패턴을 한번 더 그려주는 것.\n" +
            "  원단이 접히는 위치에 배치를 하며 이때, 원단의 시접방향과도 맞아야 한다.\n" +
            "  원단의 시접방향과 다른 경우에는 옷이 늘어지며 틀어지는 경우가 있다.\n" +
            "<b>*</b> 배치할 때 시접량에 유의해서 위치를 옮긴다.\n" +
            "  시접량이 많아 원단의 끝에 닿아 면적이 좁거나, 시접량이 서로 맞물리는 경우에는 시접량이 부족하여 재봉 시 어려움이 있다.", TextAnchor.UpperLeft, 500);
                break;
                case ClothType.skirt:
                uiMenu.AddScrollView("식서방향과 골선의 위치와 뱡향에 따라 배치\n\n" +
            "<b>*</b> <i>식서방향</i> : 원단 가장자리의 구멍이 뚫려있는 부분.원단이 제일 늘어나지 않는 방향.\n" +
            "			 식서방향에 맞추는 이유: 옷의 늘어짐과 뒤틀림이 최대한 없게 하기 위해서.\n" +
            "<b>*</b> 골선이라고 표시된 부분의 선을 중심축으로 하여 본을 바로 해서 올려 놓고 1장 그리고, 중심축에서 본을 넘기듯" +
            " 뒤집어서 좌, 우 대칭이 되도록 패턴을 한번 더 그려주는 것.\n" +
            "  원단이 접히는 위치에 배치를 하며 이때, 원단의 시접방향과도 맞아야 한다.\n" +
            "  원단의 시접방향과 다른 경우에는 옷이 늘어지며 틀어지는 경우가 있다.\n" +
            "<b>*</b> 배치할 때 시접량에 유의해서 위치를 옮긴다.\n" +
            "  시접량이 많아 원단의 끝에 닿아 면적이 좁거나, 시접량이 서로 맞물리는 경우에는 시접량이 부족하여 재봉 시 어려움이 있다.", TextAnchor.UpperLeft, 500);
                break;
        }
        
        btnOk = uiMenu.AddButton("확인", DoArrange);
        uiMenu.Show();
    }

    public void DoArrange()
    {
        btnOk.GetComponent<Button>().interactable = false;
        imageBtns = new RectTransform[parts[(int)Data.clothType].arrangeParts.Length];
        counter = 0;

        uiMenu.Hide();
        uiMenu.SetPaneWidth(200, UIBuilder.PANE_RIGHT);
        uiMenu.AddLabel("배치", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
        uiMenu.AddDivider(UIBuilder.PANE_RIGHT);
        uiMenu.AddLabel("원단을 클릭하여 배치하세요.", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
        for (int i = 0; i < parts[(int)Data.clothType].arrangeParts.Length; i++)
        {
            int partNum = i;
            Sprite sprite = parts[(int)Data.clothType].arrangeParts[i].GetComponent<Image>().sprite;
            imageBtns[i] = uiMenu.AddImageButton(sprite, delegate { arrange(partNum); }, UIBuilder.PANE_RIGHT); // 배치하기
        }
        uiMenu.Show();
    }

    public void arrange(int partNum)
    {
        imageBtns[partNum].GetComponent<Button>().interactable = false;

        StartCoroutine(FillImage(parts[(int)Data.clothType].arrangeParts[partNum], 0.5f));
        if (++counter == parts[(int)Data.clothType].arrangeParts.Length)
            uiMenu.AddButton("다음으로", ShowCutDesc, UIBuilder.PANE_RIGHT);
    }

    public void ShowCutDesc()
    {
        Destroy(uiMenu.gameObject);
        uiMenu = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiMenu.SetPosition(menuPosition);
        uiMenu.SetPaneWidth(700);
        uiMenu.AddLabel("<B>티셔츠 마름질 - 시접 및 재단</B>");
        uiMenu.AddDivider();
        uiMenu.AddScrollView("<li><b>시접</b>: 패턴을 원단에 맞게 직선은 1.5cm, 곡선은 1cm, 소매산은 2cm, 밑단은 4cm 의 여유분을 준다.</li>\n" +
            "<li><b>마킹</b>: 시접의 모서리 부분은 최대한의 각을 살려 마킹.\n" +
            "마킹할때 다트, MP, 소매와 암홀의 맞춤 표시를 시접분량 안에서 직각으로 마킹을 한다.\n" +
            "골선을 마킹할 때 원단의 끝부분과 골선의 패턴 중심 부분을 맞추어 표시헤야 한다.\n" +
            "<li><b>재단</b>: 시접선을 따라 원단을 자른다.", TextAnchor.UpperLeft, 500);
        btnOk = uiMenu.AddButton("확인", DoCut);
        uiMenu.Show();

    }

    public void DoCut()
    {
        int clothType = (int)Data.clothType;
        int partsCount = parts[clothType].pointGroup.Length;

        btnOk.GetComponent<Button>().interactable = false;

        for (int parts = 0; parts < this.parts[clothType].dotParts.Length; parts++)
        {
            StartCoroutine(FillImage(this.parts[clothType].dotParts[parts], 1)); // 점선 이미지
            StartCoroutine(FillImage(this.parts[clothType].dotParts[parts].transform.GetChild(0).gameObject, 0.5f)); // 화살표
        }

        PointSelected = new List<bool>[partsCount];
        for (int parts = 0; parts < partsCount; parts++)
        {
            int count = this.parts[clothType].pointGroup[parts].transform.childCount;
            PointerOverHandler[] handlers = this.parts[clothType].pointGroup[parts].GetComponentsInChildren<PointerOverHandler>();
            PointSelected[parts] = new List<bool>();
            MeshRenderer[] rends = this.parts[clothType].pointGroup[parts].GetComponentsInChildren<MeshRenderer>();
            for (int idxPoint = 0; idxPoint < count; idxPoint++)
            {
                //rends[idxPoint].enabled = false;
                int gr = parts;
                int id = idxPoint;
                handlers[idxPoint].group = gr;
                handlers[idxPoint].id = id;
                handlers[idxPoint].OnPointerOver += OnOverHander;
                PointSelected[parts].Add(false);
            }
            this.parts[clothType].pointGroup[parts].SetActive(true);
        }

        uiMenu.AddLabel("시접", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
        uiMenu.AddDivider(UIBuilder.PANE_RIGHT);
        uiMenu.AddLabel("원단 도안의 점선을 따라 포인터를 움직여 시접선을 그리세요.", TextAnchor.MiddleCenter, UIBuilder.PANE_RIGHT);
        uiMenu.Show();
    }

    public void OnOverHander(int parts, int idxPoint)
    {
        int clothType = (int)Data.clothType;
        if (curDrawLineItem == -1)
        {
            curDrawLineItem = parts;
            counter = 0;
            this.parts[clothType].cutParts[parts].gameObject.SetActive(true);
        }
        if (curDrawLineItem == parts)
        {
            if (!PointSelected[parts][idxPoint])
            {
                // 현재 그리고 있는 도안이고, 이전에 선택된 점이 아닐 때만
                int totalPoints = PointSelected[parts].Count;
                PointSelected[parts][idxPoint] = true;
                this.parts[clothType].pointGroup[parts].transform.GetChild(idxPoint).gameObject.SetActive(false);
                ++counter;
                this.parts[clothType].cutParts[parts].GetComponent<Image>().fillAmount = (float)counter / (float)totalPoints;
                if (counter == totalPoints)
                {
                    this.parts[clothType].pointGroup[parts].SetActive(false);
                    curDrawLineItem = -1;
                    counter = 0;
                    if (++numDrawingCompleted == this.parts[clothType].cutParts.Length)
                    {
                        GoNext();
                    }
                }
            }
        }
        else if (uiMsg == null)
        {
            uiMenu.Hide();
            uiMsg = Instantiate<UIBuilder>(uiCanvasPrefab);
            uiMsg.SetPosition(menuPosition);
            uiMsg.AddLabel("<b>알림</b>");
            uiMsg.AddDivider();
            uiMsg.AddLabel("기존에 그리고 있던 도안의 시접선 마킹이 끝나지 않았습니다.\n한 파트를 끝낸 후 다른 파트에 마킹해 주세요.");
            uiMsg.AddButton("확인", delegate { Destroy(uiMsg.gameObject); uiMsg = null; uiMenu.Show(); });
            uiMsg.Show();
        }
    }

    public void GoNext()
    {
        Destroy(uiMenu.gameObject);
        uiMenu = Instantiate<UIBuilder>(uiCanvasPrefab);
        uiMenu.SetPosition(menuPosition);
        uiMenu.AddLabel("재단");
        uiMenu.AddDivider();
        uiMenu.AddLabel("시접선이 그려졌습니다.\n시접선을 따라 재단을 합니다.");
        uiMenu.Show();
        StartCoroutine(FadeOutBackground());
    }

    IEnumerator FadeOutBackground()
    {
        float alpha = 1;
        Image img;
        Color color;
        for (int i = 0; i < parts[(int)Data.clothType].background.Length; i++)
        {
            img = parts[(int)Data.clothType].background[i].GetComponent<Image>();//
            while (alpha > 0)
            {
                color = img.color;
                color.a = alpha;
                img.color = color;
                alpha -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }       
        uiMenu.AddButton("다음으로", OnTutorialEnd);
    }

    public override void OnTutorialEnd()
    {
        // 뭔가 한 뒤 마지막에 base.OnAllDone() 호출
        base.OnTutorialEnd();
    }
}
