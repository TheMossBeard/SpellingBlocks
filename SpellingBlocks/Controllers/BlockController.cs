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
        public List<Block> PlayField { get; set; }
        private SpriteBatch spriteBatch { get; set; }


        public BlockController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            BlockList = new List<Block>();
            EmptyList = new List<Block>();
            PlayField = new List<Block>();
            //testing parse
            LetterValues = new LetterValue(gameContent);
            List<LetterValue> parsedWord = new List<LetterValue>();
            WinCheck = new List<LetterValue>();
            string current = "CAT";
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

        //public void Update(Rectangle touchPosition)
        //{
        //    foreach (Block block in BlockList)
        //    {
        //        if (HitTest(block.HitBox, touchPosition) == true && !block.IsSelected)
        //        {
        //            block.ChangeSelect();
        //        }
        //        //else if (HitTest(block.HitBox, touchPosition) == true && block.IsSelected)
        //        //{
        //        //    block.ChangeUnSelect();
        //        //}
        //        else
        //            block.ChangeUnSelect();
        //    }
        //    foreach (Block block in EmptyList)
        //    {
        //        if (HitTest(block.HitBox, touchPosition) == true && !block.IsSelected)
        //        {
        //            block.ChangeSelect();
        //        }
        //        //else if (HitTest(block.HitBox, touchPosition) == true && block.IsSelected)
        //        //{
        //        //    block.ChangeUnSelect();
        //        //}
        //        else
        //            block.ChangeUnSelect();
        //    }
        //}

        public void MoveHighlightedBlock(TouchLocation tl, GameContent gameContent)
        {
            Rectangle touchBox;
            int touchIndex = -1;
            int selectedIndex = -1;
            int count = EmptyList.Count();
            touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 2, 2);
            List<Block> allBlockList = new List<Block>();
            Block touchBlock;
            Block selectBlock;
            foreach(Block block in BlockList)
            {
                allBlockList.Add(block);
            }
            foreach(Block block in EmptyList)
            {
                allBlockList.Add(block);
            }
            for (int ii = 0; ii < allBlockList.Count(); ii++)
            {
                if (HitTest(allBlockList[ii].HitBox, touchBox))
                    touchIndex = ii;
                if (allBlockList[ii].IsSelected == true)
                    selectedIndex = ii;
            }
            if (selectedIndex == -1 && touchIndex != -1)
            {
                allBlockList[touchIndex].ChangeSelect();
            }
            else if (selectedIndex != -1 && touchIndex != -1) //not really being moved, need to create new blocks and insert them
            {
                selectBlock = new Block(allBlockList[selectedIndex]);
                touchBlock = new Block(allBlockList[touchIndex]);

                allBlockList.RemoveAt(selectedIndex);
                allBlockList.RemoveAt(touchIndex);

                allBlockList.Insert(selectedIndex, touchBlock);
                allBlockList.Insert(touchIndex, selectBlock);

                //tmpBlock = allBlockList[selectedIndex];
                //allBlockList[selectedIndex] = allBlockList[touchIndex];
                //allBlockList[touchIndex] = tmpBlock;

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



            //bool touchEmpty = false;
            //foreach (Block emptyblock in EmptyList)
            //{
            //    if (HitTest(emptyblock.HitBox, touchBox))
            //    {
            //        touchEmpty = true;
            //    }
            //}

            //Vector2 tmpV;
            //Rectangle tmpR;
            //char tmpValue;
            //if (touchEmpty)
            //{
            //    foreach (Block block in EmptyList)
            //    {
            //        touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 2, 2);
            //        if (HitTest(block.HitBox, touchBox))
            //        {

            //            foreach (Block block1 in BlockList)
            //            {
            //                if (block.IsSelected)
            //                {
            //                    tmpV = block.Position;
            //                    tmpR = block.HitBox;

            //                    block.Position = block1.Position;
            //                    block.HitBox = block1.HitBox;

            //                    block1.Position = tmpV;
            //                    block1.HitBox = tmpR;
            //                    block1.Value = block.Value;
            //                }

            //            }
            //            foreach (Block block1 in EmptyList)
            //            {
            //                if (block.IsSelected && block.Value != '0')
            //                {
            //                    tmpV = block.Position;
            //                    tmpR = block.HitBox;
            //                    tmpValue = block.Value;

            //                    block.Position = block1.Position;
            //                    block.HitBox = block1.HitBox;
            //                    block.Value = block1.Value;

            //                    block1.Position = tmpV;
            //                    block1.HitBox = tmpR;
            //                    block1.Value = tmpValue;
            //                }

            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (Block blockHit in BlockList)
            //    {
            //        touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 2, 2);
            //        if (HitTest(blockHit.HitBox, touchBox))
            //        {
            //            foreach (Block blockMove in EmptyList)
            //            {
            //                if (blockMove.IsSelected)
            //                {
            //                    tmpV = blockHit.Position;
            //                    tmpR = blockHit.HitBox;

            //                    blockHit.Position = blockMove.Position;
            //                    blockHit.HitBox = blockMove.HitBox;

            //                    blockMove.Position = tmpV;
            //                    blockMove.HitBox = tmpR;

            //                }
            //            }
            //            //foreach (Block blockMove in BlockList)
            //            //{
            //            //    if (blockHit.IsSelected)
            //            //    {
            //            //        tmpV = blockHit.Position;
            //            //        tmpR = blockHit.HitBox;

            //            //        blockHit.Position = blockMove.Position;
            //            //        blockHit.HitBox = blockMove.HitBox;

            //            //        blockMove.Position = tmpV;
            //            //        blockMove.HitBox = tmpR;
            //            //    }
            //            //}
            //        }
            //    }
            //}




            //Rectangle touchBox;
            //char tmp;
            //foreach (Block emptyblock in EmptyList)
            //{
            //    touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 2, 2);
            //    if (HitTest(emptyblock.HitBox, touchBox))
            //    {
            //        foreach (Block block in BlockList)
            //        {
            //            Vector2 tmpV;
            //            Rectangle tmpR;
            //            if (block.IsSelected)
            //            {
            //                tmpV = block.Position;
            //                tmpR = block.HitBox;

            //                block.Position = emptyblock.Position;
            //                block.HitBox = emptyblock.HitBox;

            //                emptyblock.Position = tmpV;
            //                emptyblock.HitBox = tmpR;
            //                //assign value to emptylist
            //                emptyblock.Value = block.Value;
            //            }
            //        }
            //        foreach (Block block in EmptyList)
            //        {
            //            Vector2 tmpV;
            //            Rectangle tmpR;
            //            if (block.IsSelected)
            //            {
            //                tmpV = block.Position;
            //                tmpR = block.HitBox;
            //                tmp = block.Value;

            //                block.Position = emptyblock.Position;
            //                block.HitBox = emptyblock.HitBox;
            //                block.Value = emptyblock.Value;

            //                emptyblock.Position = tmpV;
            //                emptyblock.HitBox = tmpR;
            //                //assign value to emptylist
            //                emptyblock.Value = block.Value;

            //            }
            //        }
            //    }

            //}
            ////for fullblock to fullblock movement, is working
            //foreach (Block emptyblock in BlockList)
            //{
            //    touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 2, 2);
            //    if (HitTest(emptyblock.HitBox, touchBox))
            //    {
            //        foreach (Block block in BlockList)
            //        {
            //            Vector2 tmpV;
            //            Rectangle tmpR;
            //            if (block.IsSelected)
            //            {
            //                tmpV = block.Position;
            //                tmpR = block.HitBox;

            //                block.Position = emptyblock.Position;
            //                block.HitBox = emptyblock.HitBox;

            //                emptyblock.Position = tmpV;
            //                emptyblock.HitBox = tmpR;

            //            }
            //        }
            //    }
            //}
            //PlayField = EmptyList;
            //foreach (Block emptyblock in EmptyList)
            //{
            //    touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 2, 2);
            //    if (HitTest(emptyblock.HitBox, touchBox))
            //    {
            //        int count = 0;
            //        foreach (Block block in EmptyList)
            //        {
            //            Vector2 tmpV;
            //            Rectangle tmpR;
            //            if (block.IsSelected)
            //            {
            //                tmpV = block.Position;
            //                tmpR = block.HitBox;
            //                tmp = block.Value;

            //                block.Position = emptyblock.Position;
            //                block.HitBox = emptyblock.HitBox;
            //                block.Value = emptyblock.Value;

            //                emptyblock.Position = tmpV;
            //                emptyblock.HitBox = tmpR;
            //                emptyblock.Value = tmp;
            //                count++;
            //            }
            //        }
            //        //foreach (Block block in BlockList)
            //        //{
            //        //    Vector2 tmpV;
            //        //    Rectangle tmpR;
            //        //    if (block.IsSelected)
            //        //    {
            //        //        tmpV = block.Position;
            //        //        tmpR = block.HitBox;
            //        //        char tmp = block.Value;

            //        //        block.Position = emptyblock.Position;
            //        //        block.HitBox = emptyblock.HitBox;
            //        //        //block.Value = emptyblock.Value;

            //        //        emptyblock.Position = tmpV;
            //        //        emptyblock.HitBox = tmpR;
            //        //        emptyblock.Value = tmp;
            //        //        count++;
            //        //    }
            //        //}
            //    }
            //}

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