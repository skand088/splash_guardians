using System;
using System.Collections;
using System.Data.Common;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using TMPro;
using System.Collections.Generic;

namespace splash_guardians
{
    public class PlayerScript : MonoBehaviour
    {
        // Movement fields
        public float MaxSpeed = 10;
        public float Acceleration;
        public Vector2 Direction; // 2D Vector where x and y are between -1 and 1 as floats.
        protected PlayerControls Controls;
        public int AlgaeScore = 0; // initial score counter for the algae collection minigame
        public int TrashScore = 0; // new score counter for the trash collection minigame
        // CollisionCheckerFields
        Rigidbody2D RigidBody;
        public bool TouchingTile;
        public Transform TileChecker;
        public LayerMask TileMask;
        //for score display
        public TMP_Text ScoreText;

        // Animations and flip
        public SpriteRenderer Sprite;
        public CircleCollider2D HeldItem;
        public Vector2 PosHeldItemOffset; 
        public Vector2 NegHeldItemOffset;

        // Colliders
        public BoxCollider2D[] DiverColliders;

        // player's radius
        public float Radius;

        // Called before first frame of the game, called a "unity message"
        void Start()
        {
            Controls = new PlayerControls();
            RigidBody = GetComponent<Rigidbody2D>();
            Sprite = GetComponent<SpriteRenderer>();
            HeldItem = GetComponent<CircleCollider2D>();
            DiverColliders = GetComponents<BoxCollider2D>();
            // ScoreText = GetComponent<TMP_Text>();

            if (HeldItem != null)
            {
                PosHeldItemOffset = HeldItem.offset;
                NegHeldItemOffset = HeldItem.offset;
                NegHeldItemOffset.x = -NegHeldItemOffset.x;
            }

            Direction.x = 0;
            Direction.y = 0;
        }

        //adding a function to detect collosion with the algae object and each collision causes score to increase and for the object to disappear
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Algae collection
            if (other.CompareTag("Algae") && HeldItem.IsTouching(other))
            {
                AlgaeScore++;
                Destroy(other.gameObject);
                Debug.Log("Algae Score: " + AlgaeScore);
                if (ScoreText != null) ScoreText.text = "Score: " + AlgaeScore;
            }

            // Trash collection
            if (other.CompareTag("Trash") && HeldItem.IsTouching(other))
            {
                TrashScore++;
                Destroy(other.gameObject);
                Debug.Log("Trash Score: " + TrashScore);
                if (ScoreText != null) ScoreText.text = "Score: " + TrashScore;
            }
        }

        private void OnMove(InputValue input)
        {
            Direction = input.Get<Vector2>(); // Updated on press or release
            HandleSprite();
        }

        // Updated EVERY FRAME. 
        void Update()
        {
            HandleMovement();
        }

        private void HandleSprite()
        {
            if (Direction.x < 0f)
            {
                Sprite.flipX = true;
                if (HeldItem != null) HeldItem.offset = NegHeldItemOffset;
            }
            else if (Direction.x > 0f)
            {
                Sprite.flipX = false;
                if (HeldItem != null) HeldItem.offset = PosHeldItemOffset;
            }
        }

        private void HandleMovement()
        {
            if (Direction.magnitude != 0) 
            {
                float boost = 1 + Vector2.Angle(Direction, RigidBody.linearVelocity)/180f;
                RigidBody.linearVelocity += Acceleration * boost * Time.deltaTime * Direction;  
            }
            RigidBody.linearVelocity -= (Acceleration / MaxSpeed) * Time.deltaTime * RigidBody.linearVelocity;
        }
    }
}