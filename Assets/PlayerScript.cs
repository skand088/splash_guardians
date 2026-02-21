using System;
using System.Collections;
using System.Data.Common;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace splash_guardians
{
    public class PlayerScript : MonoBehaviour
    {
        // Movement fields
        public float MaxSpeed = 10;
        public float Acceleration;
        public Vector2 Direction; // 2D Vector where x and y are between -1 and 1 as floats.
        protected PlayerControls Controls;
        public int AlgaeScore = 0; //initial score counter for the algae collection minigame
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

            Direction.x = 0;
            Direction.y = 0;
        }

        //adding a function to detect collosion with the algae object and each collision causes score to increase and for the object to disappear
        private void OnTriggerEnter2D(Collider2D algae_object)
        {
            //check if it an algae object
            if (algae_object.CompareTag("Algae"))
            {
                AlgaeScore++; //increase score
                Destroy(algae_object.gameObject); //delete algae object
                Debug.Log("Score: " + AlgaeScore); //show in console for now 
            }
        }

        private void OnMove(InputValue input)
        {
            Direction = input.Get<Vector2>(); // Updated on press or release
        }

        // Updated EVERY FRAME. 
        void Update()
        {
            // Chekcs whether we are touching a tile -- Currently unused
            //TouchingTile = Physics2D.OverlapCircle(TileChecker.position, Radius, TileMask);

            // Guys this took WAY too much experimentation please appreciate how the movement feels :)
            if (Direction.magnitude != 0) 
            {
                float boost = 1 + Vector2.Angle(Direction, RigidBody.linearVelocity)/180f;
                RigidBody.linearVelocity += Acceleration * boost * Time.deltaTime * Direction;  
            }
            RigidBody.linearVelocity -= (Acceleration / MaxSpeed) * Time.deltaTime * RigidBody.linearVelocity;
        }
    }
}

 