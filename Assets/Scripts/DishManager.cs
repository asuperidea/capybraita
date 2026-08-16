using UnityEngine;

public class DishManager : MonoBehaviour {
	[SerializeField] private int plateCount;
	[SerializeField] private int dirtPerPlate;

	[SerializeField] private GameObject plate;
	[SerializeField] private GameObject dirt;
	[SerializeField] private float plateRadius = 1.5f;
	[SerializeField] private float dirtRadius  = 0.5f;
	
	private void Start() => SpawnPlatesAndDirt();
	

	private void SpawnPlatesAndDirt() {
		
		int cols = Mathf.CeilToInt(Mathf.Sqrt(plateCount));
		int rows = Mathf.CeilToInt(plateCount / (float)cols);
		float cellW = 14f / cols, cellH = 7f / rows;
		
		for (int i = 0; i < plateCount; i++) {
			int cx = i % cols, cy = i / cols;
			float x = -7f + cellW * (cx + 0.5f) + Random.Range(-1f, 1f) * (cellW * 0.5f - plateRadius);
			float y = -3.5f + cellH * (cy + 0.5f) + Random.Range(-1f, 1f) * (cellH * 0.5f - plateRadius);
			var obj = Instantiate(plate, new Vector3(x, y, 0), Quaternion.identity);
			for (int j = 0; j < dirtPerPlate; j++) {
				var angle = (360f / dirtPerPlate) * j + Random.Range(-40f, 40f);
				var scale = (plateRadius - dirtRadius)*Mathf.Sqrt(Random.Range(0f, 1f));
				var dirtObject = Instantiate(dirt,
				            obj.transform);
				dirtObject.transform.position = obj.transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * scale, Mathf.Sin(angle * Mathf.Deg2Rad) * scale, 0);
				dirtObject.transform.localScale = new Vector3(1f / 3f, 1f / 3f, 1f);
			}
		}
	}
}
