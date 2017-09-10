using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager {

    private static ScoreManager instance;
    public static ScoreManager Instance {
        get {
            if (instance == null)
                instance = new ScoreManager();
            return instance;
        }
    }

    private float score = 0;
    public float Score {
        get { return score; }
    }

    public void UpdateScore(float value) {
        if (value > score) {
            score = value;
        }
    }

    public void ResetScore() {
        score = 0;
    }
}
