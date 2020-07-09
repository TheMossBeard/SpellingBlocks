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
            this.spriteBatch = spriteBatch;
        }

  
    }
}