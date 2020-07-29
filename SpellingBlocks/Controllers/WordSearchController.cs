﻿using System;
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
using RandomExtensions;
using SpellingBlocks.Objects;
using Vector2Extensions;

namespace SpellingBlocks.Controllers
{

    class WordSearchController
    {
        public SearchLetter[,] CurrentLetter2DArray { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        public GameContent gameContent { get; set; }
        private int PlayFieldWidth { get; set; }
        private int PlayFieldHeight { get; set; }
        public SearchBox WordBox { get; set; }
        public List<SearchWord> WordList { get; set; }
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

            CurrentLetter2DArray = new SearchLetter[PlayFieldWidth, PlayFieldHeight];
            PopulateNonWords(spriteBatch, gameContent);
        }

        public void PopulateNonWords(SpriteBatch spriteBatch, GameContent gameContent)
        {
            char[] letterValueArray = { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o',
                'p','q','r','s','t','u','v','w','x','y','z'};
            SearchLetter letter;
            Random random = new Random();
            int randomIndex;
            int positionY = 8;

            for (int ii = 0; ii < PlayFieldWidth; ii++)
            {
                int positionX = 300;
                for (int jj = 0; jj < PlayFieldHeight; jj++)
                {
                    randomIndex = random.Next(0, 26);
                    letter = new SearchLetter(letterValueArray[randomIndex], new Vector2(positionX, positionY), spriteBatch, gameContent);
                    letter.Value = '0';
                    CurrentLetter2DArray[ii, jj] = letter;
                    positionX += 64;
                }
                positionY += 64;
            }
        }

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

            //populate words and wordbox
            List<int> wordIndexList = GetWords(categoryIndex);
            WordList = new List<SearchWord>();
            SearchWord word;
            Vector2 pos = new Vector2(0, 64);
            foreach (int ii in wordIndexList)
            {
                word = new SearchWord(CategoryWordList[categoryIndex][ii], spriteBatch, gameContent);
                WordList.Add(word);
                word.SetWordPosition(word, pos, WordDirection.Box);
                WordBox.DisplayList.Add(word);
                pos = new Vector2(0, pos.Y += 64);
            }
            //got wordbox correct, play board is full, need to check for word positions and set them etc


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

        public List<WordDirection> CheckDirections(SearchWord word, Vector2 position)
        {
            List<WordDirection> directionTrueList = new List<WordDirection>();

            if (position.Y - word.Word.Count >= 0)
                directionTrueList.Add(WordDirection.Up);
            if (position.X + word.Word.Count <= PlayFieldWidth)
                directionTrueList.Add(WordDirection.Right);
            if (position.Y + word.Word.Count <= PlayFieldHeight)
                directionTrueList.Add(WordDirection.Down);
            if (position.X - word.Word.Count >= 0)
                directionTrueList.Add(WordDirection.Left);

            if (directionTrueList.Contains(WordDirection.Up) && directionTrueList.Contains(WordDirection.Right))
                directionTrueList.Add(WordDirection.Up_Right);
            if (directionTrueList.Contains(WordDirection.Down) && directionTrueList.Contains(WordDirection.Right))
                directionTrueList.Add(WordDirection.Down_Right);
            if (directionTrueList.Contains(WordDirection.Down) && directionTrueList.Contains(WordDirection.Left))
                directionTrueList.Add(WordDirection.Down_Left);
            if (directionTrueList.Contains(WordDirection.Up) && directionTrueList.Contains(WordDirection.Left))
                directionTrueList.Add(WordDirection.Up_Left);

            return directionTrueList;
        }

        public List<WordDirection> CheckPosition(List<WordDirection> directionList, SearchWord word, Vector2 position)
        {
            List<WordDirection> positionList = new List<WordDirection>();
            foreach (WordDirection dir in directionList)
            {
                switch (dir)
                {
                    case WordDirection.Up:
                        {
                            bool check = true;
                            for(int ii = 0; ii < word.Word.Count; ii++) //needs work, brain is dead
                            {
                                if (CurrentLetter2DArray[(int)position.X, (int)position.Y + ii].Value != word.Word[ii].Value &&
                                CurrentLetter2DArray[(int)position.X, (int)position.Y + ii].Value != '0')
                                    if (CurrentLetter2DArray[(int)position.X, (int)position.Y + ii].Value != '0')
                                    {
                                        check = false;
                                    }
                            }
                            if (check)
                                positionList.Add(WordDirection.Up);

                            break;
                        }
                    case WordDirection.Right:
                        {

                            break;
                        }
                    case WordDirection.Down:
                        {

                            break;
                        }
                    case WordDirection.Left:
                        {

                            break;
                        }
                    case WordDirection.Up_Right:
                        {

                            break;
                        }
                    case WordDirection.Down_Right:
                        {

                            break;
                        }
                    case WordDirection.Down_Left:
                        {

                            break;
                        }
                    case WordDirection.Up_Left:
                        {

                            break;
                        }
                }
            }

            return positionList;
        }

        public Vector2 GetRandomPosition()
        {
            Random rand = new Random();
            Vector2 random = new Vector2(rand.Next(0, PlayFieldWidth + 1), rand.Next(0, PlayFieldHeight + 1));
            return random;
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


