using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
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
    class Words
    {
        public List<Word> AnimalWordList;
        public List<Word> AutoWordList;
        public List<Word> NatureWordList;
        public List<List<Word>> WordsLists { get; set; }

        string[] category = { "ANIMALS", "AUTOMOBILES", "OBJECTS" };
        string[] wordAnimals = { "SNAKE", "DOG", "CAT", "TIGGER", "ELEPHANT"};
        string[] wordAuto = { "BOAT", "TRUCK", "AIRPLANE", "BUS", "TRAIN" };
        string[] wordNature = {  "TREE", "FLOWER", "MUSHROOM", "WATER", "SUN"};

        public Words(GameContent gameContent)
        {
            Word newWord;
            AnimalWordList = new List<Word>();
            foreach(string word in wordAnimals)
            {
                newWord = new Word(category[0], word, gameContent);
                AnimalWordList.Add(newWord);
            }

            AutoWordList = new List<Word>();
            foreach(string word in wordAuto)
            {
                newWord = new Word(category[1], word, gameContent);
                AutoWordList.Add(newWord);
            }

            NatureWordList = new List<Word>();
            foreach(string word in wordNature)
            {
                newWord = new Word(category[2], word, gameContent);
                NatureWordList.Add(newWord);
            }

            WordsLists = new List<List<Word>>();
            WordsLists.Add(NatureWordList);
            WordsLists.Add(AnimalWordList);
            WordsLists.Add(AutoWordList);
        }
    }
}