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
using Java.Security;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingBlocks.Objects
{
    class LetterValue
    {
        public char Value { get; set; }
        public int ID { get; set; }
        public Texture2D Sprite { get; set; }
        public List<LetterValue> LetterValueList { get; set; }

        private char[] values = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
        public LetterValue(GameContent gameContent)
        {
            LetterValue letter;
            LetterValueList = new List<LetterValue>();
            for(int ii = 0; ii < 26; ii++)
            {
                letter = new LetterValue(ii, values[ii], gameContent.SpriteList[ii]);
                LetterValueList.Add(letter);

            }
        }
        public LetterValue(int id, char value, Texture2D sprite)
        {
            ID = id;
            Value = value;
            Sprite = sprite;
        }

    
    }
}