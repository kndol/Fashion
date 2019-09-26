using UnityEngine;
using Fashion.UIManager;

namespace Fashion
{
	public class FashionController : MonoBehaviour
	{
		[SerializeField]
		protected UIBuilder uiCanvasPrefab = null;
		[SerializeField]
		protected Transform playerPos = null;
		[SerializeField]
		FashionController nextController;

		GameObject player = null;

		public virtual void StartTutorial()
		{
			player = GameObject.Find("PlayerController");
			player.transform.position = playerPos.position;
			player.transform.rotation = playerPos.rotation;
		}

		public virtual void OnTutorialEnd()
		{
			nextController.StartTutorial();
		}

	}
}
