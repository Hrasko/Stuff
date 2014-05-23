using UnityEngine;
using System.Collections;

public class LoadWWW : MonoBehaviour {
	void Awake() {
		print("Starting WaitAndPrint " + Time.time);
		StartCoroutine(WaitAndPrint(2.0F));
		print("Before WaitAndPrint Finishes " + Time.time);
	}
	IEnumerator WaitAndPrint(float waitTime) {
		print("WaitAndPrint Before " + Time.time);
		yield return new WaitForSeconds (waitTime);
		print("WaitAndPrint After " + Time.time);
		WWW www = new WWW("https://forcapainel.azurewebsites.net/forca/server/getPalavrasIdioma2.php");
		print("WaitAndPrint Start www" + Time.time);
		yield return www;
		print("WaitAndPrint End www" + Time.time);
	}
}
