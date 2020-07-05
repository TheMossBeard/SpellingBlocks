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
        public List<string> ValueList { get; set; }
        private SpriteBatch spriteBatch { get; set; }


        public BlockController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            BlockList = new List<Block>();
            EmptyList = new List<Block>();
            ValueList = new List<string>();
            ValueList.Add("A");
            ValueList.Add("B");
            ValueList.Add("C");
            ValueList.Add("D");
            ValueList.Add("E");
            ValueList.Add("F");
            ValueList.Add("G");
            ValueList.Add("H");
            ValueList.Add("I");

            this.spriteBatch = spriteBatch;
            //get screen size
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            BlockPositionX = ScreenWidth * .05f;
            BlockPositionY = ScreenHeight * .80f;

            Block block;
            for (int ii = 0; ii < 9; ii++)
            {
                block = new Block(BlockPositionX, BlockPositionY, ValueList[ii], gameContent.SpriteList[ii], spriteBatch, gameContent);
                BlockList.Add(block);
                BlockPositionX += (ScreenWidth / 12);
            }
           // Shuffle();
            BlockPositionX = ScreenWidth * .25f;
            BlockPositionY = ScreenHeight / 3;

            for (int ii = 0; ii < 4; ii++)
            {
                block = new Block(BlockPositionX, BlockPositionY, "", gameContent.emptySprite, spriteBatch, gameContent);
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

        public void MoveHighlightedBlock(TouchLocation tl)
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
        }

        public bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
                return true;
            else
                return false;
        }

        public void Shuffle()
        {
            Random ran = new Random(6);
            int random1;
            int random2;
            Block tmp;
            for (int ii = 0; ii < 49; ii++)
            {
                random1 = ran.Next(0, 6);
                random2 = ran.Next(0, 6);
                while (random1 == random2)
                    random2 = ran.Next(0, 6);

                tmp = BlockList[random1];
                BlockList[random1] = BlockList[random2];
                BlockList[random2] = tmp;
            }
        }
    }

}