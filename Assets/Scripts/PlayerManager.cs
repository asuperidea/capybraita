using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager instance;

	private TextMeshProUGUI mangoesText;
	private TextMeshProUGUI hungerText;
	private                  int             hunger        = 100;
	private                  int             mangoes       = 10;
	public                   string          characterType = "emo";

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

	public void Restart() {
		mangoes = 0;
		hunger  = 100;
		UpdateText();
	}

	private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (scene.name  != "StreetScene") return;
		UpdateText();
	}

	public void ChangeMangoes(int amount) {
		mangoes += amount;
		hunger  -= amount * Mathf.CeilToInt(UnityEngine.Random.Range(1f, 1.2f));
		if (hunger < 0) hunger = 0;
		Die();
	}

	public void Eat(int amount) {
		print("Tried to eat");
		if (amount > mangoes || hunger + amount > 100) return;
		ChangeMangoes(-amount);
		UpdateText();
	}

	private void UpdateText() {
		if (mangoesText == null) mangoesText = GameObject.Find("MangoesText").GetComponent<TextMeshProUGUI>();
		mangoesText.text = mangoes.ToString();
		if (hungerText == null) hungerText = GameObject.Find("HungerText").GetComponent<TextMeshProUGUI>();
		hungerText.text = hunger.ToString() + "/ 100";
	}

	private void Die() {
		
	}

	public int GetMangoes() => mangoes;
}