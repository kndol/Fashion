using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// https://forum.unity.com/threads/how-best-to-test-vr-when-not-always-using-a-vr-headset.532265/
public class PositionLaserPointer : MonoBehaviour
{
	[SerializeField]
	float maxReach = 30;
	[SerializeField]
	LayerMask layerMask;

	Fashion.UIManager.LaserPointer lp;
	Vector3 offset;
	GameObject go;

    bool isCheck = false;

    private void Start()
	{
		offset = new Vector3(0, -0.01f, 0);
		go = new GameObject();
	}
	void LateUpdate()
	{
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_EDITOR
		if (!lp) lp = FindObjectOfType<Fashion.UIManager.LaserPointer>();
		if (lp)
		{
			go.transform.position = transform.position + offset;
			go.transform.rotation = transform.rotation;
			go.transform.localScale = transform.localScale;
			Ray ray = new Ray(go.transform.position, go.transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxReach, layerMask))
			{
                lp.SetCursorStartDest(go.transform.position, hit.point, hit.point.normalized);
            }
			else
			{
				lp.SetCursorRay(go.transform);
			}
		}
#endif
	}
}
