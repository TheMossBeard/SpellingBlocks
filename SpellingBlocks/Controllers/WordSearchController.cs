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
        private int PlayFieldWidth { get; set; }
        private int PlayFieldHeight { get; set; }

        public WordSearchController(SpriteBatch spriteBatch, GameContent gameContent, GameState state)
        {
            this.spriteBatch = spriteBatch;
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
                    CurrentLetter2DArray[jj,ii] = letter;
                    positionX += 64;
                }
                positionY += 64;
            }
        }


        public void Draw()
        {
            for(int ii = 0; ii < PlayFieldHeight; ii++)
            {
                for(int jj = 0; jj < PlayFieldWidth; jj++)
                {
                    CurrentLetter2DArray[jj,ii].Draw();
                }
            }
        }

    }


}