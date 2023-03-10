using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject waterPrefab;
    public GameObject goalPrefab;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                float tempY = (14.0f - (float)levelRows.Count);
                float tempX = ((float)column);
                var letter = letters[column];
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                if (letter == 'x')
                {
                    var temp = Instantiate(rockPrefab, new Vector3(tempX, tempY, 0.0f), Quaternion.identity);
                    temp.transform.parent = environmentRoot;
                }

                if (letter == 'b')
                {
                    var temp = Instantiate(brickPrefab, new Vector3(tempX, tempY, 0.0f), Quaternion.identity);
                    temp.transform.parent = environmentRoot;
                }

                if (letter == '?')
                {
                    var temp = Instantiate(questionBoxPrefab, new Vector3(tempX, tempY, 0.0f), Quaternion.identity);
                    temp.transform.parent = environmentRoot;
                }
                
                if (letter == 's')
                {
                    var temp = Instantiate(stonePrefab, new Vector3(tempX, tempY, 0.0f), Quaternion.identity);
                    temp.transform.parent = environmentRoot;
                }
                
                if (letter == 'w')
                {
                    var temp = Instantiate(waterPrefab, new Vector3(tempX, tempY, 0.0f), Quaternion.identity);
                    temp.transform.parent = environmentRoot;
                }
                
                if (letter == 'g')
                {
                    var temp = Instantiate(goalPrefab, new Vector3(tempX, tempY, 0.0f), Quaternion.identity);
                    temp.transform.parent = environmentRoot;
                }
            }
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
