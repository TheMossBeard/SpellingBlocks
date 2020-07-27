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
            CreateLetterList(spriteBatch, gameContent);

        }

        public void CreateLetterList(SpriteBatch spriteBatch, GameContent gameContent)
        {
            SearchLetter letter;
            char[] letterValueArray = { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o',
            'p','q','r','s','t','u','v','w','x','y','z'};

            for (int ii = 0; ii < letterValueArray.Length; ii++)
            {
                letter = new SearchLetter(letterValueArray[ii], gameContent.SearchList[ii], spriteBatch, gameContent);
                LetterList.Add(letter);
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

            Random rand = new Random();
            SearchLetter letter;
            PlayFieldWidth = 10;
            PlayFieldHeight = 8;
            int positionY = 40;
            int positionX = 300; //was 200 for centered
            int random;

            for (int ii = 0; ii < PlayFieldHeight; ii++)
            {
                positionX = 300;
                for (int jj = 0; jj < PlayFieldWidth; jj++)
                {
                    random = rand.Next(0, 25);
                    letter = new SearchLetter(LetterList[random])
                    {
                        Position = new Vector2(positionX, positionY),
                        Value = '0'
                    };
                    CurrentLetter2DArray[jj, ii] = letter;
                    positionX += 64;
                }
                positionY += 64;
            }

            List<int> randomWordIndexList = new List<int>();
            randomWordIndexList = GetWords(categoryIndex);

            positionY = 144;
            for (int ii = 0; ii < WORD_COUNT; ii++)
            {
                PositionWords(spriteBatch, gameContent, CategoryWordList[categoryIndex][randomWordIndexList[ii]]);
                string word = CategoryWordList[categoryIndex][randomWordIndexList[ii]];
                List<SearchLetter> boxWord = new List<SearchLetter>();
                positionX = 0;
                for(int jj = 0; jj < word.Length; jj++)
                {
                    SearchLetter boxLetter = FindLetter(spriteBatch, gameContent, word[jj]);
                    boxLetter.Position = new Vector2(positionX, positionY);
                    boxLetter.Size = .5f;
                    boxWord.Add(boxLetter);
                    positionX += 32;
                }
                WordBox.DisplayList.Add(boxWord);
                positionY += 40;
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

        public void PositionWords(SpriteBatch spriteBatch, GameContent gameContent, string word)
        {
            bool fail = true;
            while (fail)
            {
                bool up = false;
                bool down = false;
                bool right = false;
                bool left = false;
                bool up_right = false;
                bool up_left = false;
                bool down_left = false;
                bool down_right = false;

                int wordLength = word.Length;

                Random rand = new Random();
                int cordX = rand.Next(0, PlayFieldWidth);
                int cordY = rand.Next(0, PlayFieldHeight);
                int randomDirection = rand.Next(0, 2);
                if (PlayFieldWidth - (cordX + wordLength) >= 0)
                    right = true;
                if (cordX - wordLength >= 0)
                    left = true;
                if (cordY - wordLength >= 0)
                    up = true;
                if (PlayFieldHeight - (cordY + wordLength) >= 0)
                    down = true;

                if (up && right)
                    up_right = true;
                if (up && left)
                    up_left = true;
                if (down && right)
                    down_right = true;
                if (down && left)
                    down_left = true;

                SearchLetter letter;

                if (up && fail && randomDirection == 0)
                {
                    if (CheckPlacement(cordX, cordY, word, 0))
                    {
                        for (int ii = 0; ii < wordLength; ii++)
                        {
                            letter = FindLetter(spriteBatch, gameContent, word[ii]);
                            letter.Position = CurrentLetter2DArray[cordX, cordY - ii].Position;
                            CurrentLetter2DArray[cordX, cordY - ii] = letter;
                        }
                        fail = false;
                    }
                }
                if (right && fail && randomDirection == 0)
                {
                    if (CheckPlacement(cordX, cordY, word, 1))
                    {
                        for (int ii = 0; ii < wordLength; ii++)
                        {
                            letter = FindLetter(spriteBatch, gameContent, word[ii]);
                            letter.Position = CurrentLetter2DArray[cordX + ii, cordY].Position;
                            CurrentLetter2DArray[cordX + ii, cordY] = letter;
                        }
                        fail = false;
                    }
                }
                if (down && fail && randomDirection == 0)
                {
                    if (CheckPlacement(cordX, cordY, word, 2))
                    {
                        for (int ii = 0; ii < wordLength; ii++)
                        {
                            letter = FindLetter(spriteBatch, gameContent, word[ii]);
                            letter.Position = CurrentLetter2DArray[cordX, cordY + ii].Position;
                            CurrentLetter2DArray[cordX, cordY + ii] = letter;
                        }
                        fail = false;
                    }
                }
                if (left && fail && randomDirection == 0)
                {
                    if (CheckPlacement(cordX, cordY, word, 3))
                    {
                        for (int ii = 0; ii < wordLength; ii++)
                        {
                            letter = FindLetter(spriteBatch, gameContent, word[ii]);
                            letter.Position = CurrentLetter2DArray[cordX - ii, cordY].Position;
                            CurrentLetter2DArray[cordX - ii, cordY] = letter;
                        }
                        fail = false;
                    }
                }
                if (up_right && fail && randomDirection == 1)
                {
                    if (CheckPlacement(cordX, cordY, word, 4))
                    {
                        for (int ii = 0; ii < wordLength; ii++)
                        {
                            letter = FindLetter(spriteBatch, gameContent, word[ii]);
                            letter.Position = CurrentLetter2DArray[cordX + ii, cordY - ii].Position;
                            CurrentLetter2DArray[cordX + ii, cordY - ii] = letter;
                        }
                        fail = false;
                    }
                }
                if (up_left && fail && randomDirection == 1)
                {
                    if (CheckPlacement(cordX, cordY, word, 5))
                    {
                        for (int ii = 0; ii < wordLength; ii++)
                        {
                            letter = FindLetter(spriteBatch, gameContent, word[ii]);
                            letter.Position = CurrentLetter2DArray[cordX - ii, cordY - ii].Position;
                            CurrentLetter2DArray[cordX - ii, cordY - ii] = letter;
                        }
                        fail = false;
                    }
                }
                if (down_left && fail && randomDirection == 1)
                {
                    if (CheckPlacement(cordX, cordY, word, 6))
                    {
                        for (int ii = 0; ii < wordLength; ii++)
                        {
                            letter = FindLetter(spriteBatch, gameContent, word[ii]);
                            letter.Position = CurrentLetter2DArray[cordX - ii, cordY + ii].Position;
                            CurrentLetter2DArray[cordX - ii, cordY + ii] = letter;
                        }
                        fail = false;
                    }
                }
                if (down_right && fail && randomDirection == 1)
                {
                    if (CheckPlacement(cordX, cordY, word, 7))
                    {
                        for (int ii = 0; ii < wordLength; ii++)
                        {
                            letter = FindLetter(spriteBatch, gameContent, word[ii]);
                            letter.Position = CurrentLetter2DArray[cordX + ii, cordY + ii].Position;
                            CurrentLetter2DArray[cordX + ii, cordY + ii] = letter;
                        }
                        fail = false;
                    }
                }
            }
        }

        public bool CheckPlacement(int cordX, int cordY, string word, int direction)
        {
            switch (direction)
            {
                case 0:
                    {
                        for (int ii = 0; ii < word.Length; ii++)
                        {
                            if (CurrentLetter2DArray[cordX, cordY - ii].Value != '0')
                            {
                                if (CurrentLetter2DArray[cordX, cordY - ii].Value != word[ii])
                                    return false;
                            }
                        }
                        return true;
                    }
                case 1:
                    {
                        for (int ii = 0; ii < word.Length; ii++)
                        {
                            if (CurrentLetter2DArray[cordX + ii, cordY].Value != '0')
                            {
                                if (CurrentLetter2DArray[cordX + ii, cordY].Value != word[ii])
                                    return false;
                            }
                        }
                        return true;
                    }
                case 2:
                    {
                        for (int ii = 0; ii < word.Length; ii++)
                        {
                            if (CurrentLetter2DArray[cordX, cordY + ii].Value != '0')
                            {
                                if (CurrentLetter2DArray[cordX, cordY + ii].Value != word[ii])
                                    return false;
                            }
                        }
                        return true;
                    }
                case 3:
                    {
                        for (int ii = 0; ii < word.Length; ii++)
                        {
                            if (CurrentLetter2DArray[cordX - ii, cordY].Value != '0')
                            {
                                if (CurrentLetter2DArray[cordX - ii, cordY].Value != word[ii])
                                    return false;
                            }
                        }
                        return true;
                    }
                case 4:
                    {
                        for (int ii = 0; ii < word.Length; ii++)
                        {
                            if (CurrentLetter2DArray[cordX + ii, cordY - ii].Value != '0')
                            {
                                if (CurrentLetter2DArray[cordX + ii, cordY - ii].Value != word[ii])
                                    return false;
                            }
                        }
                        return true;
                    }
                case 5:
                    {
                        for (int ii = 0; ii < word.Length; ii++)
                        {
                            if (CurrentLetter2DArray[cordX - ii, cordY - ii].Value != '0')
                            {
                                if (CurrentLetter2DArray[cordX - ii, cordY - ii].Value != word[ii])
                                    return false;
                            }
                        }
                        return true;
                    }
                case 6:
                    {
                        for (int ii = 0; ii < word.Length; ii++)
                        {
                            if (CurrentLetter2DArray[cordX - ii, cordY + ii].Value != '0')
                            {
                                if (CurrentLetter2DArray[cordX - ii, cordY + ii].Value != word[ii])
                                    return false;
                            }
                        }
                        return true;
                    }
                case 7:
                    {
                        for (int ii = 0; ii < word.Length; ii++)
                        {
                            if (CurrentLetter2DArray[cordX + ii, cordY + ii].Value != '0')
                            {
                                if (CurrentLetter2DArray[cordX + ii, cordY + ii].Value != word[ii])
                                    return false;
                            }
                        }
                        return true;
                    }
            }
            return true;
        }

        public void Touch(Rectangle touchBox, GameContent gameContent, SpriteBatch spriteBatch)
        {
            Drawable.DrawUpdate(touchBox, gameContent, spriteBatch);
        }

        public void Release()
        {
            Drawable.NewDraw();
            //Drawable.Clear();
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

        public SearchLetter FindLetter(SpriteBatch spriteBatch, GameContent gameContent, char value)
        {
            SearchLetter letter = new SearchLetter(value, gameContent.SearchList[0], spriteBatch, gameContent);
            switch (value)
            {
                case 'a':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[0], spriteBatch, gameContent);
                        break;
                    }
                case 'b':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[1], spriteBatch, gameContent);
                        break;
                    }
                case 'c':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[2], spriteBatch, gameContent);
                        break;
                    }
                case 'd':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[3], spriteBatch, gameContent);
                        break;
                    }
                case 'e':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[4], spriteBatch, gameContent);
                        break;
                    }
                case 'f':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[5], spriteBatch, gameContent);
                        break;
                    }
                case 'g':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[6], spriteBatch, gameContent);
                        break;
                    }
                case 'h':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[7], spriteBatch, gameContent);
                        break;
                    }
                case 'i':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[8], spriteBatch, gameContent);
                        break;
                    }
                case 'j':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[9], spriteBatch, gameContent);
                        break;
                    }
                case 'k':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[10], spriteBatch, gameContent);
                        break;
                    }
                case 'l':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[11], spriteBatch, gameContent);
                        break;
                    }
                case 'm':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[12], spriteBatch, gameContent);
                        break;
                    }
                case 'n':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[13], spriteBatch, gameContent);
                        break;
                    }
                case 'o':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[14], spriteBatch, gameContent);
                        break;
                    }
                case 'p':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[15], spriteBatch, gameContent);
                        break;
                    }
                case 'q':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[16], spriteBatch, gameContent);
                        break;
                    }
                case 'r':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[17], spriteBatch, gameContent);
                        break;
                    }
                case 's':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[18], spriteBatch, gameContent);
                        break;
                    }
                case 't':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[19], spriteBatch, gameContent);
                        break;
                    }
                case 'u':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[20], spriteBatch, gameContent);
                        break;
                    }
                case 'v':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[21], spriteBatch, gameContent);
                        break;
                    }
                case 'w':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[22], spriteBatch, gameContent);
                        break;
                    }
                case 'x':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[23], spriteBatch, gameContent);
                        break;
                    }
                case 'y':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[24], spriteBatch, gameContent);
                        break;
                    }
                case 'z':
                    {
                        letter = new SearchLetter(value, gameContent.SearchList[25], spriteBatch, gameContent);
                        break;
                    }
            }
            return letter;
        }
    }

}


