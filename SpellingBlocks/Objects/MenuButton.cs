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
    class MenuButton
    {
        public Rectangle HitBox { get; set; }
        public Vector2 Position { get; set; }
        public string Value { get; set; }
        public Texture2D Sprite { get; set; }
        private SpriteBatch spriteBatch { get; set; }

        public MenuButton(Vector2 position, string value, Texture2D sprite, SpriteBatch spriteBatch, GameContent gameContent)
        {
            Position = position;
            Value = value;
            Sprite = sprite;
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, 384, 128);
            this.spriteBatch = spriteBatch;
        }

        public void Draw()
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0,
                new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }
    }
}