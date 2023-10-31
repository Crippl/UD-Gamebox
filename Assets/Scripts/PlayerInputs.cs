using UnityEngine;

namespace Player.Inputs
{
    [RequireComponent(typeof(PlayerMovement))]

    public class PlayerInputs : MonoBehaviour
    {
        public const string HorizontalAxis = "Horizontal";
        public const string VerticalAxis = "Vertical";
        private Vector3 movement;
        private PlayerMovement playerMovement;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            float horizontal = Input.GetAxis(HorizontalAxis);
            float vertical = Input.GetAxis(VerticalAxis);

            movement = new Vector3(horizontal, 0, vertical).normalized;
        }

        private void FixedUpdate()
        {
            playerMovement.Move(movement);
        }
    }
}
