using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace splash_guardians
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerScript : MonoBehaviour
    {
        // Movement fields
        public float MoveSpeed;
        protected PlayerControls Controls;

        // CollisionCheckerFields
        Rigidbody2D RigidBody;
        public bool TouchingTile;
        public Transform TileChecker;
        public LayerMask TileMask;
        
        // player's radius
        public float Radius;

        // Called before first frame of the game, called a "unity message"
        void Start()
        {
            Controls = new PlayerControls();
            RigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnMove(InputValue input)
        {
            RigidBody.linearVelocity = input.Get<UnityEngine.Vector2>() * MoveSpeed;
        }

        // Updated EVERY FRAME. 
        void Update()
        {
            // Chekcs whether we are touching a tile -- Currently unused
            TouchingTile = Physics2D.OverlapCircle(TileChecker.position, Radius, TileMask);
        }

    }
}

