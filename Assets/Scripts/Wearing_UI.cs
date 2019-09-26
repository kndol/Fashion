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

    public override void StartTutorial()
    {
        base.StartTutorial();

        uiWearing = Instantiate<UIBuilder>(uiCanvasPrefab);

        uiWearing.AddLabel("착장");
        uiWearing.AddDivider();
        uiWearing.AddButton("확인", WearingButton);
        uiWearing.AddButton("재시작", Restart_Button);
    }

    public void WearingButton()
    {
        uiWearing.Hide();
    }

    public void Restart_Button()
    {
        SceneManager.LoadScene("Fashion_Shop");
    }

    public override void OnTutorialEnd()
    {
        base.OnTutorialEnd();
    }
}
