using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PushMeter : MonoBehaviour {
	
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	public Vector2 pos = new Vector2(20, 40);
	public Vector2 size = new Vector2(20, 60);
	public Text hintText; 
	private GUIStyle currentStyle = null;
	private Pusher pusher;
	private float pusherCarDistance;
	private GameObject player;
	private LevelController lc;

	[SerializeField]
	public float pushMeterAmount = 0.1f;

	[SerializeField]
	public float fudgeFactor = .01f;

	[SerializeField]
	public float duration = 1f;
	float barDisplay = 0;

	void Start() {
		pusher = GameObject.FindGameObjectWithTag ("Pusher").GetComponent<Pusher> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		lc = GameObject.FindGameObjectWithTag ("LevelController").GetComponent<LevelController> ();
		hintText.gameObject.SetActive (false);
	}


	void Update() {
		pusherCarDistance = Vector3.Distance(pusher.gameObject.transform.position, player.transform.position);
		if (pusherCarDistance > pusher.maxDistanceToPlayer)
			hintText.gameObject.SetActive (false);
		else if (!hintText.gameObject.activeInHierarchy && lc.raceStarted)  {
			hintText.gameObject.SetActive (true);
		}
	}


	void OnGUI() {

		if (currentStyle == null) {
			currentStyle = new GUIStyle (GUI.skin.box);
			currentStyle.normal.background = MakeTex (2, 2, new Color (0f, 0f, 0f, 0f));
		}

		if (pusherCarDistance > pusher.maxDistanceToPlayer || !lc.raceStarted) 
			return;
			
		// draw the background:
		GUI.BeginGroup(new Rect (pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0, 0, size.x, size.y), progressBarEmpty, currentStyle);

		// draw the filled-in part:
		GUI.BeginGroup(new Rect (0, (size.y - (size.y  * barDisplay)), size.x, size.y  * barDisplay));
		GUI.Box(new Rect (0, -size.y + (size.y * barDisplay), size.x, size.y), progressBarFull, currentStyle);
		GUI.EndGroup();
		GUI.EndGroup ();
	}
		
	public void GoUp() {
		if (pusherCarDistance > pusher.maxDistanceToPlayer)
			return;
		
		StopAllCoroutines();
		StartCoroutine(GoUpRoutine());
	}

	private IEnumerator GoUpRoutine() {
		float origin = barDisplay;
		float destination = (float)System.Math.Round(Mathf.Clamp01 (barDisplay + pushMeterAmount), 2);
		float timeElapsed = 0f;
		while (Mathf.Abs(barDisplay - destination) > fudgeFactor) {
			barDisplay = (float)System.Math.Round(easeOutExpo (timeElapsed, origin, destination - origin, duration), 3);
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		destination = 0f;
		timeElapsed = 0f;
		origin = barDisplay;
		while (Mathf.Abs(barDisplay - destination) > fudgeFactor) {
			barDisplay = (float)System.Math.Round(easeOutExpo (timeElapsed, origin, destination - origin, duration), 3);
			timeElapsed += Time.deltaTime;
			yield return null;
		}

	}

	private float easeOutExpo(float t, float b, float c, float d) {
//		@t is the current time (or position) of the tween. This can be seconds or frames, steps, seconds, ms, whatever – as long as the unit is the same as is used for the total time [3].
//		@b is the beginning value of the property.
//		@c is the change between the beginning and destination value of the property.
//		@d is the total time of the tween.
		return (t==d) ? b+c : c * (-Mathf.Pow(2, -10 * t/d) + 1) + b;
	}


	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}

	public void HideMeter() {
	}


}

