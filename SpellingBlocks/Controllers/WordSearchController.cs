﻿using System;
using System.Collections.Generic;
using System.IO;
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
using SQLite;
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
        private MenuButton HomeButton { get; set; }
        public bool Won { get; set; }
        private Texture2D WinnerSplash { get; set; }
        private MenuButton NewButton { get; set; }
        private bool Missed { get; set; }
        private SqliteDB DB { get; set; }
        private Texture2D Background { get; set; }

        const int WORD_COUNT = 7;

        private List<string[]> CategoryWordList { get; set; }

        public WordSearchController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            this.gameContent = gameContent;

            DB = new SqliteDB();

            HomeButton = new MenuButton(new Vector2(16, 16), "HomeButton", gameContent.home, spriteBatch, gameContent);
            WinnerSplash = gameContent.congrats;

            WordBox = new SearchBox(spriteBatch, gameContent);
            Drawable = new Drawing(spriteBatch, gameContent);
            Background = gameContent.wordseachBackground;

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
            int positionY = 32;

            for (int ii = 0; ii < PlayFieldHeight; ii++)
            {
                int positionX = 364; 
                for (int jj = 0; jj < PlayFieldWidth; jj++)
                {
                    randomIndex = random.Next(0, 26);
                    letter = new SearchLetter(letterValueArray[randomIndex], new Vector2(positionX, positionY), spriteBatch, gameContent);
                    letter.Value = '0';
                    CurrentLetter2DArray[jj, ii] = letter;
                    positionX += 64;
                }
                positionY += 64;
            }
        }

        public void CreateWordSearch(SpriteBatch spriteBatch, GameContent gameContent, GameState state)
        {
            List<int> idList = new List<int>();
            CurrentLetter2DArray = new SearchLetter[PlayFieldWidth, PlayFieldHeight];
            WordList = new List<SearchWord>();
            WordBox = new SearchBox(spriteBatch, gameContent);
            Drawable = new Drawing(spriteBatch, gameContent);
            NewButton = new MenuButton();
            Missed = false;
            Won = false;

            PopulateNonWords(spriteBatch, gameContent);

            int IdMax = 0;
            switch (state)
            {
                case GameState.WordSearchNature:
                    {
                        IdMax = 24;
                        break;
                    }
                case GameState.WordSearchAnimal:
                    {
                        IdMax = 49;
                        break;
                    }
                case GameState.WordSearchMachines:
                    {
                        IdMax = 74;
                        break;
                    }
            }

            idList = GetRandomIDs(IdMax);

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Words.db3");
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<WordDB>();

            SearchWord word;
            SearchWord boxWord;
            Vector2 pos = new Vector2(16, 130);
            foreach (int ii in idList)
            {
                word = new SearchWord(table.ElementAt(ii).Word, spriteBatch, gameContent);
                boxWord = new SearchWord(table.ElementAt(ii).Word, spriteBatch, gameContent);
                WordList.Add(word);
                boxWord.SetWordPosition(boxWord, pos, WordDirection.Box, CurrentLetter2DArray);
                WordBox.DisplayList.Add(boxWord);
                pos = new Vector2(16, pos.Y += 48);
            }
            db.Close();

            foreach (SearchWord searchword in WordList)
            {
                List<WordDirection> possiblePositions = new List<WordDirection>();
                pos = GetRandomPosition();
                possiblePositions = CheckControl(searchword, pos);
                while (possiblePositions.Count < 1)
                {
                    pos = GetRandomPosition();
                    possiblePositions = CheckControl(searchword, pos);
                }
                SetWord(searchword, pos, possiblePositions, CurrentLetter2DArray);
            }
        }

        public List<WordDirection> CheckControl(SearchWord word, Vector2 pos)
        {
            List<WordDirection> possiblePositions = new List<WordDirection>();
            possiblePositions = CheckDirections(word, pos);
            possiblePositions = CheckPosition(possiblePositions, word, pos);
            return possiblePositions;
        }

        public List<int> GetRandomIDs(int categoryIndex)
        {
            Random rand = new Random();
            List<int> list = new List<int>();
            list.Add(rand.Next(categoryIndex - 24, categoryIndex + 1));
            for (int ii = 1; ii < WORD_COUNT; ii++)
            {
                bool check = true;
                int tmp = rand.Next(categoryIndex - 24, categoryIndex + 1);
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
                        tmp = rand.Next(categoryIndex - 24, categoryIndex + 1);
                }
                list.Add(tmp);
            }
            return list;
        }

        public void DrawSelection(SpriteBatch spriteBatch, GameContent gameContent, Rectangle touchBox)
        {
            foreach (SearchWord word in WordList)
            {
                for (int ii = 0; ii < word.Word.Count; ii++)
                {
                    if (HitTest(word.Word[ii].HitBox, touchBox))
                        word.Word[ii].IsSelected = true;
                }
            }
            foreach (SearchLetter letter in CurrentLetter2DArray)
            {
                if (HitTest(letter.HitBox, touchBox) && letter.Value == '0')
                    Missed = true;
            }
        }

        public bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
                return true;
            else
                return false;
        }

        public void SetWord(SearchWord word, Vector2 position, List<WordDirection> directionList, SearchLetter[,] currentLetterArray)
        {
            Random rand = new Random();
            int option = rand.Next(0, directionList.Count);
            word.SetWordPosition(word, position, directionList[option], currentLetterArray);
        }

        public List<WordDirection> CheckDirections(SearchWord word, Vector2 position)
        {
            List<WordDirection> directionTrueList = new List<WordDirection>();

            if (position.Y - word.Word.Count > 0)
                directionTrueList.Add(WordDirection.Up);
            if (position.X + word.Word.Count < PlayFieldWidth)
                directionTrueList.Add(WordDirection.Right);
            if (position.Y + word.Word.Count < PlayFieldHeight)
                directionTrueList.Add(WordDirection.Down);
            if (position.X - word.Word.Count > 0)
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
                            bool check = false;
                            for (int ii = 0; ii < word.Word.Count; ii++)
                            {
                                if (CurrentLetter2DArray[(int)position.X, (int)position.Y - ii].Value == '0')
                                    check = true;
                                else if (CurrentLetter2DArray[(int)position.X, (int)position.Y - ii].Value == word.Word[ii].Value)
                                    check = true;
                                else
                                {
                                    check = false;
                                    ii = word.Word.Count;
                                }
                            }
                            if (check)
                                positionList.Add(WordDirection.Up);
                            break;
                        }
                    case WordDirection.Right:
                        {
                            bool check = false;
                            for (int ii = 0; ii < word.Word.Count; ii++)
                            {
                                if (CurrentLetter2DArray[(int)position.X + ii, (int)position.Y].Value == '0')
                                    check = true;
                                else if (CurrentLetter2DArray[(int)position.X + ii, (int)position.Y].Value == word.Word[ii].Value)
                                    check = true;
                                else
                                {
                                    check = false;
                                    ii = word.Word.Count;
                                }
                            }
                            if (check)
                                positionList.Add(WordDirection.Right);
                            break;
                        }
                    case WordDirection.Down:
                        {
                            bool check = false;
                            for (int ii = 0; ii < word.Word.Count; ii++)
                            {
                                if (CurrentLetter2DArray[(int)position.X, (int)position.Y + ii].Value == '0')
                                    check = true;
                                else if (CurrentLetter2DArray[(int)position.X, (int)position.Y + ii].Value == word.Word[ii].Value)
                                    check = true;
                                else
                                {
                                    check = false;
                                    ii = word.Word.Count;
                                }
                            }
                            if (check)
                                positionList.Add(WordDirection.Down);
                            break;
                        }
                    case WordDirection.Left:
                        {
                            bool check = false;
                            for (int ii = 0; ii < word.Word.Count; ii++)
                            {
                                if (CurrentLetter2DArray[(int)position.X - ii, (int)position.Y].Value == '0')
                                    check = true;
                                else if (CurrentLetter2DArray[(int)position.X - ii, (int)position.Y].Value == word.Word[ii].Value)
                                    check = true;
                                else
                                {
                                    check = false;
                                    ii = word.Word.Count;
                                }
                            }
                            if (check)
                                positionList.Add(WordDirection.Left);
                            break;
                        }
                    case WordDirection.Up_Right:
                        {
                            bool check = false;
                            for (int ii = 0; ii < word.Word.Count; ii++)
                            {
                                if (CurrentLetter2DArray[(int)position.X + ii, (int)position.Y - ii].Value == '0')
                                    check = true;
                                else if (CurrentLetter2DArray[(int)position.X + ii, (int)position.Y - ii].Value == word.Word[ii].Value)
                                    check = true;
                                else
                                {
                                    check = false;
                                    ii = word.Word.Count;
                                }
                            }
                            if (check)
                                positionList.Add(WordDirection.Up_Right);
                            break;
                        }
                    case WordDirection.Down_Right:
                        {
                            bool check = false;
                            for (int ii = 0; ii < word.Word.Count; ii++)
                            {
                                if (CurrentLetter2DArray[(int)position.X + ii, (int)position.Y + ii].Value == '0')
                                    check = true;
                                else if (CurrentLetter2DArray[(int)position.X + ii, (int)position.Y + ii].Value == word.Word[ii].Value)
                                    check = true;
                                else
                                {
                                    check = false;
                                    ii = word.Word.Count;
                                }
                            }
                            if (check)
                                positionList.Add(WordDirection.Down_Right);
                            break;
                        }
                    case WordDirection.Down_Left:
                        {
                            bool check = false;
                            for (int ii = 0; ii < word.Word.Count; ii++)
                            {
                                if (CurrentLetter2DArray[(int)position.X - ii, (int)position.Y + ii].Value == '0')
                                    check = true;
                                else if (CurrentLetter2DArray[(int)position.X - ii, (int)position.Y + ii].Value == word.Word[ii].Value)
                                    check = true;
                                else
                                {
                                    check = false;
                                    ii = word.Word.Count;
                                }
                            }
                            if (check)
                                positionList.Add(WordDirection.Down_Left);
                            break;
                        }
                    case WordDirection.Up_Left:
                        {
                            bool check = false;
                            for (int ii = 0; ii < word.Word.Count; ii++)
                            {
                                if (CurrentLetter2DArray[(int)position.X - ii, (int)position.Y - ii].Value == '0')
                                    check = true;
                                else if (CurrentLetter2DArray[(int)position.X - ii, (int)position.Y - ii].Value == word.Word[ii].Value)
                                    check = true;
                                else
                                {
                                    check = false;
                                    ii = word.Word.Count;
                                }
                            }
                            if (check)
                                positionList.Add(WordDirection.Up_Left);
                            break;
                        }
                }
            }
            return positionList;
        }

        public Vector2 GetRandomPosition()
        {
            Random rand = new Random();
            int randX = rand.Next(0, PlayFieldWidth);
            int randY = rand.Next(0, PlayFieldHeight);
            Vector2 random = new Vector2(randX, randY);
            return random;
        }

        public void Touch(Rectangle touchBox, GameContent gameContent, SpriteBatch spriteBatch)
        {
            Drawable.DrawUpdate(touchBox, gameContent, spriteBatch);
        }

        public void Release()
        {
            if (CheckSelected() && !Missed)
                Drawable.NewDraw();
            else
                Drawable.ClearCurrentDraw();

            Missed = false;
            if (WordBox.DisplayList.Count == 0)
                Won = true;
        }

        public bool CheckSelected()
        {
            bool check = false;
            foreach (SearchWord word in WordList)
            {
                if (word.WordIsSelected(word) && !Missed)
                {
                    check = true;
                    List<char> wordValues = word.GetValues(word);
                    for (int ii = 0; ii < WordBox.DisplayList.Count; ii++)
                    {
                        List<char> boxValues = WordBox.DisplayList[ii].GetValues(WordBox.DisplayList[ii]);
                        bool equal = true;
                        if (boxValues.Count == wordValues.Count)
                        {
                            for (int jj = 0; jj < boxValues.Count; jj++)
                            {
                                if (boxValues[jj] != wordValues[jj])
                                {
                                    jj = wordValues.Count;
                                    equal = false;
                                }
                            }
                            if (equal && !Missed)
                                WordBox.DisplayList.RemoveAt(ii);
                        }
                    }
                }
            }
            foreach (SearchWord word in WordList)
            {
                word.RefershSelected(word);
            }
            return check;
        }

        public GameState HomeButtonUpdate(Rectangle touchBox, GameState state)
        {
            if (HitTest(HomeButton.HitBox, touchBox))
                state = GameState.MainMenu;

            return state;
        }

        public GameState NewGameButton(Rectangle touchBox, GameState state)
        {
            if (HitTest(NewButton.HitBox, touchBox))
                state = GameState.CategoryWord;

            return state;
        }

        public void Draw()
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), null, Color.White, 0,
                new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            WordBox.Draw();
            for (int ii = 0; ii < PlayFieldWidth; ii++)
            {
                for (int jj = 0; jj < PlayFieldHeight; jj++)
                {
                    CurrentLetter2DArray[ii, jj].Draw();
                }
            }
            HomeButton.Draw();
            Drawable.Draw();

            if (Won)
            {
                spriteBatch.Draw(WinnerSplash, new Vector2(256, 144), null, Color.White, 0,
                    new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                NewButton = new MenuButton(new Vector2(480, 448), "NewButton", gameContent.arrorRight, spriteBatch, gameContent);
                NewButton.Draw();
            }
        }
    }
}


