using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Text goldText;
    [SerializeField] private Text buildText;
    [SerializeField] private Text peopleText;
    [SerializeField] private Text scoreText;
    [SerializeField] private List<Text> speedText;
    [SerializeField] private RedNotifi redNotifi;
    [SerializeField] private UpgradeInfoUI entityUp;
    [Space(10)]
    [SerializeField] private int maxBuild;
    [SerializeField] private int maxPeople;
    private int score;
    private int gold;
    private int build;
    private int people;
    private int preSpeedIndex = 0;

    private void Start()
    {
        Score = 0;
        Gold = 0;
        People = 0;
        Build = 0;
        SpeedUP(0);
        entityUp.action += BuildAndPeopleUpgrade;
        StartCoroutine(GetGoldRoutine());
    }

    private IEnumerator GetGoldRoutine()
    {
        while (!isGameOver && !isGameClear)
        {
            Gold += 2;
            yield return new WaitForSeconds(1f);
        }
    }

    public bool isGameOver { get; set; } = false;
    public bool isGameClear { get; set; } = false;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = $"{score}";
        }
    }

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldText.text = $"{gold}";
        }
    }

    public int People
    {
        get
        {
            return people;
        }
        set
        {
            if (value > maxPeople)
            {
                return;
            }

            people = value;
            peopleText.text = $"{people} / {maxPeople}";
        }
    }

    public int Build
    {
        get
        {
            return build;
        }
        set
        {
            if (value > maxBuild)
            {
                return;
            }

            build = value;
            buildText.text = $"{build} / {maxBuild}";
        }
    }

    public bool EnoughGoldCheck(int gold)
    {
        if (gold > this.gold) return false;
        else return true;
    }

    public bool EnoughBuildCheck()
    {
        if (build >= maxBuild) return false;
        else return true;
    }

    public bool EnoughPeopleCheck()
    {
        if (people >= maxPeople) return false;
        else return true;
    }

    public void Red_Notifi(string text)
    {
        redNotifi.OnNotifi(text);
    }

    public void SpeedUP(int index)
    {
        speedText[preSpeedIndex].color = Color.white;

        speedText[index].color = Color.blue;
        preSpeedIndex = index;

        Time.timeScale = index + 1;
    }

    public void BuildAndPeopleUpgrade()
    {
        entityUp.Buy();

        maxBuild += 3;
        maxPeople += 5;

        People = people;
        Build = build;
    }

    public void SpawnUnit(UnitInfo info)
    {
        var pos = Player.Inst.finalHeadQuarters.GetRandSpawnPoint();
        Instantiate(info.unitObj, pos, Quaternion.identity);
    }

    public void BuildTower(TowerInfo info)
    {
        StartCoroutine(building());
        IEnumerator building()
        {
            var towerObj = Instantiate(info.towerObj, Input.mousePosition, Quaternion.identity);

            while (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(Ray, out hit, Mathf.Infinity, LayerMask.GetMask("Tile")))
                {
                    var posX = Mathf.RoundToInt(hit.point.x) + ((info.isEven) ? 0.5f : 0f);
                    var posZ = Mathf.RoundToInt(hit.point.z) + ((info.isEven) ? 0.5f : 0f);

                    towerObj.transform.position = new Vector3(posX, 0, posZ);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    Destroy(towerObj.gameObject);
                    yield break;
                }
                yield return null;
            }

            if (towerObj.BuildCheck())
            {
                GameManager.Inst.Build++;
                GameManager.Inst.Gold -= info.price;
                towerObj.TowerInit();
            }
            else
            {
                Red_Notifi(Utils.CantBuildHere);
                Destroy(towerObj.gameObject);
            }
        }
    }
}
