using UnityEngine;

// https://forum.unity.com/threads/how-best-to-test-vr-when-not-always-using-a-vr-headset.532265/
public class PositionLaserPointer : MonoBehaviour
{
	[SerializeField]
	float maxReach = 30;
	[SerializeField]
	LayerMask layerMask;

	Fashion.UIManager.LaserPointer lp;

	void LateUpdate()
	{
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_EDITOR
		if (!lp) lp = FindObjectOfType<Fashion.UIManager.LaserPointer>();
		if (lp)
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, maxReach, layerMask))
			{
				lp.SetCursorStartDest(transform.position, hit.point, hit.point.normalized);
			}
			else
			{
				lp.SetCursorRay(transform);
			}
		}
#endif
	}
}
