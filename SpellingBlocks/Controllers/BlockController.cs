using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Util;
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

        public Words AllWords { get; set; }
        public LetterValue LetterValues { get; set; }

        public List<LetterValue> WinCheck;
        public List<Block> PlayField { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        private int CurrentWordIndex { get; set; }
        private Texture2D BackGround { get; set; }
        private Texture2D Elephant { get; set; }

        private MenuButton ArrowRight { get; set; }

        private MenuButton HomeButton { get; set; }

        public bool Skip { get; set; }

        private int CategoryIndex { get; set; }

        public bool IsWinner { get; set; }

        public BlockController(SpriteBatch spriteBatch, GameContent gameContent, GameState state)
        {
            this.spriteBatch = spriteBatch;
            Elephant = gameContent.elephant;
            BackGround = gameContent.spellingblocksbackground;
            HomeButton = new MenuButton(new Vector2(32, 32), "HomeButton", gameContent.home, spriteBatch, gameContent);
            ArrowRight = new MenuButton(new Vector2(938, 32), "ArrowRight", gameContent.arrorRight, spriteBatch, gameContent);
            Skip = false;
            IsWinner = false;
           
        }

        public void CreateGame(SpriteBatch spriteBatch, GameContent gameContent, GameState state)
        {
            Skip = false;
            IsWinner = false;
            switch (state)
            {
                case GameState.SpellingBlocksNature:
                    CategoryIndex = 0;
                    break;
                case GameState.SpellingBlocksAnimals:
                    CategoryIndex = 1;
                    break;
                case GameState.SpellingBlocksMachines:
                    CategoryIndex = 2;
                    break;

            }

            BlockList = new List<Block>();
            EmptyList = new List<Block>();
            LetterValues = new LetterValue(gameContent);
            WinCheck = new List<LetterValue>();
            AllWords = new Words(gameContent);
            Random random = new Random();
            CurrentWordIndex = random.Next(0, 5);

            List<LetterValue> parsedWord = new List<LetterValue>();
            for (int ii = 0; ii < AllWords.WordsLists[CategoryIndex][CurrentWordIndex].Value.Count(); ii++)
            {
                parsedWord.Add(AllWords.WordsLists[CategoryIndex][CurrentWordIndex].Value[ii]);
                WinCheck.Add(AllWords.WordsLists[CategoryIndex][CurrentWordIndex].Value[ii]);
            }

            while (parsedWord.Count < 10)
            {
                random.Next(0, 25);
                parsedWord.Add(LetterValues.LetterValueList[random.Next(0, 25)]);
            }
            int ran1;
            int ran2;
            LetterValue tmp;
            for (int ii = 0; ii < 50; ii++) //make this a function
            {
                ran1 = random.Next(0, 9);
                ran2 = random.Next(0, 9);
                tmp = parsedWord[ran1];
                parsedWord[ran1] = parsedWord[ran2];
                parsedWord[ran2] = tmp;
            }

            int blockPosX = 112;
            int blockPosY = 480;

            Block block;
            for (int ii = 0; ii < parsedWord.Count; ii++)
            {
                block = new Block(blockPosX, blockPosY, parsedWord[ii].Value, parsedWord[ii].Sprite, spriteBatch, gameContent);
                BlockList.Add(block);
                blockPosX += 64 + 16;
            }
            blockPosX = GetBlockPositionX(AllWords.WordsLists[CategoryIndex][CurrentWordIndex].Value.Count());
            blockPosY = 384;

            for (int ii = 0; ii < AllWords.WordsLists[CategoryIndex][CurrentWordIndex].Value.Count(); ii++)
            {
                block = new Block(blockPosX, blockPosY, '0', gameContent.emptySprite, spriteBatch, gameContent);
                block.IsEmptyBlock = true;
                EmptyList.Add(block);
                blockPosX += 64 + 16;
            }
        }

        public int GetBlockPositionX(int count)
        {
            int x = 512 - 32;
            for(int ii = 0; ii < count; ii++)
            {
                x -= 32;
            }
            return x; 
        }

        public void Draw()
        {
            
            spriteBatch.Draw(AllWords.WordsLists[CategoryIndex][CurrentWordIndex].Image, new Vector2(320, 64), null, Color.White, 0,
               new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(BackGround, new Vector2(0, 0), null, Color.White, 0,
                new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            HomeButton.Draw();
            ArrowRight.Draw();

            foreach (Block block in BlockList)
            {
                block.Draw();
            }
            foreach (Block block in EmptyList)
            {
                block.Draw();
            }
        }
        public void MoveHighlightedBlock(Rectangle touchBox)
        {
            List<Block> allBlockList = new List<Block>();
            Block selectBlock;
            Block selectBlock0;
            Rectangle tmpHitBox;
            Vector2 tmpPosition;
            int selectedCount = 0;
            int selectedIndex = -1;
            int selectedIndex2 = -1;
            int count = EmptyList.Count();

            foreach (Block block in BlockList)
            {
                allBlockList.Add(block);
            }
            foreach (Block block in EmptyList)
            {
                allBlockList.Add(block);
            }
            bool hit = false;
            for (int ii = 0; ii < allBlockList.Count(); ii++)
            {
                if (HitTest(allBlockList[ii].HitBox, touchBox))
                {
                    allBlockList[ii].ChangeSelect();
                    hit = true;
                }
            }
            if (!hit)
            {
                for (int kk = 0; kk < allBlockList.Count(); kk++)
                {
                    allBlockList[kk].ChangeUnSelect();
                }
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
                for (int ii = 10; ii < 10 + count; ii++)
                {
                    EmptyList.Add(allBlockList[ii]);
                    EmptyList[index].ChangeUnSelect();
                    index++;
                }
                BlockList = new List<Block>();
                for (int ii = 0; ii < 10; ii++)
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
            IsWinner = win;
            return IsWinner;
        }

        public GameState HomeButtonUpdate(Rectangle touchBox, GameState state)
        {
            if (HitTest(HomeButton.HitBox, touchBox))
                state = GameState.MainMenu;

            return state;
        }

        public void ArrowButton(Rectangle touchBox, GameState state)
        {
            if (HitTest(ArrowRight.HitBox, touchBox))
            {
                this.Skip = true;
                this.IsWinner = false;
            }

        }
    }

}