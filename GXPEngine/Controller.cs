using System;
using GXPEngine;


    public class Controller : Sprite
    {
        private Player player;
        private int turnSpeed = 1;
        private float lastRotation;
        public Controller(Player pPlayer) : base("sprites/square.png")
        {
            player = pPlayer;
            width = 70;
            height = 20;
            SetOrigin(width/2, height/2);
           
            collider.isTrigger = true;
        }

        void Update()
        {
            x = player.x;
            y = player.y;

        }

        public float GetRotation()
        {
            return rotation;
        }
        
    }
