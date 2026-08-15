using System;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class BowlManager : MonoBehaviour {
	private                  string[]        allowedFoods     = {};
	private                  string[]        allowedFoods1    = { "Water", "Flour" };
	private                  string[]        allowedFoods2    = { "Oil", "Cheese" };
	private                  string[]        addedFoods       = { };
	private                  int             allowedFoodCount = 0;
	private const            int             stages           = 2;
	private                  int             stage            = 0;
	[SerializeField] private TextMeshProUGUI text;
	[SerializeField] private GameObject[]    foods;
	[SerializeField] private Sprite          Stage1Sprite;
	[SerializeField] private Sprite          Stage2Sprite;

	private void Start() {
		allowedFoodCount = foods.Length;
		NextStage();
	}
	
    private void OnCollisionEnter2D(Collision2D other) {
	    StageOneMiniGame(other);
    }

    private void StageOneMiniGame(Collision2D other) {
	    if (other.gameObject.CompareTag("Food"))
	    {	
		    if(foods.Contains(other.gameObject) ) {
			    addedFoods = addedFoods.Append(other.gameObject.name).ToArray();
			    Destroy(other.gameObject);
			    if (addedFoods.Length == allowedFoodCount) {
				    NextStage();
			    }
		    }
	    }
    }

    private void NextStage() {
	    stage++;
	    if (stage > stages) {
		    PlayerManager.instance.ChangeMangoes(1);
		    Debug.Log(PlayerManager.instance.GetMangoes());
		    SceneManager.LoadSceneAsync("StreetScene");
		    return;
	    }

	    switch (stage) {
		    case 1:
			    GetComponent<SpriteRenderer>().sprite = Stage1Sprite;
			    text.text                             = "Add ingredient!";
			    break;
		    case 2:
			    GetComponent<SpriteRenderer>().sprite = Stage2Sprite;
			    break;
	    }
	    addedFoods       = new string[] { };
    }
}
