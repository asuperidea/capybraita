using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[ System.Serializable]
public struct MinigameData {
	[SerializeField] public string gameName;
	[SerializeField] public string description;
	[SerializeField] public string sceneName;
	[SerializeField] public int    reward;
}

public class StreetSceneManager : MonoBehaviour {
	
	[SerializeField] private MinigameData    aretaMinigame;
	[SerializeField] private MinigameData    gamblingEvent;
	[SerializeField] private GameObject      MinigamePanel;
	[SerializeField] private TextMeshProUGUI gameName;
	[SerializeField] private TextMeshProUGUI gameDescription;
	[SerializeField] private TextMeshProUGUI reward;
	[SerializeField] private GameObject      deathPanel;
	
	private MinigameData currentMinigame;

	public static StreetSceneManager instance;

	public void Die() {
		deathPanel.SetActive(true);
	}

	public void Reset() {
		deathPanel.SetActive(false);
		PlayerManager.instance.Restart();
		SceneManager.LoadScene("StartScene");
	}
	
	public void Quit() => Application.Quit();

	private void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}
	
	public void Eat(int amount) => PlayerManager.instance.Eat(amount);
	
    public void OpenMinigamePanel(int index) {
	    MinigameData data;
	    switch (index) {
		    case 0:
			    data = aretaMinigame;
			    break;
		    case 1:
			    data = gamblingEvent;
			    break;
		    default:
			    return;
	    }
	    currentMinigame = data;
	    MinigamePanel.SetActive(true);
	    gameName.text = data.gameName;
	    gameDescription.text = data.description;
	    reward.text = "Reward: " + data.reward.ToString() + " mangoes";
    }

    public void StartMinigame() {
	    SceneManager.LoadSceneAsync(currentMinigame.sceneName);
	    MinigamePanel.SetActive(false);
	    return;
    }
    
    public void GiveReward() => PlayerManager.instance.ChangeMangoes(currentMinigame.reward);
    
    public void CloseMinigamePanel() => MinigamePanel.SetActive(false);
}
