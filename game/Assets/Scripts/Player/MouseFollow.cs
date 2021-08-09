using UnityEngine;

namespace Player
{
    public class MouseFollow : MonoBehaviour
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
        
        }

        private void Update()
        {
            _transform.position = Helpers.GetMouseWorldPosition();
        }
    }
}
