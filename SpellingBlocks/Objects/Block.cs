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
        public bool IsEmptyBlock { get; set; }
        public Texture2D Sprite { get; set; }
        private SpriteBatch spriteBatch { get; set; }


        Color color;

        public Block(float x, float y, string value, Texture2D sprite, SpriteBatch spriteBatch, GameContent gameContent)
        {
            XPosition = x;
            YPosition = y;
            Position = new Vector2(x, y);
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, (int)(64 * 2.5f), (int)(64 * 2.5f));
            Value = value;
            IsSelected = false;
            IsEmptyBlock = false;
            this.spriteBatch = spriteBatch;
            Sprite = sprite;
            color = Color.White;
        }

        public void Draw()
        {
            spriteBatch.Draw(Sprite, Position, null, color, 0,
                new Vector2(0, 0), 2.5f, SpriteEffects.None, 0);
        }


        public void ChangeUnSelect()
        {
            IsSelected = false;
            color = Color.White;
        }
        public void ChangeSelect()
        {
            IsSelected = true;
            color = Color.LightSlateGray;
        }
    }
}