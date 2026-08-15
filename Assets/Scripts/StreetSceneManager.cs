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
	[SerializeField] private GameObject      MinigamePanel;
	[SerializeField] private TextMeshProUGUI gameName;
	[SerializeField] private TextMeshProUGUI gameDescription;
	[SerializeField] private TextMeshProUGUI reward;
	
	private MinigameData currentMinigame;
	
    public void OpenMinigamePanel(int index) {
	    MinigameData data;
	    switch (index) {
		    case 0:
			    data = aretaMinigame;
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
    
    public void CloseMinigamePanel() => MinigamePanel.SetActive(false);
}
