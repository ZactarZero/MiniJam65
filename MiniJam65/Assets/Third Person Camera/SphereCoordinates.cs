using UnityEngine;

public class SphericalCoordinates {

	public float MaxVertAngle, MinVertAngle;
	public float MaxRadius, MinRadius;
    private float _radius;
    private float _elevation;
    private float _polar;

    #region Constructors
    public SphericalCoordinates (Vector3 v) : this(v.x, v.y, v.z) {}

	public SphericalCoordinates (float x, float y, float z) {
		CartesianToSpherical (x, y, z);
		MaxVertAngle = 80f * Mathf.PI / 180f;//Degrees to radians
		MinVertAngle = -15f * Mathf.PI / 180f;//Degrees to radians
	}
	#endregion

	public Vector3 Zoom (float amount) {
		_radius -= amount;
		_radius = Mathf.Clamp(_radius, MinRadius, MaxRadius);
		return SphericalToCartesian();
	}

	public Vector3 Rotate (float horizontalAngle, float verticalAngle) {
		_polar -= horizontalAngle;
		_elevation -= verticalAngle;
		_polar = Mathf.Repeat(_polar, 2f * Mathf.PI);
		_elevation = Mathf.Clamp(_elevation, MinVertAngle, MaxVertAngle);
		return SphericalToCartesian();
	}
	
	public void CartesianToSpherical (float x, float y, float z) {
		_radius = Mathf.Sqrt(x * x + y * y + z * z);
		_elevation = Mathf.Asin(y/ _radius);
		_polar = Mathf.Atan(z/x);
	}

	public Vector3 SphericalToCartesian () {
		float a = _radius * Mathf.Cos(_elevation);
		float x = a * Mathf.Cos(_polar);
		float y = _radius * Mathf.Sin(_elevation);
		float z = a * Mathf.Sin(_polar);
		return new Vector3(x, y, z);
	}
	
}
