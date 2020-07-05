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
        public LetterValue LetterValues { get; set; }

        public List<LetterValue> WinCheck;
        private SpriteBatch spriteBatch { get; set; }


        public BlockController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            BlockList = new List<Block>();
            EmptyList = new List<Block>();
            //testing parse
            LetterValues = new LetterValue(gameContent);
            List<LetterValue> parsedWord = new List<LetterValue>();
            WinCheck = new List<LetterValue>();
            string current = "SNAKE";
            ParseWord(current, parsedWord);
            ParseWord(current, WinCheck);
            //after parse, add random letters to parse so parse.length = 9
            Random random = new Random();
            int wordLength = parsedWord.Count;
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
            BlockPositionX = ScreenWidth * .25f;
            BlockPositionY = ScreenHeight / 3;

            for (int ii = 0; ii < wordLength; ii++)
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

        public void Update(Rectangle touchPosition)
        {
            foreach (Block block in BlockList)
            {
                if (HitTest(block.HitBox, touchPosition) == true && !block.IsSelected)
                {
                    block.ChangeSelect();
                }
                else if (HitTest(block.HitBox, touchPosition) == true && block.IsSelected)
                {
                    block.ChangeUnSelect();
                }
                else
                    block.ChangeUnSelect();
            }
        }

        public bool MoveHighlightedBlock(TouchLocation tl)
        {
            Rectangle touchBox;
            foreach (Block emptyblock in EmptyList)
            {
                touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 2, 2);
                if (HitTest(emptyblock.HitBox, touchBox))
                {
                    foreach (Block block in BlockList)
                    {
                        Vector2 tmpV;
                        Rectangle tmpR;
                        if (block.IsSelected)
                        {
                            tmpV = block.Position;
                            tmpR = block.HitBox;

                            block.Position = emptyblock.Position;
                            block.HitBox = emptyblock.HitBox;

                            emptyblock.Position = tmpV;
                            emptyblock.HitBox = tmpR;
                            //assign value to emptylist
                            emptyblock.Value = block.Value;
                        }
                    }
                }
            }
            foreach (Block emptyblock in BlockList)
            {
                touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 2, 2);
                if (HitTest(emptyblock.HitBox, touchBox))
                {
                    foreach (Block block in BlockList)
                    {
                        Vector2 tmpV;
                        Rectangle tmpR;
                        if (block.IsSelected)
                        {
                            tmpV = block.Position;
                            tmpR = block.HitBox;

                            block.Position = emptyblock.Position;
                            block.HitBox = emptyblock.HitBox;

                            emptyblock.Position = tmpV;
                            emptyblock.HitBox = tmpR;
                        }
                    }
                }
                            
            }
            return (CheckWin(EmptyList, WinCheck));
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
            for(int ii = 0; ii < current.Length; ii++)
            {
               
                for(int jj = 0; jj <  LetterValues.LetterValueList.Count(); jj++)
                {
                    if (current[ii] == LetterValues.LetterValueList[jj].Value)
                    {
                        parsedWord.Add(LetterValues.LetterValueList[jj]);
                        jj = LetterValues.LetterValueList.Count();
                    }
                }
            }
        }
        public bool CheckWin(List<Block> emptyList, List<LetterValue> winCheck)
        {
            bool win = true;
            for (int ii = 0; ii < winCheck.Count(); ii++)
            {
                if (emptyList[ii].Value != winCheck[ii].Value)
                {
                    win = false;
                    ii = winCheck.Count();
                }
            }
            return win;
        }
    }

}