using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.SocialPlatforms.Impl;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "80de398d131611c192c399f09a04decfbe4505454cd469a3f8f7c0757a9be93a";

    bool entrySet = false;

    private void Start() {
        GetLeaderboard();
    }

    public void GetLeaderboard() {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++) {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score) {
        if (entrySet) {
            return;
        }

        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username,
            score, ((msg) => {
                GetLeaderboard();
                LeaderboardCreator.ResetPlayer();
        }));

        entrySet = true;
    }

}
