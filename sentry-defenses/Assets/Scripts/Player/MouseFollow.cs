using UnityEngine;

namespace Player
{
    public class MouseFollow : MonoBehaviour
    {
        public float SmoothTime = 0.25f;
        
        private Transform _transform;
        private Vector3 _targetPosition;

        private Vector3 _velocity;
        
        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _targetPosition = Helpers.GetMouseWorldPosition();
            
            if (Input.GetMouseButtonDown(0))
            {
                _transform.position = _targetPosition;
                return;
            }
            
            _transform.position = Vector3.SmoothDamp(_transform.position, _targetPosition, ref _velocity, SmoothTime);
        }
    }
}
