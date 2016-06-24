using UnityEngine;
using System.Collections;

public class FadeScene : MonoBehaviour {

	public Animator animFade; 						// Reference to animator which will fade to and from black when starting game.
	public AnimationClip fadeAlphaAnimationClip;
	public float animTime { get; set; }

	void Start() {
		animTime = fadeAlphaAnimationClip.length * .5f;
	}

	public void FadeToBlack() {
		animFade.SetTrigger ("fade");	
	}

}
