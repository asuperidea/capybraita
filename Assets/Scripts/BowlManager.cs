using System;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class BowlManager : MonoBehaviour {
	private       string[] allowedFoods     = {};
	private string[] allowedFoods1     = { "Water", "Flour" };
	private  string[] allowedFoods2     = { "Oil", "Cheese" };
	private       string[] addedFoods       = { };
	private       int      allowedFoodCount = 0;
	private const int      stages           = 2;
	private       int      stage            = 0;
	[SerializeField] private TextMeshProUGUI text;
	
	private void Start() => NextStage();
	
    private void OnCollisionEnter2D(Collision2D other) {
	    if (other.gameObject.CompareTag("Food"))
		{	
			if(allowedFoods.Contains(other.gameObject.name) ) {
				Debug.Log("Is in allowedfoods");
				addedFoods = addedFoods.Append(other.gameObject.name).ToArray();
				Destroy(other.gameObject);
				if (addedFoods.Length == allowedFoodCount) {
					NextStage();
				}
			}
			else // Fail
			{
				Debug.Log("Is not in allowedfoods");
				Destroy(other.gameObject);
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
			    allowedFoods = allowedFoods1;
			    break;
		    case 2:
			    allowedFoods = allowedFoods2;
			    break;
	    }
	    allowedFoodCount = allowedFoods.Length;
	    text.text        = "Add " + string.Join(", ", allowedFoods);
	    addedFoods       = new string[] { };
    }
}
