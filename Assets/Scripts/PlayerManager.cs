using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    
    private int mangoes = 0;

    private void Awake() {
	    if (instance == null) {
		    instance = this;
	    }
	    else if (instance != this) {
		    Destroy(gameObject);
		    return;
	    }
	    
	    DontDestroyOnLoad(gameObject);
    }
    
    public void ChangeMangoes(int amount) => mangoes += amount;
    
    public int GetMangoes() => mangoes;
}
