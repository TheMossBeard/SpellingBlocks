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
using SpellingBlocks.Objects;

namespace SpellingBlocks.Controllers
{
    class MatchingController
    {
        public SpriteBatch spriteBatch { get; set; }
        public Texture2D Background { get; set; }
        private MenuButton HomeButton { get; set; }
        private List<Block> BlockList { get; set; }
        private List<Block> HiddenList { get; set; }

        public MatchingController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            Background = gameContent.matchingBackground;

            HomeButton = new MenuButton(new Vector2(16, 16), "HomeButton", gameContent.home, spriteBatch, gameContent);

            PopulateBlockList(gameContent);
        }

        private void PopulateBlockList(GameContent gameContent)
        {
             BlockList = new List<Block>();
            HiddenList = new List<Block>();
            LetterValue letterValue = new LetterValue(gameContent);
            //List<int> randomList = Random();

            Block block;
            int xcord = 128 + 32;
            int ycord = 32 - 8;
            for(int ii = 0; ii < 24; ii++)
            {
                block = new Block(xcord, ycord, letterValue.LetterValueList[ii].Value, letterValue.LetterValueList[ii].Sprite, 
                    spriteBatch, gameContent);
                BlockList.Add(block);

                block = new Block(xcord, ycord, letterValue.LetterValueList[ii].Value, gameContent.question,
                    spriteBatch, gameContent);
                HiddenList.Add(block);

                xcord += (128 + 8);

                if (ii == 5 || ii == 11 || ii == 17)
                {
                    ycord += (128 + 8);
                    xcord = 128 + 32;
                }
            }

        }

        public List<int> Randomize()
        {
            int ran;
            List<int> list = new List<int>();
            Random random = new Random();
            for (int ii = 0; ii < 10; ii++)
            {
                ran = random.Next(0, 26);
                //foreach()
             
            }
            return list;
        }

        public bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
                return true;
            else
                return false;
        }

        public GameState HomeButtonUpdate(Rectangle touchBox, GameState state)
        {
            if (HitTest(HomeButton.HitBox, touchBox))
                state = GameState.MainMenu;

            return state;
        }

        public void Draw()
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), null, Color.White, 0,
                new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            HomeButton.Draw();
            foreach(Block block in BlockList)
            {
                block.DrawBig();
            }
            foreach (Block block in HiddenList)
            {
                block.DrawBig();
            }
        }
    }
}