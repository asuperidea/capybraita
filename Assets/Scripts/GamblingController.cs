using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamblingController : MonoBehaviour
{
	private                  Animator       animator;
	[SerializeField] private TMP_InputField inputField;
	[SerializeField] private bool           rigged;
	[SerializeField] private bool           inAdvantage;
	
	private void Start() => animator = GetComponent<Animator>();
	
	public void Return() {
		SceneManager.LoadScene("StreetScene");
	}	

	public void Gamble() {
		if(!int.TryParse(inputField.text, out int bet)) return;
		if (bet <= 0 || bet > PlayerManager.instance.GetMangoes()) return;
		PlayerManager.instance.ChangeMangoes(-bet);

		if (rigged) {
			if(inAdvantage) {
				PlayerManager.instance.ChangeMangoes(2 * bet);
				animator.SetBool("EndOnHead", true);
				animator.SetTrigger("StartSpinning");
			} else {
				animator.SetBool("EndOnHead", false);
				animator.SetTrigger("StartSpinning");
			}
		} else {
			bool win = Random.value < 0.5f;
			if (win) {
				PlayerManager.instance.ChangeMangoes(2 * bet);
				animator.SetBool("EndOnHead", true);
				animator.SetTrigger("StartSpinning");
			} else {
				animator.SetBool("EndOnHead", false);
				animator.SetTrigger("StartSpinning");
			}
		}

		StartCoroutine(Timer(7));
	}

	private IEnumerator Timer(float time) {
		yield return new WaitForSeconds(time);
		Return();
	}
}

