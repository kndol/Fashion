using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
	public static PlayerSingleton instance;

	private void Awake()
	{
		// 싱글톤으로 실행
		if (instance == null) { instance = this; }
		else if (instance != this) { Destroy(gameObject); }
		DontDestroyOnLoad(gameObject);
	}
}
