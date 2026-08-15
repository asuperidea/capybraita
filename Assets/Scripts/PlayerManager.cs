using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager instance;

	[SerializeField] private TextMeshProUGUI mangoesText;

	private int    mangoes       = 10;
	public  string characterType = "emo";

	private void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (scene.name  != "StreetScene") return;
		if (mangoesText == null) mangoesText = GameObject.Find("MangoesText").GetComponent<TextMeshProUGUI>();
		mangoesText.text = "Mangoes: " + mangoes.ToString();
	}

	public void ChangeMangoes(int amount) => mangoes += amount;


	public int GetMangoes() => mangoes;
}