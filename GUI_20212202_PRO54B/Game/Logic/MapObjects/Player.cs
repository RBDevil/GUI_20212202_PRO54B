﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Logic.MapObjects
{
    public class Player : MapObject, ICollidable
    {
        public int Health { get; set; }
        public int CollisionDisabledUntil { get; set; }
        public float Speed { get; private set; }
        public Rect Rect { get; private set; }

        float acceleration;
        float brake;
        float turningSpeed;

        const int MIRROR_WIDTH = 4;

        public Player(Vector2 position, float startingSpeed, float acceleration, float brake, float turningSpeed)
            : base(position, 0, 0)
        {
            BitmapImage image = new BitmapImage(new Uri(Path.Combine("Resources", "miata_na_green.png"), UriKind.RelativeOrAbsolute));
            Brush = new ImageBrush(image);
            Widht = (int)image.Width;
            Height = (int)image.Height;
            Speed = startingSpeed;
            this.acceleration = acceleration;
            this.brake = brake;
            this.turningSpeed = turningSpeed;
            Health = 1;
            Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
        }

        public void MoveRight()
        {
            // update position
            Position += new Vector2(turningSpeed, 0);
            // update rect
            Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
        }

        public void MoveLeft()
        {
            // update position
            Position += new Vector2(-turningSpeed, 0);
            // update rect
            Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
        }

        public void MoveForward()
        {
            // update position
            Position += new Vector2(0, -acceleration);
            // update rect
            Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
        }

        public void MoveBackward()
        {
            // update position
            Position += new Vector2(0, brake);
            // update rect
            Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
        }

        public void SpeedUp(float speed)
        {
            Speed += speed;
        }

        public override void Update(float playerSpeed)
        {
            // do NOT call base method
            if (Keyboard.IsKeyDown(Key.D))
            {
                MoveRight();
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                MoveLeft();
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                MoveForward();
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                MoveBackward();
            }
        }

        public override void Render(DrawingContext drawingContext)
        {
            if (GameLogic.DebugMode)
            {
                drawingContext.DrawRectangle(Brushes.Red, null, Rect);
            }
            drawingContext.DrawRectangle(Brush, null, new Rect(Position.X, Position.Y, Widht, Height));
        }
    }
}
