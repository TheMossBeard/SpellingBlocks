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
using SpellingBlocks.Objects;

namespace SpellingBlocks.Controllers
{
    class BlockController
    {
        public List<Block> BlockList { get; set; }
        private int ScreenWidth { get; set; }
        private int ScreenHeight { get; set; }
        private float BlockPositionX {get;set;}
        private float BlockPositionY { get; set; }
        private SpriteBatch spriteBatch { get; set; }


        public BlockController (SpriteBatch spriteBatch, GameContent gameContent)
        {
            BlockList = new List<Block>();
            this.spriteBatch = spriteBatch;
            //get screen size
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //calculate position of blocks based on screen size
            BlockPositionX = (ScreenWidth / 9) - (64 * 2.5f);
            BlockPositionY = (5 * (ScreenHeight / 5) - (64 * 2.5f)) - (ScreenHeight * .05f);

            Block block;
            for (int ii = 0; ii < 9; ii++)
            {
                block = new Block(BlockPositionX, BlockPositionY, "", gameContent.SpriteList[ii], spriteBatch, gameContent);
                BlockList.Add(block);
                BlockPositionX += ScreenWidth / 10;
                
            }
        }

        public void Draw()
        {
            foreach(Block block in BlockList)
            {
                block.Draw();
            }
        }

        public void Update(Rectangle touchPosition)
        {
            foreach(Block block in BlockList)
            {
               if ( HitTest(block.HitBox, touchPosition) == true)
                {
                    block.Update();
                }
                
                    
                
            }
        }

        public bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty) //in game1, get touch.position, send to controller, check position == block.list[].position, update that block position + hitbox
                return true;
            else
                return false;

        }
    }

}