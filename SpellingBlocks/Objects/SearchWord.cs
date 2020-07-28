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
        Up_Left,
        Down_Left,
        Down_Right
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
            foreach(char c in word)
            {
                letter = new SearchLetter(c, spriteBatch, gameContent);
                Word.Add(letter);
            }
        }

        public void SetWordPosition(List<SearchLetter> word, Vector2 position, WordDirection dir)
        {
            //call searchletter SetLetterPOsition
        }
    }
}