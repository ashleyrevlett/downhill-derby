using UnityEngine;
using System.Collections;

public class FadeScene : MonoBehaviour {

	public Animator animFade; 						// Reference to animator which will fade to and from black when starting game.
	public AnimationClip fadeAlphaAnimationClip;
	public float animTime { get; set; }

	void Start() {
		animTime = fadeAlphaAnimationClip.length;
		Debug.Log ("animTime: " + animTime);
	}

	public void Fade() {
		animFade.SetTrigger ("fade");	
	}

}
