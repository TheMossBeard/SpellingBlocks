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
    class Word
    {
        private LetterValue Letters {get; set;}
        public int WordLength { get; private set; }
        public List<LetterValue> Value { get; private set; }
        public string Category { get; private set; }

        public Word(string category, string word, GameContent gameContent)
        {
            Letters = new LetterValue(gameContent);
            WordLength = word.Length;
            Category = category;
            Value = FormatWord(word, Letters);
        }

        private List<LetterValue> FormatWord(string word, LetterValue Letters)
        {
            List<LetterValue> wordLetters = new List<LetterValue>();
            for (int ii = 0; ii < word.Length; ii++)
            {
                for (int jj = 0; jj < Letters.LetterValueList.Count(); jj++)
                {
                    if (word[ii] == Letters.LetterValueList[jj].Value)
                    {
                        wordLetters.Add(Letters.LetterValueList[jj]);
                        jj = Letters.LetterValueList.Count();
                    }
                }
            }
            return wordLetters;
        }
    }
}