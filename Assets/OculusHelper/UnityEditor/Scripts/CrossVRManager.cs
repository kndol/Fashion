using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossVRManager : MonoBehaviour
{
	void Awake()
	{
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_EDITOR
		FindObjectOfType<OVRPlayerController>().enabled = false;
#endif
	}
}
