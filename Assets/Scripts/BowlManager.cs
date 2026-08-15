using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class BowlManager : MonoBehaviour {
	private                  string[]        addedFoods       = { };
	private                  int             allowedFoodCount = 0;
	private const            int             stages           = 4;
	private                  int             stage            = 0;
	[SerializeField] private TextMeshProUGUI text;
	[SerializeField] private GameObject[]    foods;
	[SerializeField] private Sprite          Stage1Sprite;
	[SerializeField] private Sprite          Stage2Sprite;
	[SerializeField] private Sprite          Stage3Sprite;
	[SerializeField] private Sprite          Stage4Sprite;
	[SerializeField] private Sprite          wonSprite;

	private                  int   clicks       = 0;
	[SerializeField] private int   neededClicks = 10;
	[SerializeField] private float clickDepreciationPerSecond = 0;
	private float timeSinceLastDepreciation = 0;

	private float timeFried = 0;
	[SerializeField] private float timeToFry = 10;
	[SerializeField] private GameObject fryingPan;
	
	[SerializeField] private GameObject plate;
	
		

	private void Start() {
		allowedFoodCount = foods.Length;
		NextStage();
	}
	
    private void OnCollisionEnter2D(Collision2D other) {
	    StageOneMiniGame(other);
	    StageFourMiniGame(other);
    }
    
    private void OnMouseDown()=> StageTwoMiniGame();

    private void StageFourMiniGame(Collision2D other) {
	    if(stage != 4 || other.gameObject != plate) return;
		NextStage();
    }

    private void StageOneMiniGame(Collision2D other) {
	    if(stage != 1) return;
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

    private void Update() => CheckClickDepreciation();
    
    private void OnCollisionStay2D(Collision2D other) => StageThreeMiniGame(other);

    private void StageThreeMiniGame(Collision2D other) {
	    print(stage + " " + other.gameObject.name);
	    if (stage != 3 || other.gameObject != fryingPan) return;
	    timeFried += Time.deltaTime;
	    print(timeFried);
	    if (timeFried >= timeToFry) {
		    timeFried = 0;
		    NextStage();
	    }
    }

    private void CheckClickDepreciation() {
	    if (stage != 2) return;
	    timeSinceLastDepreciation += Time.deltaTime;
	    if (timeSinceLastDepreciation >= clickDepreciationPerSecond) {
		    if(clicks > 0) clicks--;
		    timeSinceLastDepreciation = 0;
	    }
    }

    private void StageTwoMiniGame() {
	    if (stage != 2) return;
	    clicks++;
	    print(clicks);
	    if(clicks >= neededClicks) NextStage();
    }

    private void Win() {
	    text.text = "You won!";
	    GetComponent<SpriteRenderer>().sprite = wonSprite;
	    plate.SetActive(false);
	    StartCoroutine(Return(5));
    }

    private IEnumerator Return(int time) {
	    yield return new WaitForSeconds(time);
	    SceneManager.LoadSceneAsync("StreetScene");
    }
    
    private void NextStage() {
	    stage++;
	    if (stage > stages) {
		    Win();
		    StreetSceneManager.instance.GiveReward();
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
			    text.text                             = "Click to knead!";
			    break;
		    case 3:
			    GetComponent<SpriteRenderer>().sprite = Stage3Sprite;
			    fryingPan.SetActive(true);
			    text.text                             = "Fry!";
			    break;
		    case 4:
			    GetComponent<SpriteRenderer>().sprite = Stage4Sprite;
			    fryingPan.SetActive(false);
			    text.text                  = "Plate!";
			    plate.SetActive(true);
			    break;
	    }
	    addedFoods       = new string[] { };
    }
}
