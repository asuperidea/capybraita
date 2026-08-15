using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour {
	[SerializeField] private Image           cutsceneImage;
	[SerializeField] private GameObject      characterSelect;
	[SerializeField] private List<Sprite>    cutsceneSprites;
	[SerializeField] private List<Sprite>    characterSprites; // 0 = Emo, 1 = Santi, 2 = Femboy
	[SerializeField] private Sprite          characterSelectSprite;
	[SerializeField] private Image           selectedCharacterImage;
	[SerializeField] private GameObject      spinningStar;
	[SerializeField] private TextMeshProUGUI skippingInText;
	[SerializeField] private float           cutsceneDuration = 3f;

	private float cutsceneTimer;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {
		cutsceneImage.sprite = cutsceneSprites[0];
		StartCoroutine(CutsceneCoroutine());
	}

	private void FixedUpdate() {
		if (skippingInText.enabled && cutsceneTimer < cutsceneDuration) {
			cutsceneTimer       += Time.deltaTime;
			skippingInText.text =  "Skipping in: " + Mathf.CeilToInt(cutsceneDuration - cutsceneTimer).ToString() + "s";
		}
	}

	public void SelectCharacter(string characterType) {
		Debug.Log("Character selected: " + characterType);
		PlayerManager.instance.characterType = characterType;
		cutsceneImage.enabled                = false;
		characterSelect.SetActive(false);

		// this is so bad :isob:
		selectedCharacterImage.sprite = characterSprites[characterType switch {
			"emo"    => 0,
			"santi"  => 1,
			"femboy" => 2,
			_        => throw new ArgumentOutOfRangeException(nameof(characterType), characterType, null)
		}];
		selectedCharacterImage.enabled = true;
		spinningStar.SetActive(true);
		skippingInText.enabled = true;
		cutsceneTimer          = 0f;
		StartCoroutine(LoadStreetSceneCoroutine());
	}

	private IEnumerator CutsceneCoroutine() {
		foreach (var t in cutsceneSprites) {
			cutsceneTimer        = 0f;
			cutsceneImage.sprite = t;
			yield return new WaitForSeconds(cutsceneDuration);
		}

		cutsceneImage.sprite = characterSelectSprite;
		characterSelect.SetActive(true);
		skippingInText.enabled = false;
	}

	private IEnumerator LoadStreetSceneCoroutine() {
		yield return new WaitForSeconds(cutsceneDuration);
		SceneManager.LoadScene("StreetScene");
	}
}