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
    public enum WordDirection
    {
        Up,
        Right,
        Down,
        Left,
        Up_Right,
        Down_Right,
        Down_Left,
        Up_Left,
        Box
    }

    class SearchWord
    {
        const int BASE_POS_Y = 8;
        const int BASE_POS_X = 300;
        const int POS_NEXT = 64;
        public List<SearchLetter> Word { get; set; }
        private SpriteBatch spriteBatch { get; set; }

        public SearchWord(string word, SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            Word = new List<SearchLetter>();
            SearchLetter letter;
            foreach (char c in word)
            {
                letter = new SearchLetter(c, spriteBatch, gameContent);
                Word.Add(letter);
            }
        }

        public void SetWordPosition(SearchWord word, Vector2 position, WordDirection dir, SearchLetter[,] currentLetterArray)
        {
            int cordX;
            int cordY;
            switch (dir)
            {
                case WordDirection.Box:
                    {
                        int positionX = 0;
                        foreach (SearchLetter l in word.Word)
                        {
                            l.Position = new Vector2(positionX, position.Y);
                            positionX += 38;
                        }
                        break;
                    }
                case WordDirection.Up:
                    {
                        cordX = BASE_POS_X + (int)position.X * POS_NEXT;
                        cordY = BASE_POS_Y + (int)position.Y * POS_NEXT;
                        for (int ii = 0; ii < word.Word.Count; ii++)
                        {
                            word.Word[ii].SetLetterPosition(new Vector2(cordX, cordY - (ii * POS_NEXT)), true);
                            currentLetterArray[(int)position.X, (int)position.Y - ii] = word.Word[ii];
                        }
                        break;
                    }
                case WordDirection.Right:
                    {
                        cordX = BASE_POS_X + (int)position.X * POS_NEXT;
                        cordY = BASE_POS_Y + (int)position.Y * POS_NEXT;
                        for (int ii = 0; ii < word.Word.Count; ii++)
                        {
                            word.Word[ii].SetLetterPosition(new Vector2(cordX + (ii * POS_NEXT), cordY), true);
                            currentLetterArray[(int)position.X + ii, (int)position.Y] = word.Word[ii];
                        }
                        break;
                    }
                case WordDirection.Down:
                    {
                        cordX = BASE_POS_X + (int)position.X * POS_NEXT;
                        cordY = BASE_POS_Y + (int)position.Y * POS_NEXT;
                        for (int ii = 0; ii < word.Word.Count; ii++)
                        {
                            word.Word[ii].SetLetterPosition(new Vector2(cordX, cordY + (ii * POS_NEXT)), true);
                            currentLetterArray[(int)position.X, (int)position.Y + ii] = word.Word[ii];
                        }
                        break;
                    }
                case WordDirection.Left:
                    {
                        cordX = BASE_POS_X + (int)position.X * POS_NEXT;
                        cordY = BASE_POS_Y + (int)position.Y * POS_NEXT;
                        for (int ii = 0; ii < word.Word.Count; ii++)
                        {
                            word.Word[ii].SetLetterPosition(new Vector2(cordX - (ii * POS_NEXT), cordY), true);
                            currentLetterArray[(int)position.X - ii, (int)position.Y] = word.Word[ii];
                        }
                        break;
                    }
                case WordDirection.Up_Right:
                    {
                        cordX = BASE_POS_X + (int)position.X * POS_NEXT;
                        cordY = BASE_POS_Y + (int)position.Y * POS_NEXT;
                        for (int ii = 0; ii < word.Word.Count; ii++)
                        {
                            word.Word[ii].SetLetterPosition(new Vector2(cordX + (ii * POS_NEXT), cordY - (ii * POS_NEXT)), true);
                            currentLetterArray[(int)position.X + ii, (int)position.Y - ii] = word.Word[ii];
                        }
                        break;
                    }
                case WordDirection.Down_Right:
                    {
                        cordX = BASE_POS_X + (int)position.X * POS_NEXT;
                        cordY = BASE_POS_Y + (int)position.Y * POS_NEXT;
                        for (int ii = 0; ii < word.Word.Count; ii++)
                        {
                            word.Word[ii].SetLetterPosition(new Vector2(cordX + (ii * POS_NEXT), cordY + (ii * POS_NEXT)), true);
                            currentLetterArray[(int)position.X + ii, (int)position.Y + ii] = word.Word[ii];
                        }
                        break;
                    }
                case WordDirection.Down_Left:
                    {
                        cordX = BASE_POS_X + (int)position.X * POS_NEXT;
                        cordY = BASE_POS_Y + (int)position.Y * POS_NEXT;
                        for (int ii = 0; ii < word.Word.Count; ii++)
                        {
                            word.Word[ii].SetLetterPosition(new Vector2(cordX - (ii * POS_NEXT), cordY + (ii * POS_NEXT)), true);
                            currentLetterArray[(int)position.X - ii, (int)position.Y + ii] = word.Word[ii];//
                        }
                        break;
                    }
                case WordDirection.Up_Left:
                    {
                        cordX = BASE_POS_X + (int)position.X * POS_NEXT;
                        cordY = BASE_POS_Y + (int)position.Y * POS_NEXT;
                        for (int ii = 0; ii < word.Word.Count; ii++)
                        {
                            word.Word[ii].SetLetterPosition(new Vector2(cordX - (ii * POS_NEXT), cordY - (ii * POS_NEXT)), true);
                            currentLetterArray[(int)position.X - ii, (int)position.Y - ii] = word.Word[ii];
                        }
                        break;
                    }
            }
        }

        public bool WordIsSelected(SearchWord word)
        {
            bool isSelected = true;
            for(int ii = 0; ii < word.Word.Count; ii++)
            {
                if(!word.Word[ii].IsSelected)
                {
                    isSelected = false;
                    ii = word.Word.Count;
                }
            }
            return isSelected;
        }

        public List<char> GetValues(SearchWord word)
        {
            List<char> valueList = new List<char>();
            foreach(SearchLetter letter in word.Word)
            {
                char v = letter.Value;
                valueList.Add(v);
            }
            return valueList;
        }

        public void RefershSelected(SearchWord word)
        {
            foreach(SearchLetter letter in word.Word)
            {
                letter.IsSelected = false;
            }
        }

        public void SetSpriteSize(SearchWord word)
        {
            foreach (SearchLetter l in word.Word)
            {
                l.Size = .5f;
            }
        }

        public void Draw()
        {
            foreach (SearchLetter letter in Word)
            {
                letter.Draw();
            }
        }
    }
}