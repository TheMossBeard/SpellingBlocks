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
        private SpriteBatch spriteBatch { get; set; }
        public List<Block> WinnerBlocks { get; set; }
        private float BlockPositionX { get; set; }
        private float BlockPositionY { get; set; }
        public List<Block> WinnerList { get; set; }

        public Winner(SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;

            BlockPositionX = 272;
            BlockPositionY = 256 - 64;

            WinnerList = new List<Block>();
            LetterValue letter = new LetterValue(gameContent);
            int[] letterIndex = { 22, 8, 13, 13, 4, 17 };

            Block block;
            for (int jj = 0; jj < 6; jj++)
            {
                block = new Block(BlockPositionX, BlockPositionY, letter.LetterValueList[letterIndex[jj]].Value, letter.LetterValueList[letterIndex[jj]].Sprite, spriteBatch, gameContent);
                WinnerList.Add(block);
                BlockPositionX += 64 + 16;
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