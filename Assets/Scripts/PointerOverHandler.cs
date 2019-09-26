using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerOverHandler : MonoBehaviour, IPointerEnterHandler
{
	[HideInInspector]
	public int group { protected get; set; }
	[HideInInspector]
	public int id { protected get; set; }

	public UnityAction<int, int> OnPointerOver = delegate { };

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnPointerOver(group, id);
	}
}
