using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialCut : MonoBehaviour
{
	[SerializeField]
	Transform[] PointGroup;

	List<bool>[] PointSelected;
	int[] countPointSelected;

	Fashion.UIManager.LaserPointer lp;

    bool hasLaser = false;
	private void Start()
	{
		int groupCount = PointGroup.Length;
		PointSelected = new List<bool>[groupCount];
		countPointSelected = new int[groupCount];
		for (int group = 0; group < groupCount; group++)
		{
			int count = PointGroup[group].childCount;
			PointerOverHandler[] handlers = PointGroup[group].GetComponentsInChildren<PointerOverHandler>();
			MeshRenderer[] rends = PointGroup[group].GetComponentsInChildren<MeshRenderer>();
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

	public void OnOverHander(int group, int id)
	{
		if (!PointSelected[group][id])
		{
			print("선택 group: " + group + ", id: " + id);
			PointSelected[group][id] = true;
			++countPointSelected[group];
			if (countPointSelected[group] > PointGroup[group].childCount * 2 / 3)
			{
				print("성공");
			}
		}
	}
}
