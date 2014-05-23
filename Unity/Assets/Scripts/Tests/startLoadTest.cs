using UnityEngine;
using System.Collections;

public class startLoadTest : MonoBehaviour {
	AsyncOperation async;
	IEnumerator Start() {
		Debug.Log("Loading start " + Time.time);
		DontDestroyOnLoad(this);
		async = Application.LoadLevelAsync("endLoadTest");
		async.allowSceneActivation = true;
		Debug.Log("Loading yield "+ Time.time);
		yield return async;
		Debug.Log("Loading complete "+ Time.time);
	}

	void Update()
	{
		Debug.Log ("progress: " + async.progress.ToString());
	}
}
