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
        public GameContent gameContent { get; set; }
        public Texture2D Background { get; set; }
        public Texture2D WinnerSplash { get; set; }
        private MenuButton HomeButton { get; set; }
        private MenuButton NewButton { get; set; }
        private List<Block> BlockList { get; set; }
        private List<Block> HiddenList { get; set; }

        public bool DidWin { get; set; }

        private const int XCORD = 96, YCORD = 149, YCORDPLUS = 136, XCORDPLUS = 160, BSIZE = 128, GAP = 8, ROW = 5;

        public MatchingController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            this.gameContent = gameContent;

            Background = gameContent.matchingBackground;
            WinnerSplash = gameContent.congrats;

            HomeButton = new MenuButton(new Vector2(16, 16), "HomeButton", gameContent.home, spriteBatch, gameContent);
            NewButton = new MenuButton(new Vector2(480, 448), "NewButton", gameContent.arrorRight, spriteBatch, gameContent);

            PopulateBlockList(gameContent);
            DidWin = false;
        }

        private void PopulateBlockList(GameContent gameContent)
        {
            BlockList = new List<Block>();
            HiddenList = new List<Block>();
            LetterValue letterValue = new LetterValue(gameContent);

            Block block;
            int xcord = XCORD;
            int ycord = YCORD;
            List<int> randomSelectList = Randomize();
            for (int ii = 0; ii < 12; ii++)
            {
                block = new Block(xcord, ycord, letterValue.LetterValueList[randomSelectList[ii]].Value,
                    letterValue.LetterValueList[randomSelectList[ii]].Sprite, spriteBatch, gameContent);
                block.HitBox = new Rectangle(xcord, ycord, 128, 128);
                BlockList.Add(block);

                block = new Block(xcord, ycord, letterValue.LetterValueList[randomSelectList[ii]].Value,
                    gameContent.question, spriteBatch, gameContent);
                block.HitBox = new Rectangle(xcord, ycord, 128, 128);
                HiddenList.Add(block);

                xcord += BSIZE + GAP;

                if (ii == ROW || ii == (ROW * 2) + 1 || ii == (ROW * 3) + 2)
                {
                    ycord += BSIZE + GAP;
                    xcord = XCORD;
                }
            }
        }

        private List<int> Randomize()
        {
            int ran;
            List<int> list = new List<int>();
            List<int> possibleList = new List<int>();
            for (int ii = 0; ii < 25; ii++)
            {
                possibleList.Add(ii);
            }
            Random random = new Random();
            for (int ii = 0; ii < 6; ii++)
            {
                ran = random.Next(0, possibleList.Count);
                list.Add(possibleList[ran]);
                possibleList.RemoveAt(ran);
            }
            list.AddRange(list);

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public void SelectBlock(Rectangle touchBox)
        {
            int selectedCount = 0;
            int index1 = -1;
            int index2 = -1;
            for (int ii = 0; ii < HiddenList.Count; ii++)
            {
                if (HitTest(HiddenList[ii].HitBox, touchBox))
                {
                    foreach (Block block in HiddenList)
                    {
                        if (block.IsEmptyBlock == true)
                            selectedCount++;
                    }
                    if (selectedCount == 2)
                    {
                        for (int jj = 0; jj < HiddenList.Count; jj++)
                        {
                            if (HiddenList[jj].IsEmptyBlock)
                            {
                                if (index1 == -1)
                                    index1 = jj;
                                else
                                    index2 = jj;
                            }
                        }
                        if (index1 != -1 && index2 != -1)
                        {
                            if (HiddenList[index1].Value == HiddenList[index2].Value)
                            {
                                HiddenList[index1].IsSelected = true;
                                HiddenList[index2].IsSelected = true;
                            }
                        }
                        foreach (Block block in HiddenList)
                        {
                            block.IsEmptyBlock = false;
                        }
                    }
                    HiddenList[ii].IsEmptyBlock = true;
                }
            }
            CheckWin();
        }

        public bool NewGameButton(Rectangle touchBox)
        {
            if(HitTest(touchBox, NewButton.HitBox) && DidWin)
            {
                return true;
            }
            return false;
        }

        public void CheckWin()
        {
            int selectedCount = 0;
            int index1 = -1;
            int index2 = -1;
            for (int ii = 0; ii < HiddenList.Count; ii++)
            {
                foreach (Block block in HiddenList)
                {
                    if (block.IsEmptyBlock == true)
                        selectedCount++;
                }
                if (selectedCount == 2)
                {
                    for (int jj = 0; jj < HiddenList.Count; jj++)
                    {
                        if (HiddenList[jj].IsEmptyBlock)
                        {
                            if (index1 == -1)
                                index1 = jj;
                            else
                                index2 = jj;
                        }
                    }
                    if (index1 != -1 && index2 != -1)
                    {
                        if (HiddenList[index1].Value == HiddenList[index2].Value)
                        {
                            HiddenList[index1].IsSelected = true;
                            HiddenList[index2].IsSelected = true;
                        }
                    }
                }
            }

            for (int ii = 0; ii < HiddenList.Count(); ii++)
            {
                if (HiddenList[ii].IsSelected == false)
                {
                    DidWin = false;
                    return;
                }
            }
            DidWin = true;

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
            foreach (Block block in BlockList)
            {
                block.DrawBig();
            }
            foreach (Block block in HiddenList)
            {
                if (!block.IsSelected && !block.IsEmptyBlock)
                    block.DrawBig();
            }
            CheckWin();
            if (DidWin)
            {
                spriteBatch.Draw(WinnerSplash, new Vector2(256, 144), null, Color.White, 0,
                    new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                //NewButton = new MenuButton(new Vector2(480, 448), "NewButton", gameContent.arrorRight, spriteBatch, gameContent);
                NewButton.Draw();
            }
        }
    }
}