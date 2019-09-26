using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Fashion.UIManager;

namespace Fashion
{
	public class FashionController : MonoBehaviour
	{
		[Header("UI 프리펩")]
		[Tooltip("UIBuilder 프리펩 연결하기")]
		[SerializeField]
		protected UIBuilder uiCanvasPrefab = null;
		[SerializeField]
		protected Transform playerPos = null;
		[Header("다음 작업")]
		[Tooltip("이 작업이 끝나면 이동할 다음 작업")]
		[SerializeField]
		string nextSceneName = null;

		public virtual void OnTutorialEnd()
		{
			Assert.IsFalse(string.IsNullOrEmpty(nextSceneName));
			SceneManager.LoadScene(nextSceneName);
		}
	}
}
