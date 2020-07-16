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
        public Vector2 Position { get; set; }
        public char Value { get; set; }
        public bool IsSelected { get; set; }
        public bool IsEmptyBlock { get; set; }
        public Texture2D Sprite { get; set; }
        public Color color { get; set; }
        private SpriteBatch spriteBatch { get; set; }

        public Block(float x, float y, char value, Texture2D sprite, SpriteBatch spriteBatch, GameContent gameContent)
        {
            XPosition = x;
            YPosition = y;
            Position = new Vector2(x, y);
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
            Value = value;
            IsSelected = false;
            IsEmptyBlock = false;
            this.spriteBatch = spriteBatch;
            Sprite = sprite;
            color = Color.White;
        }

        public Block(Block old)
        {
            XPosition = old.XPosition;
            YPosition = old.YPosition;
            Position = new Vector2(XPosition, YPosition);
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
            Value = old.Value;
            IsSelected = old.IsSelected;
            IsEmptyBlock = old.IsEmptyBlock;
            this.spriteBatch = old.spriteBatch;
            Sprite = old.Sprite;
            color = Color.White;
        }

        public void Draw()
        {
            spriteBatch.Draw(Sprite, Position, null, color, 0,
                new Vector2(0, 0), 1f, SpriteEffects.None, 0);
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