using UnityEngine;

namespace HS.Camera
{
    public class ConstOrthographicCamera : MonoBehaviour
    {
        [SerializeField] private Vector2 _defaultResolution = new Vector2(1600, 900);

        private UnityEngine.Camera _camera;

        private float _initialSize;
        private float _targetAspect;
        private float _prevCameraAspect;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _camera = GetComponent<UnityEngine.Camera>();

            _initialSize = _camera.orthographicSize;
            _targetAspect = _defaultResolution.x / _defaultResolution.y;
            _prevCameraAspect = _camera.aspect;
        }

        private void Update()
        {
            RefreshCamera();
        }

        private void RefreshCamera()
        {
            if (Mathf.Approximately(_camera.aspect, _prevCameraAspect))
            {
                return;
            }
            _prevCameraAspect = _camera.aspect;

            float constantWidthSize = _initialSize * (_targetAspect / _camera.aspect);
            _camera.orthographicSize = Mathf.Max(constantWidthSize, _initialSize);
        }
    }
}