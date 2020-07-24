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

        public WordSearchController(SpriteBatch spriteBatch, GameContent gameContent, GameState state)
        {
            this.spriteBatch = spriteBatch;
            this.gameContent = gameContent;
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
            Random rand = new Random();
            SearchLetter letter;
            PlayFieldWidth = 10;
            PlayFieldHeight = 8;
            int positionY = 40;
            int random = 0;

            for (int ii = 0; ii < PlayFieldHeight; ii++)
            {
                int positionX = 200;
                for (int jj = 0; jj < PlayFieldWidth; jj++)
                {
                    random = rand.Next(0, 25);
                    letter = new SearchLetter(LetterList[random]);
                    letter.Position = new Vector2(positionX, positionY);
                    letter.Value = '0';
                    CurrentLetter2DArray[jj, ii] = letter;
                    positionX += 64;
                }
                positionY += 64;
            }
            PositionWords(spriteBatch, gameContent);
        }

        public void PositionWords(SpriteBatch spriteBatch, GameContent gameContent)
        {
            bool fail = true;
            while (fail)
            {
                bool up = false;
                bool down = false;
                bool right = false;
                bool left = false;
                //bool up_right = false;
                //bool up_left = false;
                //bool down_left = false;
                //bool down_right = false;

                string word = "snake";
                int wordLength = word.Length;

                Random rand = new Random();
                int cordX = rand.Next(0, PlayFieldWidth);
                int cordY = rand.Next(0, PlayFieldHeight);

                if (PlayFieldWidth - (cordX + wordLength) >= 0)
                    right = true;
                if (cordX - wordLength >= 0)
                    left = true;
                if (cordY - wordLength >= 0)
                    up = true;
                if (PlayFieldHeight - (cordY + wordLength) >= 0)
                    down = true;

                //if (up && right)
                //    up_right = true;
                //if (up && left)
                //    up_left = true;
                //if (down && right)
                //    down_right = true;
                //if (down && left)
                //    down_left = true;

                SearchLetter letter;

                if (up)
                {
                    for (int ii = 0; ii < wordLength; ii++)
                    {
                        letter = FindLetter(spriteBatch, gameContent, word[ii]);
                        letter.Position = CurrentLetter2DArray[cordX, cordY - ii].Position;
                        // letter.color = Color.Red;
                        CurrentLetter2DArray[cordX, cordY - ii] = letter;
                    }
                    fail = false;
                }
                else if (right)
                {
                    for (int ii = 0; ii < wordLength; ii++)
                    {
                        letter = FindLetter(spriteBatch, gameContent, word[ii]);
                        letter.Position = CurrentLetter2DArray[cordX + ii, cordY].Position;
                        //letter.color = Color.Red;
                        CurrentLetter2DArray[cordX + ii, cordY] = letter;
                    }
                    fail = false;
                }
                else if (down)
                {
                    for (int ii = 0; ii < wordLength; ii++)
                    {
                        letter = FindLetter(spriteBatch, gameContent, word[ii]);
                        letter.Position = CurrentLetter2DArray[cordX, cordY + ii].Position;
                        // letter.color = Color.Red;
                        CurrentLetter2DArray[cordX, cordY + ii] = letter;
                    }
                    fail = false;
                }
                else if (left)
                {
                    for (int ii = 0; ii < wordLength; ii++)
                    {
                        letter = FindLetter(spriteBatch, gameContent, word[ii]);
                        letter.Position = CurrentLetter2DArray[cordX - ii, cordY].Position;
                        // letter.color = Color.Red;
                        CurrentLetter2DArray[cordX - ii, cordY] = letter;
                    }
                    fail = false;
                }
            }

        }

        public void Draw()
        {
            for (int ii = 0; ii < PlayFieldHeight; ii++)
            {
                for (int jj = 0; jj < PlayFieldWidth; jj++)
                {
                    CurrentLetter2DArray[jj, ii].Draw();
                }
            }
            //this is temporary, find word cheat
            for (int ii = 0; ii < PlayFieldHeight; ii++)
            {
                for (int jj = 0; jj < PlayFieldWidth; jj++)
                {
                   if(CurrentLetter2DArray[jj, ii].Value != '0')
                    {
                        spriteBatch.Draw(gameContent.spriteA, new Rectangle((int)CurrentLetter2DArray[jj, ii].Position.X + 12, 
                            (int)CurrentLetter2DArray[jj, ii].Position.Y + 12, 12, 12), Color.Red);
                        
                    }
                }
            }
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


