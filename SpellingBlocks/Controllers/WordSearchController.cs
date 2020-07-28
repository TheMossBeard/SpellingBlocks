using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Javax.Net.Ssl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SpellingBlocks.Objects;

namespace SpellingBlocks.Controllers
{

    class WordSearchController
    {
        public List<SearchLetter> LetterList { get; set; }
        public SearchLetter[,] CurrentLetter2DArray { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        public GameContent gameContent { get; set; }
        private int PlayFieldWidth { get; set; }
        private int PlayFieldHeight { get; set; }
        public SearchBox WordBox { get; set; }
        public Drawing Drawable { get; set; }

        const int WORD_COUNT = 7;

        private string[] natureWords = { "tree", "river", "creek", "lake", "hiking", "trail", "camping", "tent", "compass", "cloud",
        "insect", "bug", "smore", "fishing", "snow", "sunset", "sunrise"};
        private string[] animalWords = { "horse", "dog", "cat", "zebra", "tiger", "lion", "bear", "rat", "mouse", "elk", "deer", "moose",
        "cow", "bobcat", "monkey", "snake", "eagle", "hawk", "owl", "crow", "buffalo"};
        private string[] machineWords = { "car", "truck", "train", "fuel", "gear", "tire", "wrench", "airplane", "tractor", "diesel",
            "boat", "ship", "hose", "funnel", "bike", "wheel", "basket", "shovel", "axe" };
        private List<string[]> CategoryWordList { get; set; }

        public WordSearchController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            this.gameContent = gameContent;

            CategoryWordList = new List<string[]>();
            CategoryWordList.Add(natureWords);
            CategoryWordList.Add(animalWords);
            CategoryWordList.Add(machineWords);

            WordBox = new SearchBox(spriteBatch, gameContent);
            Drawable = new Drawing(spriteBatch, gameContent);

            PlayFieldWidth = 10;
            PlayFieldHeight = 8;
            LetterList = new List<SearchLetter>();
            CurrentLetter2DArray = new SearchLetter[PlayFieldWidth, PlayFieldHeight];
            //CreateLetterList(spriteBatch, gameContent);

        }

        //public void CreateLetterList(SpriteBatch spriteBatch, GameContent gameContent)
        //{
            //    SearchLetter letter;
            //    char[] letterValueArray = { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o',
            //    'p','q','r','s','t','u','v','w','x','y','z'};

            //    for (int ii = 0; ii < letterValueArray.Length; ii++)
            //    {
            //        letter = new SearchLetter(letterValueArray[ii], gameContent.SearchList[ii], spriteBatch, gameContent);
            //        LetterList.Add(letter);
            //    }
            //}

            public void CreateWordSearch(SpriteBatch spriteBatch, GameContent gameContent, GameState state)
            {
                int categoryIndex = 0;
                switch (state)
                {
                    case GameState.WordSearchNature:
                        {
                            categoryIndex = 0;
                            break;
                        }
                    case GameState.WordSearchAnimal:
                        {
                            categoryIndex = 1;
                            break;
                        }
                    case GameState.WordSearchMachines:
                        {
                            categoryIndex = 2;
                            break;
                        }
                }


            }

            public List<int> GetWords(int categoryIndex)
            {
                Random rand = new Random();
                List<int> list = new List<int>();
                list.Add(rand.Next(0, CategoryWordList[categoryIndex].Length - 1));
                for (int ii = 1; ii < WORD_COUNT; ii++)
                {
                    bool check = true;
                    int tmp = rand.Next(0, CategoryWordList[categoryIndex].Length - 1);
                    while (check)
                    {
                        for (int jj = 0; jj < list.Count; jj++)
                        {
                            if (tmp != list[jj])
                                check = false;
                            else
                            {
                                check = true;
                                jj = list.Count;
                            }
                        }
                        if (check)
                            tmp = rand.Next(0, CategoryWordList[categoryIndex].Length - 1);
                    }
                    list.Add(tmp);
                }
                return list;
            }

            public void DrawSelection(SpriteBatch spriteBatch, GameContent gameContent, Rectangle touchBox)
            {
                foreach (SearchLetter letter in CurrentLetter2DArray)
                {
                    if (HitTest(letter.HitBox, touchBox))
                        letter.IsSelected = true;
                }
            }

            public bool HitTest(Rectangle r1, Rectangle r2)
            {
                if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
                    return true;
                else
                    return false;
            }

            public void PositionWords(SpriteBatch spriteBatch, GameContent gameContent, string word)
            {

            }

            public bool CheckPlacement(int cordX, int cordY, string word, int direction)
            {
                return true;
            }

            public void Touch(Rectangle touchBox, GameContent gameContent, SpriteBatch spriteBatch)
            {
                Drawable.DrawUpdate(touchBox, gameContent, spriteBatch);
            }

            public void Release()
            {
            }

            public void Draw()
            {
                WordBox.Draw();
                for (int ii = 0; ii < PlayFieldHeight; ii++)
                {
                    for (int jj = 0; jj < PlayFieldWidth; jj++)
                    {
                        CurrentLetter2DArray[jj, ii].Draw();
                    }
                }
                Drawable.Draw();
            }
        }
    }


