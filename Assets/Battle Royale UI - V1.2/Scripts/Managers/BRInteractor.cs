using UnityEngine;

namespace BattleRoyaleUI
{
    [RequireComponent(typeof(Camera))]
    public class BRInteractor : MonoBehaviour
    {
        [Header("Raycast Features")]
        [SerializeField] private float rayDistance = 5;

        [Header("Raycast Features")]
        [SerializeField] private KeyCode pickupKey;

        private BRItem useableItem;
        private Camera _camera;

        void Start()
        {
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, rayDistance))
            {
                var brItem = hit.collider.GetComponent<BRItem>();
                if (brItem != null)
                {
                    useableItem = brItem;
                    HighlightCrosshair(true);
                }
                else
                {
                    ClearExaminable();
                }
            }
            else
            {
                ClearExaminable();
            }

            if (useableItem != null)
            {
                if (Input.GetKeyDown(pickupKey))
                {
                    useableItem.ObjectInteraction();
                }
            }
        }

        private void ClearExaminable()
        {
            if (useableItem != null)
            {
                HighlightCrosshair(false);
                useableItem = null;
            }
        }

        void HighlightCrosshair(bool on)
        {
            BRUIManager.instance.HighlightCrosshair(on);
        }
    }
}
