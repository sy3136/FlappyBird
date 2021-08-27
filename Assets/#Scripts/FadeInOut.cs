using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
	public void hitFade()
    {
		StartCoroutine(hit());

	}
	public void FadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeIn(fadeOutTime, nextEvent));
	}

	public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
	}
	IEnumerator hit()
    {
		Image panel = this.gameObject.GetComponent<Image>();
		Color tmpColor = panel.color;
		Color white = new Color(1, 1, 1, 0);
		Color tempColor = white;
		while (tempColor.a < 1f)
		{
			tempColor.a += Time.unscaledDeltaTime / 0.1f;
			panel.color = tempColor;
			if (tempColor.a >= 1f) tempColor.a = 1f;
			yield return null;
		}
		panel.color = tmpColor;
	}
	// 투명 -> 불투명
	IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		Image panel = this.gameObject.GetComponent<Image>();
		Color tempColor = panel.color;
		while (tempColor.a < 1f)
		{
			
			tempColor.a += Time.unscaledDeltaTime / fadeOutTime;
			panel.color = tempColor;

			if (tempColor.a >= 1f) tempColor.a = 1f;

			yield return null;
		}

		panel.color = tempColor;
		if (nextEvent != null) nextEvent();
	}

	// 불투명 -> 투명
	IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
	{
		Image panel = this.gameObject.GetComponent<Image>();
		Color tempColor = panel.color;
		while (tempColor.a > 0f)
		{
			tempColor.a -= Time.unscaledDeltaTime / fadeOutTime;
			panel.color = tempColor;

			if (tempColor.a <= 0f) tempColor.a = 0f;

			yield return null;
		}
		panel.color = tempColor;
		if (nextEvent != null) nextEvent();
	}

}
