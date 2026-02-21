using UnityEngine;
using System.IO;

public static class SaveSystem {
    // Define the path to the save file.
    private static string savePath => Path.Combine(Application.persistentDataPath, "save.json");

    // A method to load the high score.
    public static int LoadHighScore() {
        if (!File.Exists(savePath)) {
            return 0; // return if the file cant be found.
        }

        // Get the text data from the json file and convert it to a SaveData object.
        string json = File.ReadAllText(savePath);
        var data = JsonUtility.FromJson<SaveData>(json);

        // Return the high score if found, otherwise exit.
        if (data != null) {
            return data.highScore;
        } else {
            return 0;
        }
    }

    // Trys to set the high score, returns true if successful, otherwise false.
    public static bool TrySetHighScore(int score) {
        int oldHighScore = LoadHighScore(); // load the saved high score.

        // If the new score is less than the old one, return false.
        if (score <= oldHighScore) {
            return false;
        }
        
        // Otherwise, prepare a new SaveData object with the new high score.
        // and overwrite the original file.
        var newData = new SaveData {
            highScore = score
        };
        string newJson = JsonUtility.ToJson(newData);
        File.WriteAllText(savePath, newJson);
        return true;
    }
}
