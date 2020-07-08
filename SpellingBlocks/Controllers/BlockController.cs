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
    class BlockController
    {
        public List<Block> BlockList { get; set; }
        public List<Block> EmptyList { get; set; }
        private int ScreenWidth { get; set; }
        private int ScreenHeight { get; set; }
        private float BlockPositionX { get; set; }
        private float BlockPositionY { get; set; }
        //public List<string> ValueList { get; set; }
        public Words AllWords { get; set; }
        public LetterValue LetterValues { get; set; }

        public List<LetterValue> WinCheck;
        public List<Block> PlayField { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        private int CurrentWordIndex { get; set; }

        public BlockController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            BlockList = new List<Block>();
            EmptyList = new List<Block>();
            
            LetterValues = new LetterValue(gameContent);
            WinCheck = new List<LetterValue>();
            AllWords = new Words(gameContent);
            Random random = new Random();
            CurrentWordIndex = random.Next(0,4);
          //  string current = "AIRPLANE";
          //  ParseWord(current, WinCheck);

            List<LetterValue> parsedWord = new List<LetterValue>();
            for(int ii = 0; ii < AllWords.NatureWordList[CurrentWordIndex].Value.Count(); ii++)
            {
                parsedWord.Add(AllWords.NatureWordList[CurrentWordIndex].Value[ii]);
                WinCheck.Add(AllWords.NatureWordList[CurrentWordIndex].Value[ii]);
            }
          //  ParseWord(current, parsedWord);
           
        //    int wordLength = parsedWord.Count;
            while (parsedWord.Count < 9)
            {
                random.Next(0, 25);
                parsedWord.Add(LetterValues.LetterValueList[random.Next(0, 25)]);
            }
            int ran1;
            int ran2;
            LetterValue tmp;
            for (int ii = 0; ii < 49; ii++)
            {
                ran1 = random.Next(0, 8);
                ran2 = random.Next(0, 8);
                tmp = parsedWord[ran1];
                parsedWord[ran1] = parsedWord[ran2];
                parsedWord[ran2] = tmp;
            }

            this.spriteBatch = spriteBatch;
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            BlockPositionX = ScreenWidth * .05f;
            BlockPositionY = ScreenHeight * .80f;

            Block block;
            for (int ii = 0; ii < parsedWord.Count; ii++)
            {
                block = new Block(BlockPositionX, BlockPositionY, parsedWord[ii].Value, parsedWord[ii].Sprite, spriteBatch, gameContent);
                BlockList.Add(block);
                BlockPositionX += (ScreenWidth / 12);
            }
            BlockPositionX = ScreenWidth * .15f;
            BlockPositionY = ScreenHeight / 3;

            for (int ii = 0; ii < AllWords.NatureWordList[CurrentWordIndex].Value.Count(); ii++)
            {
                block = new Block(BlockPositionX, BlockPositionY, '0', gameContent.emptySprite, spriteBatch, gameContent);
                block.IsEmptyBlock = true;
                EmptyList.Add(block);
                BlockPositionX += (ScreenWidth / 12);
            }

        }
        public void Draw()
        {
            foreach (Block block in BlockList)
            {
                block.Draw();
            }
            foreach (Block block in EmptyList)
            {
                block.Draw();
            }
        }        
        public void MoveHighlightedBlock(TouchLocation tl)
        {
            List<Block> allBlockList = new List<Block>();
            Block selectBlock;
            Block selectBlock0;
            Rectangle touchBox;
            Rectangle tmpHitBox;
            Vector2 tmpPosition;
            int selectedCount = 0;
            int selectedIndex = -1;
            int selectedIndex2 = -1;
            int count = EmptyList.Count();
            touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 2, 2);
            foreach (Block block in BlockList)
            {
                allBlockList.Add(block);
            }
            foreach (Block block in EmptyList)
            {
                allBlockList.Add(block);
            }
            for (int ii = 0; ii < allBlockList.Count(); ii++)
            {
                if (HitTest(allBlockList[ii].HitBox, touchBox))
                    allBlockList[ii].ChangeSelect();          
            }
            for (int ii = 0; ii < allBlockList.Count(); ii++)
            {
                if (allBlockList[ii].IsSelected)
                {
                    selectedCount++;
                    if (selectedIndex == -1)
                        selectedIndex = ii;
                    else if (selectedIndex2 == -1)
                        selectedIndex2 = ii;
                }
            }
            if (selectedCount == 2) 
            {
                tmpPosition = allBlockList[selectedIndex].Position;
                tmpHitBox = allBlockList[selectedIndex].HitBox;

                selectBlock = new Block(allBlockList[selectedIndex]);
                selectBlock.Position = allBlockList[selectedIndex2].Position;
                selectBlock.HitBox = allBlockList[selectedIndex2].HitBox;

                selectBlock0 = new Block(allBlockList[selectedIndex2]);  
                selectBlock0.Position = tmpPosition;
                selectBlock0.HitBox = tmpHitBox;

                allBlockList[selectedIndex] = selectBlock0;
                allBlockList[selectedIndex2] = selectBlock;

                EmptyList = new List<Block>();
                int index = 0;
                for (int ii = 9; ii < 9 + count; ii++)
                {
                    EmptyList.Add(allBlockList[ii]);
                    EmptyList[index].ChangeUnSelect();
                    index++;
                }
                BlockList = new List<Block>();
                for (int ii = 0; ii < 9; ii++)
                {
                    BlockList.Add(allBlockList[ii]);
                    BlockList[ii].ChangeUnSelect();
                }
            }            
        }

        public bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
                return true;
            else
                return false;
        }
        public void ParseWord(string current, List<LetterValue> parsedWord)
        {
            for (int ii = 0; ii < current.Length; ii++)
            {
                for (int jj = 0; jj < LetterValues.LetterValueList.Count(); jj++)
                {
                    if (current[ii] == LetterValues.LetterValueList[jj].Value)
                    {
                        parsedWord.Add(LetterValues.LetterValueList[jj]);
                        jj = LetterValues.LetterValueList.Count();
                    }
                }
            }
        }
        public bool CheckWin()
        {
            bool win = true;
            for (int ii = 0; ii < WinCheck.Count(); ii++)
            {
                if (EmptyList[ii].Value != WinCheck[ii].Value)
                {
                    win = false;
                    ii = WinCheck.Count();
                }
            }
            return win;
        }
    }

}