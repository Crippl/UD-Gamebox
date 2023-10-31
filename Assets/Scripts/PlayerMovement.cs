using UnityEngine;

namespace Player.Inputs
{
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        private Rigidbody playerRigidbody;

        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }
        public void Move(Vector3 movement)
        {
            playerRigidbody.AddForce(movement * speed,ForceMode.Impulse);
        }
    }
}