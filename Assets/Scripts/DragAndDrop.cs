using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class DragAndDrop : MonoBehaviour {
	private Camera  cam;
	private Vector3 offset;
	private bool    dragging;

	void Awake() => cam = Camera.main;

	void OnMouseDown() {
		offset = transform.position - cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		dragging = true;
	}

	void Update() {
		if(dragging) transform.position = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + offset;
	}

	void OnMouseUp() => dragging = false;

	Vector3 WorldPos() {
		var p = cam.ScreenToWorldPoint(Input.mousePosition);
		p.z = transform.position.z;
		return p;
	}
}
