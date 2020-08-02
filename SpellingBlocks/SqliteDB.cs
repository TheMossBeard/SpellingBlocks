using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SpellingBlocks.Objects;
using SQLite;

namespace SpellingBlocks
{
    [Table("Words")]
    class SqliteDB
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Category { get; set; }
        [Unique]
        public string Word { get; set; }

        //nature 1-25 animals 26-50 machines 51 - 75

        public SqliteDB()
        {
            CreateDB();
        }

        public static void CreateDB()
        {
            string[] natureWords = { "tree", "river", "creek", "lake", "hiking", "trail", "camping", "tent", "compass", "cloud",
        "insect", "bug", "smore", "fishing", "snow", "sunset", "sunrise", "ocean", "beach", "pebble", "leaf", "seed", "cliff", "fern", "stream"};
            string[] animalWords = { "horse", "dog", "zebra", "tiger", "lion", "bear", "rat", "mouse", "elk", "deer", "moose",
        "cow", "bobcat", "monkey", "snake", "eagle", "hawk", "owl", "crow", "buffalo", "bison", "donkey", "lizard", "frog", "rabit"};
            string[] machineWords = { "car", "truck", "train", "fuel", "gear", "tire", "wrench", "airplane", "tractor", "diesel",
            "boat", "ship", "hose", "funnel", "bike", "wheel", "basket", "shovel", "axe", "electric", "power", "drill", "hammer", "socket", "oil" };

            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Words.db3");
            var db = new SQLiteConnection(dbPath);
            //db.DeleteAll<WordDB>();

            db.CreateTable<WordDB>();
            
            if (db.Table<WordDB>().Count() == 0 )
            {
                WordDB word = new WordDB();
                foreach (string s in natureWords)
                {
                    word = new WordDB();
                    word.Category = "nature";
                    word.Word = s;
                    db.Insert(word);
                }
                foreach (string s in animalWords)
                {
                    word = new WordDB();
                    word.Category = "animals";
                    word.Word = s;
                    db.Insert(word);
                }
                foreach (string s in machineWords)
                {
                    word = new WordDB();
                    word.Category = "machines";
                    word.Word = s;
                    db.Insert(word);
                }
             }
            db.Close();
        }
    }
}