using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

	public class FadeAlpha : MonoBehaviour
	{

    private bool startedFading = false;
    private MonsterManager monManager;

    private void Start() {
        //test  		StartCoroutine(FadeOut3D(this.transform, 0, true, 1));
        monManager = (MonsterManager)FindObjectOfType(typeof(MonsterManager));
    }

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Return))
			SceneManager.LoadScene(0); //or whatever number your scene is

	}

	public void kill() {
        monManager.enemies.Remove(this.gameObject);

		if (gameObject.active && !startedFading)
		{
			startedFading = true;
			StartCoroutine(FadeOut3D(this.transform, 0, true, 0.001f));
		}
	}
	public static IEnumerator FadeOut3D(Transform t, float targetAlpha, bool isVanish, float duration)
	{
		Debug.Log("Fading");
		Renderer sr = t.GetChild(0).GetComponent<Renderer>();
		float diffAlpha = (targetAlpha - sr.material.color.a);

		if (sr.transform.gameObject.name != "avatar") {
			Collider coll = t.GetComponent<Collider>();
			coll.isTrigger = true;
		}
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
			if (sr.transform.gameObject.name != "avatar")
			{
				Destroy(t.gameObject);
			}
			else {
				t.gameObject.SetActive(false);
			}
		}
	}
}
