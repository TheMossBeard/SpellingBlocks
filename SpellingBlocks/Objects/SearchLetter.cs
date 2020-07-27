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
    class SearchLetter
    {
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public Rectangle HitBox { get; set; }
        public Vector2 Position { get; set; }
        public char Value { get; set; }
        public bool IsSelected { get; set; }
        public Texture2D Sprite { get; set; }
        public Color Color { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        public float Size { get; set; }

        public SearchLetter(char value, Texture2D sprite, SpriteBatch spriteBatch, GameContent gameContent)
        {
            Value = value;
            IsSelected = false;
            this.spriteBatch = spriteBatch;
            Sprite = sprite;
            Color = Color.White;
            Size = 1f;
        }
        //need to impliment HITBOX for selecting...
        public SearchLetter(SearchLetter letter)
        {
            Value = letter.Value;
            IsSelected = false;
            this.spriteBatch = letter.spriteBatch;
            Sprite = letter.Sprite;
            Color = Color.White;
            Size = 1f;
        }

        public void Draw()
        {
            spriteBatch.Draw(Sprite, Position, null, Color, 0,
                new Vector2(0, 0), Size, SpriteEffects.None, 0);
        }
    }
}