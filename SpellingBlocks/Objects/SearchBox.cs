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
    class SearchBox
    {
        public List<List<SearchLetter>> DisplayList { get; set; }

        private SpriteBatch spriteBatch { get; set; }

        public Texture2D Sprite { get; set; }

        public SearchBox(SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            Sprite = gameContent.categoryBackground;

            DisplayList = new List<List<SearchLetter>>();
            
        }

        public void Draw()
        {
            for(int ii = 0; ii < DisplayList.Count; ii++)
            {
                foreach(SearchLetter letter in DisplayList[ii])
                {
                    letter.Draw();
                }
            }
        }
    }
}