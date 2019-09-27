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

public class Test_Mode_UI : FashionController
{
    UIBuilder uiTest;
	private void Start()
	{

	}
}
