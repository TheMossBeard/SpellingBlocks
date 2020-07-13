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
        string[] wordAnimals = { "SNAKE", "DOG", "ZEBRA", "LION", "ELEPHANT" };
        string[] wordAuto = { "BOAT", "TRAIN", "AIRPLANE", "CAR", "BULLDOZER" };
        string[] wordNature = { "TREE", "FLOWER", "MUSHROOM", "WATERFALL", "APPLE" };

        public Words(GameContent gameContent)
        {
            Word newWord;
            AnimalWordList = new List<Word>();

            newWord = new Word(category[0], wordAnimals[0], gameContent.snake, gameContent);
            AnimalWordList.Add(newWord);
            newWord = new Word(category[0], wordAnimals[1], gameContent.dog, gameContent);
            AnimalWordList.Add(newWord);
            newWord = new Word(category[0], wordAnimals[2], gameContent.zebra, gameContent);
            AnimalWordList.Add(newWord);
            newWord = new Word(category[0], wordAnimals[3], gameContent.lion, gameContent);
            AnimalWordList.Add(newWord);
            newWord = new Word(category[0], wordAnimals[4], gameContent.elephant, gameContent);
            AnimalWordList.Add(newWord);


            AutoWordList = new List<Word>();

            newWord = new Word(category[1], wordAuto[0], gameContent.boat, gameContent);
            AutoWordList.Add(newWord);
            newWord = new Word(category[1], wordAuto[1], gameContent.train, gameContent);
            AutoWordList.Add(newWord);
            newWord = new Word(category[1], wordAuto[2], gameContent.airplane, gameContent);
            AutoWordList.Add(newWord);
            newWord = new Word(category[1], wordAuto[3], gameContent.car, gameContent);
            AutoWordList.Add(newWord);
            newWord = new Word(category[1], wordAuto[4], gameContent.bulldozer, gameContent);
            AutoWordList.Add(newWord);


            NatureWordList = new List<Word>();

            newWord = new Word(category[2], wordNature[0], gameContent.tree, gameContent);
            NatureWordList.Add(newWord);
            newWord = new Word(category[2], wordNature[1], gameContent.flower, gameContent);
            NatureWordList.Add(newWord);
            newWord = new Word(category[2], wordNature[2], gameContent.mushroom, gameContent);
            NatureWordList.Add(newWord);
            newWord = new Word(category[2], wordNature[3], gameContent.waterfall, gameContent);
            NatureWordList.Add(newWord);
            newWord = new Word(category[2], wordNature[4], gameContent.apple, gameContent);
            NatureWordList.Add(newWord);


            WordsLists = new List<List<Word>>();
            WordsLists.Add(NatureWordList);
            WordsLists.Add(AnimalWordList);
            WordsLists.Add(AutoWordList);
        }
    }
}