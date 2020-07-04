using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingBlocks.Objects
{
    class Block
    {
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public Rectangle HitBox { get; set; }

        public Vector2 Position;
        public string Value { get; set; }
        public bool IsSelected { get; set; }
        public Texture2D Sprite { get; set; }
        private SpriteBatch spriteBatch { get; set; }

        public Block(float x, float y, string value, Texture2D sprite, SpriteBatch spriteBatch, GameContent gameContent)
        {
            XPosition = x;
            YPosition = y;
            Position = new Vector2(x, y);
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
            Value = value;
            IsSelected = false;
            this.spriteBatch = spriteBatch;
            Sprite = sprite;
        }

        public void Draw()
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0,
                new Vector2(0, 0), 2.5f, SpriteEffects.None, 0);
        }

        public void Update()
        {
            IsSelected = !IsSelected;
        }

    }
}