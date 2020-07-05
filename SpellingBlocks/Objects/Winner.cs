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
    class Winner
    {
        private int ScreenWidth { get; set; }
        private int ScreenHeight { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        public List<Block> WinnerBlocks { get; set; }
        private float BlockPositionX { get; set; }
        private float BlockPositionY { get; set; }
        public List<Block> WinnerList { get; set; }
        Block block;
        public Winner(SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            BlockPositionX = ScreenWidth * .25f;
            BlockPositionY = ScreenHeight / 6;

            WinnerList = new List<Block>();
            LetterValue letter = new LetterValue(gameContent);
            int[] letterIndex = { 22, 8, 13, 13, 4, 17 };

            // ParseWord(winner, parsedWord);
            Block block;
            for (int jj = 0; jj < 6; jj++)
            {
                block = new Block(BlockPositionX, BlockPositionY, letter.LetterValueList[letterIndex[jj]].Value, letter.LetterValueList[letterIndex[jj]].Sprite, spriteBatch, gameContent);
                WinnerList.Add(block);
                BlockPositionX += (ScreenWidth / 12);
            }


        }
        public void Draw()
        {
            foreach (Block block in WinnerList)
            {
                block.Draw();
            }
        }



    }
}