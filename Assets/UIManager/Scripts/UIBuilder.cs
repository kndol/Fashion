/************************************************************************************

Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.  

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided “AS IS” WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif

namespace Fashion.UIManager
{
	public class UIBuilder : MonoBehaviour
	{
		// room for extension:
		// support update funcs
		// fix bug where it seems to appear at a random offset
		// support remove

		// Convenience consts for clarity when using multiple panes. 
		// But note that you can an arbitrary number of panes if you add them in the inspector.
		public const int PANE_CENTER = 0;
		public const int PANE_RIGHT = 1;
		public const int PANE_LEFT = 2;

		[SerializeField]
		private RectTransform buttonPrefab;
		[SerializeField]
		private RectTransform labelPrefab;
		[SerializeField]
		private RectTransform scrollViewPrefab;
		[SerializeField]
		private RectTransform sliderPrefab;
		[SerializeField]
		private RectTransform dividerPrefab;
		[SerializeField]
		private RectTransform togglePrefab;
		[SerializeField]
		private RectTransform radioPrefab;
		[SerializeField]
		private RectTransform imagePrefab;
		[SerializeField]
		private RectTransform HorizontalSectionPrefab;

		private RectTransform []HorizontalSections;

		[SerializeField]
		private GameObject uiHelpersToInstantiate;

		[SerializeField]
		private Transform[] targetContentPanels;

		[SerializeField]
		private bool manuallyResizeContentPanels;

		private bool[] reEnable;

		[SerializeField]
		private List<GameObject> toEnable;
		[SerializeField]
		private List<GameObject> toDisable;

		public delegate void OnClick();
		public delegate void OnToggleValueChange(Toggle t);
		public delegate void OnSlider(float f);
		public delegate bool ActiveUpdate();

		private const float elementSpacing = 16.0f;
		private const float marginH = 16.0f;
		private const float marginV = 16.0f;
		private Vector2[] insertPositions;
		private List<RectTransform>[] insertedElements;
		private Vector3 menuOffset;

		OVRCameraRig rig;
		private Dictionary<string, ToggleGroup> radioGroups = new Dictionary<string, ToggleGroup>();
		LaserPointer lp;
		LineRenderer lr;

		public LaserPointer.LaserBeamBehavior laserBeamBehavior;

		public void Awake()
		{
			menuOffset = transform.position; // TODO: this is unpredictable/busted
			gameObject.SetActive(false);
			rig = FindObjectOfType<OVRCameraRig>();
			for (int i = 0; i < toEnable.Count; ++i)
			{
				toEnable[i].SetActive(false);
			}

			insertPositions = new Vector2[targetContentPanels.Length];
			for (int i = 0; i < insertPositions.Length; ++i)
			{
				insertPositions[i].x = marginH;
				insertPositions[i].y = -marginV;
			}
			insertedElements = new List<RectTransform>[targetContentPanels.Length];
			for (int i = 0; i < insertedElements.Length; ++i)
			{
				insertedElements[i] = new List<RectTransform>();
			}

			lp = FindObjectOfType<LaserPointer>();
			print(lp);
			if (!lp)
			{
				if (uiHelpersToInstantiate)
				{
					GameObject.Instantiate(uiHelpersToInstantiate);
				}
				lp = FindObjectOfType<LaserPointer>();
				if (!lp)
				{
					Debug.LogError("UIBuilder requires use of a LaserPointer and will not function without it. Add one to your scene, or assign the UIHelpers prefab to the UIBuilder in the inspector.");
					return;
				}
			}
			lp.laserBeamBehavior = laserBeamBehavior;
			if (!toEnable.Contains(lp.gameObject))
			{
				toEnable.Add(lp.gameObject);
			}
			GetComponent<OVRRaycaster>().pointer = lp.gameObject;
			HorizontalSections = new RectTransform[targetContentPanels.Length];
			for(int i=0; i< HorizontalSections.Length; i++)
				HorizontalSections[i] = null;

#if UNITY_EDITOR
			string scene = SceneManager.GetActiveScene().name;
			OVRPlugin.SendEvent("ui_builder",
			  ((scene == "Fashion shop") ||
			   (scene == "UIManager") ||
			   (scene == "DistanceGrab") ||
			   (scene == "OVROverlay") ||
			   (scene == "Locomotion")).ToString(),
			  "fashion_framework");
#endif
		}

		/// <summary>
		/// 패널의 너비 변경하기
		/// </summary>
		/// <param name="width">새로운 너비</param>
		/// <param name="panelIdx">변경할 패널의 ID, 기본값은 0</param>
		public void SetPaneWidth(float width, int panelIdx = 0)
		{
			RectTransform canvasRect = targetContentPanels[panelIdx].GetComponent<RectTransform>();
			canvasRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
			if (gameObject.activeInHierarchy)
			{
				Relayout();
			}
		}

		/// <summary>
		/// 플레이어와 메뉴 사이의 거리 조절
		/// </summary>
		/// <param name="distance">거리(m 단위)</param>
		public void SetDistanceFromPlayer(float distance)
		{
			menuOffset.z = distance;
			if (gameObject.activeInHierarchy)
			{
				Vector3 pos = rig.transform.TransformPoint(menuOffset);
				// KnDol - 위치가 이상하게 낮아지면 기본 위치로 복구
				if (pos.y < menuOffset.y) pos.y = menuOffset.y;
				transform.position = pos;
			}
		}

		/// <summary>
		/// UI 패널 표시
		/// </summary>
		public void Show()
		{
			Relayout();
			gameObject.SetActive(true);
			Vector3 pos = rig.transform.TransformPoint(menuOffset);
			// KnDol - 위치가 이상하게 낮아지면 기본 위치로 복구
			if (pos.y < menuOffset.y) pos.y = menuOffset.y;
			transform.position = pos;
			Vector3 newEulerRot = rig.transform.rotation.eulerAngles;
			newEulerRot.x = 0.0f;
			newEulerRot.z = 0.0f;
			transform.eulerAngles = newEulerRot;

			if (reEnable == null || reEnable.Length < toDisable.Count) reEnable = new bool[toDisable.Count];
			reEnable.Initialize();
			int len = toDisable.Count;
			for (int i = 0; i < len; ++i)
			{
				if (toDisable[i])
				{
					reEnable[i] = toDisable[i].activeSelf;
					toDisable[i].SetActive(false);
				}
			}
			len = toEnable.Count;
			for (int i = 0; i < len; ++i)
			{
				toEnable[i].SetActive(true);
			}

			int numPanels = targetContentPanels.Length;
			for (int i = 0; i < numPanels; ++i)
			{
				targetContentPanels[i].gameObject.SetActive(insertedElements[i].Count > 0);
			}
		}

		/// <summary>
		/// UI 패널 감추기
		/// </summary>
		public void Hide()
		{
			gameObject.SetActive(false);

			for (int i = 0; i < reEnable.Length; ++i)
			{
				if (toDisable[i] && reEnable[i])
				{
					toDisable[i].SetActive(true);
				}
			}

			int len = toEnable.Count;
			for (int i = 0; i < len; ++i)
			{
				toEnable[i].SetActive(false);
			}
		}

		// Currently a slow brute-force method that lays out every element. 
		// As this is intended as a UI manager, it might be fine, but there are many simple optimizations we can make.
		private void Relayout()
		{
			for (int panelIdx = 0; panelIdx < targetContentPanels.Length; ++panelIdx)
			{
				RectTransform canvasRect = targetContentPanels[panelIdx].GetComponent<RectTransform>();
				List<RectTransform> elems = insertedElements[panelIdx];
				int elemCount = elems.Count;
				float x = marginH;
				float y = -marginV;
				float maxWidth = canvasRect.offsetMax.x - canvasRect.offsetMin.x - marginH * 2f;
				for (int elemIdx = 0; elemIdx < elemCount; ++elemIdx)
				{
					RectTransform r = elems[elemIdx];
					r.anchoredPosition = new Vector2(x, y);
					y -= (r.rect.height + elementSpacing);
					maxWidth = Mathf.Max(r.rect.width + 2 * marginH, maxWidth);
					r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth - 2 * marginH);
				}
				canvasRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
				canvasRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, -y + marginV);
			}
		}

		private void AddRect(RectTransform r, int targetCanvas)
		{
			if (targetCanvas > targetContentPanels.Length)
			{
				Debug.LogError("Attempted to add panel to canvas " + targetCanvas + ", but only " + targetContentPanels.Length + " panels were provided. Fix in the inspector or pass a lower value for target canvas.");
				return;
			}
			bool isHorizontalSection = HorizontalSections[targetCanvas] != null;
			Transform parent = isHorizontalSection ? HorizontalSections[targetCanvas] : targetContentPanels[targetCanvas];
			r.transform.SetParent(parent, false);
			if (!isHorizontalSection) insertedElements[targetCanvas].Add(r);
			if (gameObject.activeInHierarchy)
			{
				Relayout();
			}
        }

		/// <summary>
		/// 버튼 만들기
		/// </summary>
		/// <param name="label">표시할 텍스트</param>
		/// <param name="handler">버튼을 클릭했을 때 호출할 콜백 함수</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddButton(string label, OnClick handler, int targetCanvas = 0)
		{
			RectTransform buttonRT = GameObject.Instantiate(buttonPrefab).GetComponent<RectTransform>();
			Button button = buttonRT.GetComponentInChildren<Button>();
			button.onClick.AddListener(delegate { handler(); });
			((Text)(buttonRT.GetComponentsInChildren(typeof(Text), true)[0])).text = label;
			AddRect(buttonRT, targetCanvas);
			return buttonRT;
		}

		/// <summary>
		/// 이미지 버튼 만들기 - 크기는 이미지의 원래 크기
		/// </summary>
		/// <param name="sprite">버튼에 사용할 이미지</param>
		/// <param name="handler">버튼을 클릭했을 때 호출할 콜백 함수</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddImageButton(Sprite sprite, OnClick handler, int targetCanvas = 0)
		{
			return AddImageButton(sprite, sprite.textureRect, handler, targetCanvas);
		}

		/// <summary>
		/// 이미지 버튼 만들기
		/// </summary>
		/// <param name="sprite">버튼에 사용할 이미지</param>
		/// <param name="size">명시적으로 버튼의 크기를 지정</param>
		/// <param name="handler">버튼을 클릭했을 때 호출할 콜백 함수</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddImageButton(Sprite sprite, Vector2 size, OnClick handler, int targetCanvas = 0)
		{
			return AddImageButton(sprite, new Rect(0, 0, size.x, size.y), handler, targetCanvas);
		}

		/// <summary>
		/// 이미지 버튼 만들기
		/// </summary>
		/// <param name="sprite">버튼에 사용할 이미지</param>
		/// <param name="rect">명시적으로 버튼의 크기를 지정</param>
		/// <param name="handler">버튼을 클릭했을 때 호출할 콜백 함수</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddImageButton(Sprite sprite, Rect rect, OnClick handler, int targetCanvas = 0)
		{
			RectTransform buttonRT = GameObject.Instantiate(buttonPrefab).GetComponent<RectTransform>();
			Button button = buttonRT.GetComponentInChildren<Button>();
			Image img = buttonRT.GetComponentInChildren<Image>();
			buttonRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.width);
			buttonRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.height);
			buttonRT.GetComponentInChildren<Text>().gameObject.SetActive(false);

			img.sprite = sprite;
			button.onClick.AddListener(delegate { handler(); });
			
            AddRect(buttonRT, targetCanvas);
            return buttonRT;
        }

		/// <summary>
		/// 이미지 만들기 - 크기는 이미지의 원래 크기
		/// </summary>
		/// <param name="sprite">사용할 이미지</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddImage(Sprite sprite, int targetCanvas = 0)
		{
			return AddImage(sprite, sprite.textureRect, targetCanvas);
		}

		/// <summary>
		/// 이미지 만들기
		/// </summary>
		/// <param name="sprite">사용할 이미지</param>
		/// <param name="size">명시적으로 이미지의 크기를 지정</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddImage(Sprite sprite, Vector2 size, int targetCanvas = 0)
		{
			return AddImage(sprite, new Rect(0, 0, size.x, size.y), targetCanvas);
		}

		/// <summary>
		/// 이미지 만들기
		/// </summary>
		/// <param name="sprite">사용할 이미지</param>
		/// <param name="rect">명시적으로 이미지의 크기를 지정</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddImage(Sprite sprite, Rect rect, int targetCanvas = 0)
		{
			RectTransform rt = GameObject.Instantiate(imagePrefab).GetComponent<RectTransform>();
			Image t = rt.GetComponent<Image>();
			t.sprite = sprite;
			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.width == 0 ? sprite.textureRect.width : rect.width);
			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.height == 0 ? sprite.textureRect.height : rect.height);
			AddRect(rt, targetCanvas);
			return rt;
		}

		/// <summary>
		/// 텍스트 레이블 만들기
		/// </summary>
		/// <param name="label">표시할 텍스트</param>
		/// <param name="txtAlign">텍스트 정렬 방식, 기본값은 왼쪽, 위 정렬</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddLabel(string label, TextAnchor txtAlign = TextAnchor.MiddleCenter, int targetCanvas = 0)
		{
			RectTransform rt = GameObject.Instantiate(labelPrefab).GetComponent<RectTransform>();
			Text t = rt.GetComponent<Text>();

			t.text = label;
			t.alignment = txtAlign;

			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, t.preferredHeight);
			AddRect(rt, targetCanvas);

			return rt;
		}

		/// <summary>
		/// 텍스트 스크롤 뷰 만들기
		/// </summary>
		/// <param name="txtContent">표시할 내용</param>
		/// <param name="txtAlign">텍스트 정렬 방식, 기본값은 왼쪽, 위 정렬</param>
		/// <param name="height">스크롤 뷰의 높이, 기본값은 300</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddScrollView(string txtContent, TextAnchor txtAlign = TextAnchor.UpperLeft, int height = 300, int targetCanvas = 0)
		{
			RectTransform rt = GameObject.Instantiate(scrollViewPrefab).GetComponent<RectTransform>();
			Text t = rt.GetComponentInChildren<Text>();

			t.text = txtContent;
			t.alignment = txtAlign;

			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
			AddRect(rt, targetCanvas);

			return rt;
		}

		/// <summary>
		/// 슬라이더바 만들기
		/// </summary>
		/// <param name="label">표시할 텍스트</param>
		/// <param name="min">최소값</param>
		/// <param name="max">최대값</param>
		/// <param name="onValueChanged">값이 변경될 때 호출할 콜백 함수</param>
		/// <param name="wholeNumbersOnly">true이면 정수만 사용</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddSlider(string label, float min, float max, OnSlider onValueChanged, bool wholeNumbersOnly = false, int targetCanvas = 0)
		{
			RectTransform rt = (RectTransform)GameObject.Instantiate(sliderPrefab);
			Slider s = rt.GetComponentInChildren<Slider>();
			s.minValue = min;
			s.maxValue = max;
			s.onValueChanged.AddListener(delegate (float f) { onValueChanged(f); });
			s.wholeNumbers = wholeNumbersOnly;
			AddRect(rt, targetCanvas);
			return rt;
		}

		/// <summary>
		/// 구분선 만들기
		/// </summary>
		/// <param name="targetCanvas"></param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddDivider(int targetCanvas = 0)
		{
			RectTransform rt = (RectTransform)GameObject.Instantiate(dividerPrefab);
			AddRect(rt, targetCanvas);
			return rt;
		}

		/// <summary>
		/// 토글 버튼 만들기
		/// </summary>
		/// <param name="label">표시할 텍스트</param>
		/// <param name="onValueChanged">값이 변경될 때 호출할 콜백 함수</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddToggle(string label, OnToggleValueChange onValueChanged, int targetCanvas = 0)
		{
			RectTransform rt = (RectTransform)GameObject.Instantiate(togglePrefab);
			AddRect(rt, targetCanvas);
			Text buttonText = rt.GetComponentInChildren<Text>();
			buttonText.text = label;
			Toggle t = rt.GetComponentInChildren<Toggle>();
			t.onValueChanged.AddListener(delegate { onValueChanged(t); });
			return rt;
		}

		/// <summary>
		/// 토글 버튼 만들기
		/// </summary>
		/// <param name="label">표시할 텍스트</param>
		/// <param name="onValueChanged">값이 변경될 때 호출할 콜백 함수</param>
		/// <param name="defaultValue">기본으로 선택된 버튼일 때 true</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddToggle(string label, OnToggleValueChange onValueChanged, bool defaultValue, int targetCanvas = 0)
		{
			RectTransform rt = (RectTransform)GameObject.Instantiate(togglePrefab);
			AddRect(rt, targetCanvas);
			Text buttonText = rt.GetComponentInChildren<Text>();
			buttonText.text = label;
			Toggle t = rt.GetComponentInChildren<Toggle>();
			t.isOn = defaultValue;
			t.onValueChanged.AddListener(delegate { onValueChanged(t); });
			return rt;
		}

		/// <summary>
		/// 라디오 버튼 만들기
		/// </summary>
		/// <param name="label">표시할 텍스트</param>
		/// <param name="group">라디오 버튼 그룹의 이름</param>
		/// <param name="handler">값이 변경될 때 호출할 콜백 함수</param>
		/// <param name="targetCanvas">표시할 패널의 ID, 기본값은 0</param>
		/// <returns>생성된 객체의 RectTransform</returns>
		public RectTransform AddRadio(string label, string group, OnToggleValueChange handler, int targetCanvas = 0)
		{
			RectTransform rt = (RectTransform)GameObject.Instantiate(radioPrefab);
			AddRect(rt, targetCanvas);
			Text buttonText = rt.GetComponentInChildren<Text>();
			buttonText.text = label;
			Toggle tb = rt.GetComponentInChildren<Toggle>();
			if (group == null) group = "default";
			ToggleGroup tg = null;
			bool isFirst = false;
			if (!radioGroups.ContainsKey(group))
			{
				tg = tb.gameObject.AddComponent<ToggleGroup>();
				radioGroups[group] = tg;
				isFirst = true;
			}
			else
			{
				tg = radioGroups[group];
			}
			tb.group = tg;
			tb.isOn = isFirst;
			tb.onValueChanged.AddListener(delegate { handler(tb); });
			return rt;
		}

		/// <summary>
		/// 레이저 포인터 켜고 끄기
		/// </summary>
		/// <param name="isOn">true이면 켜기</param>
		public void ToggleLaserPointer(bool isOn)
		{
			if (lp)
			{
				if (isOn)
				{
					lp.enabled = true;
				}
				else
				{
					lp.enabled = false;
				}
			}
		}

		public void StartHorizontalSection(float Spacing = 0, int targetCanvas = 0)
		{
			RectTransform rt = (RectTransform)GameObject.Instantiate(HorizontalSectionPrefab);
			HorizontalLayoutGroup hl = rt.GetComponent<HorizontalLayoutGroup>();
			hl.spacing = Spacing;
			AddRect(rt, targetCanvas);
			HorizontalSections[targetCanvas] = rt;
		}

		public void EndHorizontalSection(int targetCanvas = 0)
		{
			Assert.IsNotNull(HorizontalSections[targetCanvas]);
			HorizontalSections[targetCanvas] = null;
		}
	}
}
