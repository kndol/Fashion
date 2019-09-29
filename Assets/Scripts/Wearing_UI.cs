using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Fashion;
using Fashion.UIManager;
using UnityEngine.SceneManagement;

public class Wearing_UI : FashionController
{
    UIBuilder uiWearing;

    void Start()
    {
        uiWearing = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiWearing.AddLabel("착장");
        uiWearing.AddDivider();
        uiWearing.AddButton("확인", WearingButton);
        uiWearing.AddButton("재시작", RestartButton);
        uiWearing.Show();
    }

    public void WearingButton()
    {
        
    }

    public void RestartButton()
    {
        OnTutorialEnd();
    }

    public override void OnTutorialEnd()
    {
        base.OnTutorialEnd();
    }
}
