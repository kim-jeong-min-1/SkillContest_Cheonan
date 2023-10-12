using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : Singleton<RankManager>
{
    public static string Path => Directory.GetParent(Application.dataPath).FullName + "/rank.txt";

    [SerializeField] private List<RankInfoUI> info;
    [SerializeField] private Text inputField;
    [SerializeField] private GameObject block;
    private List<RankInfo> ranks = new();
    private int inputMax = 3;
    private int rankMax = 10;

    private void Start()
    {
        Load();
    }

    public void NewRank()
    {
        block.SetActive(true);
        inputField.text = "";
        StartCoroutine(Input());

        IEnumerator Input()
        {
            while (true)
            {
                if (inputField.text.Length == 3) break;
                yield return null;
            }
            var rankInfo = new RankInfo(inputField.text, PlayerPrefs.GetInt("Score"));
            ranks.Add(rankInfo);
            ranks.Sort(SortDesc);

            Save();
            Load();
            block.SetActive(false);
        }
    }

    public void Save()
    {
        string saveInfo ="";
        for (int i = 0; i < rankMax; i++)
        {
            saveInfo += $"{i + 1},{ranks[i].name},{ranks[i].score}";

            if(i != rankMax - 1)
            {
                saveInfo += "\n";
            }
        }
        File.WriteAllText(Path, saveInfo);
    }

    public void Load()
    {
        var content = File.ReadAllText(Path);
        var saveInfo = content.Split('\n');

        for(int i = 0; i < rankMax; i++)
        {
            var item = saveInfo[i].Split(',');
            ranks.Add(new RankInfo(item[1], int.Parse(item[2])));
        }

        for (int i = 0; i < rankMax; i++)
        {
            info[i].name.text = ranks[i].name;
            info[i].score.text = $"{ranks[i].score}";
        }
    }

    public int SortDesc(RankInfo a, RankInfo b)
    {
        return (a.score < b.score) ? 1 : -1;
    }

    public void Typing(string type)
    {
        if (inputField.text.Length >= inputMax) return;
        inputField.text += type.ToUpper();
    }

    public void GoTitle()
    {
        Save();
        SceneLoadManager.Inst.LoadScene("Title");
    }
}

[System.Serializable]
public class RankInfoUI
{
    public Text name;
    public Text score;
}

[System.Serializable]
public class RankInfo
{
    public string name;
    public int score;

    public RankInfo (string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
