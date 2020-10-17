using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] private Transform _target;
	[SerializeField] private Vector3 _offset;
	[SerializeField] private float _lookHeight;
	[SerializeField] private float _minZoom;
	[SerializeField] private float _maxZoom;
	[SerializeField] private float _zoomSpeed;
	[SerializeField] private float _hSpeed;
	[SerializeField] private float _vSpeed;

	private float mouseX, mouseY, zoomDelta;

	SphericalCoordinates sc;

	void Start () {
		sc = new SphericalCoordinates(_offset) {
			MaxRadius = _maxZoom,
			MinRadius = _minZoom
		};
	}

	void Update () {

		if (Input.GetMouseButton(1))
		{
			mouseX = Input.GetAxisRaw("Mouse X") * _hSpeed * Time.deltaTime;
			mouseY = Input.GetAxisRaw("Mouse Y") * _vSpeed * Time.deltaTime;

			_offset = sc.Rotate(mouseX, mouseY);
		}

		zoomDelta = Input.GetAxisRaw("Mouse ScrollWheel") * _zoomSpeed;
		if (zoomDelta != 0) {
			_offset = sc.Zoom(zoomDelta);
		}
	}
	
	void LateUpdate () {
		transform.position = _target.position + _offset;

		transform.LookAt(_target.position + Vector3.up * _lookHeight);
	}

}
