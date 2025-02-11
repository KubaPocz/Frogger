using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TableManager : MonoBehaviour
{
    private string ScoreTablePath = @"ScoreTable.txt";
    public TMP_Text scores;
    void Start()
    {
        scores.text = "";
        List<KeyValuePair<string, string>> allScores = new List<KeyValuePair<string, string>>();

        if (File.Exists(ScoreTablePath))
        {
            using (StreamReader sr = new StreamReader(ScoreTablePath))
            {
                string line;
                string[] values;
                while ((line = sr.ReadLine()) != null)
                {
                    values = line.Split("|");
                    allScores.Add(new KeyValuePair<string, string>(values[0], values[1]));
                }
            }
        }
        List<KeyValuePair<string, string>> allScoresSorted = allScores
            .OrderByDescending(score => int.Parse(score.Value))
            .ToList();
        for (int i = 0; i < allScoresSorted.Count && i < 5; i++)
        {
            scores.text += $"{i + 1}. {allScoresSorted[i].Key}  -  {allScoresSorted[i].Value}\n";
        }

    }

    public void ToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
