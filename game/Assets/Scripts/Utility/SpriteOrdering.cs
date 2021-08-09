using UnityEngine;

namespace Utilities.SpriteOrdering
{
    [ExecuteInEditMode]
    public class SpriteOrdering : MonoBehaviour
    {
        [SerializeField] public int _layerOffset;
        
        private Transform _transform;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _transform = transform;
        }

        private void Start()
        {
            _spriteRenderer.sortingOrder = -(int) (_transform.position.y * 10) + _layerOffset;
        }

        private void Update()
        {
            _spriteRenderer.sortingOrder = -(int) (_transform.position.y * 10) + _layerOffset;
        }
    }
}