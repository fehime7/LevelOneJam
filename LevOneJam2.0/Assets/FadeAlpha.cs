using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class FadeAlpha : MonoBehaviour
	{

	private void Start() {
		//test  		StartCoroutine(FadeOut3D(this.transform, 0, true, 1));
	}

	public void kill() {
		if (gameObject.active)
		{
			StartCoroutine(FadeOut3D(this.transform, 0, true, 1));
		}
	}
	public static IEnumerator FadeOut3D(Transform t, float targetAlpha, bool isVanish, float duration)
	{
		Debug.Log("Fading");
		Renderer sr = t.GetComponent<Renderer>();
		float diffAlpha = (targetAlpha - sr.material.color.a);

		float counter = 0;
		while (counter < duration)
		{
			Debug.Log("Fading while");
			float alphaAmount = sr.material.color.a + (Time.deltaTime * diffAlpha) / duration;
			sr.material.color = new Color(sr.material.color.r, sr.material.color.g, sr.material.color.b, alphaAmount);

			counter += Time.deltaTime;
			yield return null;
		}
		sr.material.color = new Color(sr.material.color.r, sr.material.color.g, sr.material.color.b, targetAlpha);
		if (isVanish)
		{
			//sr.transform.gameObject.SetActive(false);
			if (sr.transform.gameObject.name != "Player")
			{
				Destroy(sr.transform.gameObject);
			}
			else {
				sr.transform.gameObject.SetActive(false);
			}
		}
	}
}
