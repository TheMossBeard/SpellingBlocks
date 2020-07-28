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
        public float Size { get; set; } = 1f;

        public SearchLetter(char value, SpriteBatch spriteBatch, GameContent gameContent)
        {
            Value = value;
            IsSelected = false;
            this.spriteBatch = spriteBatch;
            Sprite = FindLetterSprite(value, gameContent);
            Color = Color.White;            
        }

        public void SetLetterPosition(Vector2 position, bool word)
        {
            Position = position;
            if (word)
                HitBox = new Rectangle((int)position.X, (int)position.Y, 48, 48);
        }

        public void Draw()
        {
            spriteBatch.Draw(Sprite, Position, null, Color, 0,
                new Vector2(0, 0), Size, SpriteEffects.None, 0);
        }

        public Texture2D FindLetterSprite(char value, GameContent gameContent)
        {
            switch (value)
            {
                case 'a':
                    {
                        return gameContent.SearchList[0];
                    }
                case 'b':
                    {
                        return gameContent.SearchList[1];
                    }
                case 'c':
                    {
                        return gameContent.SearchList[2];
                    }
                case 'd':
                    {
                        return gameContent.SearchList[3];
                    }
                case 'e':
                    {
                        return gameContent.SearchList[4];
                    }
                case 'f':
                    {
                        return gameContent.SearchList[5];
                    }
                case 'g':
                    {
                        return gameContent.SearchList[6];
                    }
                case 'h':
                    {
                        return gameContent.SearchList[7];
                    }
                case 'i':
                    {
                        return gameContent.SearchList[8];
                    }
                case 'j':
                    {
                        return gameContent.SearchList[9];
                    }
                case 'k':
                    {
                        return gameContent.SearchList[10];
                    }
                case 'l':
                    {
                        return gameContent.SearchList[11];
                    }
                case 'm':
                    {
                        return gameContent.SearchList[12];
                    }
                case 'n':
                    {
                        return gameContent.SearchList[13];
                    }
                case 'o':
                    {
                        return gameContent.SearchList[14];
                    }
                case 'p':
                    {
                        return gameContent.SearchList[15];
                    }
                case 'q':
                    {
                        return gameContent.SearchList[16];
                    }
                case 'r':
                    {
                        return gameContent.SearchList[17];
                    }
                case 's':
                    {
                        return gameContent.SearchList[18];
                    }
                case 't':
                    {
                        return gameContent.SearchList[19];
                    }
                case 'u':
                    {
                        return gameContent.SearchList[20];
                    }
                case 'v':
                    {
                        return gameContent.SearchList[21];
                    }
                case 'w':
                    {
                        return gameContent.SearchList[22];
                    }
                case 'x':
                    {
                        return gameContent.SearchList[23];
                    }
                case 'y':
                    {
                        return gameContent.SearchList[24];
                    }
                case 'z':
                    {
                        return gameContent.SearchList[25];
                    }
            }
            return gameContent.SearchList[0];
        }
    }
}