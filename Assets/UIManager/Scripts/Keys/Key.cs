using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace VRKeyboard.Utils
{
    public class Key : MonoBehaviour
    {
        protected Text key;
		protected Image image;
		protected Color color;
        public delegate void OnKeyClickedHandler(string key);

        // The event which other objects can subscribe to
        // Uses the function defined above as its type
        public event OnKeyClickedHandler OnKeyClicked;

        public virtual void Awake()
        {
            key = transform.Find("Text").GetComponent<Text>();
			image = GetComponent<Image>();
			color = image.color;
			GetComponent<Button>().onClick.AddListener(() =>
            {
				image.color = new Color(0.125f, 0, 1);
				StartCoroutine(ResetColor());
				OnKeyClicked(key.text);
            });
        }

		public IEnumerator ResetColor()
		{
			yield return new WaitForSeconds(0.2f);
			image.color = color;
		}

        public virtual void CapsLock(bool isUppercase) { }
        public virtual void ShiftKey() { }
		public virtual void Enter() { }
	};
}