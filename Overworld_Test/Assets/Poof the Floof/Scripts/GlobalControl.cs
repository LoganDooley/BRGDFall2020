using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SheepCounting
{
    public class GlobalControl : MonoBehaviour 
    {
        public static GlobalControl Instance;

        // Change this value to 2 or 3 for easier testing
        public const int maxNumOfLeaderboard = 10;
        public const string filePath = "sheepCountLeaderboard.txt";
        
        public SortedList<float, string> pastScores;
        public int numOfLeaderboardPlayers;

        void Awake ()   
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;

                var descendingComparer = Comparer<float>.Create((x, y) => y.CompareTo(x));
                pastScores = new SortedList<float, string>(descendingComparer);
                readLeaderboardFromFile();

                // uncomment either one of these to pre-fill the leaderboard with scores
                // initializePlaceHolderScores();
                // initializeLargePlaceHolderScores();
            }
            else if (Instance != this)
            {
                Destroy (gameObject);
            }
        }

        void readLeaderboardFromFile()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    numOfLeaderboardPlayers = 0;
                }

                else
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        int index = 0;
                        string lastName = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (index == 0) // first value
                                numOfLeaderboardPlayers = int.Parse(line);
                            else if (index % 2 == 1) // odd; usernames
                                lastName = line;
                            else if (index % 2 == 0) // even; scores
                                pastScores.Add(float.Parse(line), lastName);

                            index++;
                        }
                    }
                }
            }
            catch (ArgumentException e)
            {
                print("The file could not be read:");
                print(e.Message);
            }
        }

        public static void updateLeaderBoard(SortedList<float, string> newPastScores, int newNumOfLeaderboardPlayers)
        {
            Instance.pastScores = newPastScores;
            Instance.numOfLeaderboardPlayers = newNumOfLeaderboardPlayers;

            var sb = new System.Text.StringBuilder();
            sb.AppendLine(newNumOfLeaderboardPlayers.ToString());

            for (int i = 0; i < newNumOfLeaderboardPlayers; i++)
            {
                sb.AppendLine(newPastScores.Values[i]);
                sb.AppendLine(newPastScores.Keys[i].ToString());
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            File.WriteAllText(filePath, sb.ToString());
        }

        void initializePlaceHolderScores()
        {
            addNewScore(0.1f, "user1");
            addNewScore(0.2f, "user2");
            addNewScore(0.3f, "user3");
            addNewScore(0.4f, "user4");
            addNewScore(0.5f, "user5");
            addNewScore(0.6f, "user6");
            addNewScore(0.7f, "user7");
            addNewScore(0.8f, "user8");
            addNewScore(0.9f, "user9");
            addNewScore(1.13f, "user10");
            addNewScore(0.11f, "user11");
        }
        void initializeLargePlaceHolderScores()
        {
            addNewScore(10.1f, "user1");
            addNewScore(10.2f, "user2");
            addNewScore(10.3f, "user3");
            addNewScore(10.4f, "user4");
            addNewScore(10.5f, "user5");
            addNewScore(10.6f, "user6");
            addNewScore(10.7f, "user7");
            addNewScore(10.8f, "user8");
            addNewScore(10.9f, "user9");
            addNewScore(10.13f, "user10");
            addNewScore(10.11f, "user11");
        }

        void addNewScore(float time, string username)
        {
            pastScores.Add(time, username);
            numOfLeaderboardPlayers = pastScores.Count < maxNumOfLeaderboard ? pastScores.Count : maxNumOfLeaderboard;
        }
    }
}

