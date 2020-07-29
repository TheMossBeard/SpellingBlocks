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
    public enum WordDirection
    {
        Up,
        Right,
        Down,
        Left,
        Up_Right,
        Down_Right,
        Down_Left,
        Up_Left,
        Box
    }

    class SearchWord
    {
        public List<SearchLetter> Word { get; set; }
        private SpriteBatch spriteBatch { get; set; }

        public SearchWord(string word, SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            Word = new List<SearchLetter>();
            SearchLetter letter;
            foreach (char c in word)
            {
                letter = new SearchLetter(c, spriteBatch, gameContent);
                Word.Add(letter);
            }
        }

        public void SetWordPosition(SearchWord word, Vector2 position, WordDirection dir)
        {
            switch (dir)
            {
                case WordDirection.Box:
                    {
                        int positionX = 0;
                        foreach (SearchLetter l in word.Word)
                        {
                            l.Position = new Vector2(positionX, position.Y);
                            positionX += 38;
                        }
                        break;
                    }
            }
        }

        public void SetSpriteSize(SearchWord word)
        {
            foreach (SearchLetter l in word.Word)
            {
                l.Size = .5f;
            }
        }

        public void Draw()
        {
            foreach (SearchLetter letter in Word)
            {
                letter.Draw();
            }
        }
    }
}