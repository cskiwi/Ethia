using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {
    public Transform target;
    public float walkDistance;
    public float runistance;
    public float height;

    public float xSpeed = 250f;
    public float ySpeed = 120f;
    public float heightDamping = 2f;
    public float rotationDamping = 3f;

    private Transform _myTransform;
    private float _x;
    private float _y;
    private bool _camButtonDown = false;

    void Awake() {
        _myTransform = transform;
    }
    void Start() {
        if (target == null)
            Debug.LogWarning("No target found");
        else {
            CameraSetup();
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            _camButtonDown = true;
        }
        if (Input.GetMouseButtonUp(1)) {
            _camButtonDown = false;
        }
    }

    void LateUpdate() {
        if (target != null) {
            if (_camButtonDown) {
                _x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                _y += Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                Quaternion rotation = Quaternion.Euler(_y, _x, 0);
                Vector3 position = rotation * new Vector3(0f, 0f, -walkDistance) + target.position;

                _myTransform.rotation = rotation;
                _myTransform.position = position;
            } else {
                _x = 0;
                _y = 0;

                float wantedRotationAngle = target.eulerAngles.y;
                float wantedHeight = target.position.y + height;

                float currentRotationAngle = _myTransform.eulerAngles.y;
                float currentHeight = _myTransform.position.y;

                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
                currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

                Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

                _myTransform.position = target.position;
                _myTransform.position -= currentRotation * Vector3.forward * walkDistance;

                _myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);

                _myTransform.LookAt(target);
            }
        }
    }

    public void CameraSetup() {
        _myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
        _myTransform.LookAt(target);
    }
}
