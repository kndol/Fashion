using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour {

    [Header("STYLE PARENT")]
	[Tooltip("패널 = 로고 스타일의 부모")]
	[SerializeField]
	GameObject stylePanel = null;

	[Header("NEXT SCENE")]
	[Tooltip("인트로가 끝난 뒤에 이동할 다음 씬의 이름")]
	[SerializeField]
	string nextSceneName = null;
	[Tooltip("지정한 시간 뒤에 다음 씬으로 이동")]

	private string panelFadeIn = "Panel Open";
    private string panelFadeOut = "Panel Close";
    private string styleExpand = "Expand";

    private Animator panelAnimator;
	bool isAnimationCompleted = false;

	void Start ()
    {
        panelAnimator = stylePanel.GetComponent<Animator>();
        panelAnimator.Play(panelFadeIn);
		panelAnimator.Play(styleExpand);
    }

	void LateUpdate()
	{
		if (isAnimationCompleted)
		{
			SceneManager.LoadScene(nextSceneName);
		}
	}

	//Called via animation event
	public void AnimationComplete()
	{
		panelAnimator.Play(panelFadeIn);
		isAnimationCompleted = true;
	}
}
