using UnityEngine;
using UnityEngine.SceneManagement;

public class StreetSceneManager : MonoBehaviour
{
    public void OpenScene(string sceneName) {
	    SceneManager.LoadScene(sceneName);
    }
}
