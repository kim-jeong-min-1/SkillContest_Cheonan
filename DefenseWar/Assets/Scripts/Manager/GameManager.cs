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
    [SerializeField] private GameResultUI gameResultUI;
    [SerializeField] private Unit invincibleUnit;
    [Space(10)]
    [SerializeField] private int maxBuild;
    [SerializeField] private int maxPeople;
    public int curStageIndex;

    [System.NonSerialized] public List<EnemyUnit> curEnemyUnits = new();
    [System.NonSerialized] public List<Tower> curTowers = new();

    private int score;
    private int gold;
    private int build;
    private int people;
    private int preSpeedIndex = 0;
    private bool isGoldUp = false;

    private void Start()
    {
        Score = 0;
        Gold = 0;
        People = 0;
        Build = 0;
        entityUp.action += BuildAndPeopleUpgrade;

        if(curStageIndex == 2) Score = PlayerPrefs.GetInt("Score"); 
        SpeedUP(0);
        StartCoroutine(GetGoldRoutine());
    }

    private IEnumerator GetGoldRoutine()
    {
        while (true)
        {
            Gold += 1;
            yield return new WaitForSeconds(1f);
        }
    }

    public bool isGameOver
    {
        set
        {
            if(value == true)
            {
                WaveManager.Inst.enabled = false;
                PlayerPrefs.SetInt("Score", Score);
                gameResultUI.OnResult(false);
            }
        }
    }
    public bool isGameClear
    {
        set
        {
            if(value == true)
            {
                WaveManager.Inst.enabled = false;
                PlayerPrefs.SetInt("Score", Score);
                gameResultUI.OnResult(true);
            }
        }
    }

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
            if(value > gold)
            {
                gold = (isGoldUp) ? gold + ((value - gold) * 2) : value;
            }
            else
            {
                gold = value;
            }
            
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

    public void WaveEnd()
    {
        foreach (var enemy in curEnemyUnits)
        {
            if (enemy != null) enemy.WaveEndDie();
        }
        curEnemyUnits.Clear();
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

    public void GoldUP()
    {
        StartCoroutine(goldUp());
        IEnumerator goldUp()
        {
            isGoldUp = true;
            yield return new WaitForSeconds(60f);
            isGoldUp = false;
        }
    }

    public void BuildAndPeopleUpgrade()
    {
        entityUp.Buy();

        maxBuild += 3;
        maxPeople += 5;

        People = people;
        Build = build;
    }

    public void EnemyAttackEnable(List<EnemyUnit> enemys)
    {
        StartCoroutine(enemyAttackEnable());
        IEnumerator enemyAttackEnable()
        {
            enemys.ForEach(e => e.isCantAttack = true);
            yield return new WaitForSeconds(10f);
            enemys.ForEach(e => e.isCantAttack = false);
        }
    }

    public void EnemySpeedDown(List<EnemyUnit> enemys)
    {
        StartCoroutine(enemySpeedDown());
        IEnumerator enemySpeedDown()
        {
            enemys.ForEach(e => e.EnemySpeedDown());
            yield return new WaitForSeconds(10f);
            enemys.ForEach(e => e.EnemySpeedUp());
        }
    }

    public void TowerAttackSpeedUp(List<Tower> towers)
    {
        towers.ForEach(t => t.TowerAttackSpeedUP());
    }

    public void TowerHeal(List<Tower> towers)
    {
        towers.ForEach(t => t.TowerHeal());
    }

    public void SpawnUnit(UnitInfo info)
    {
        var pos = Player.Inst.finalHeadQuarters.GetRandSpawnPoint();
        var unit = Instantiate(info.unitObj, pos, Quaternion.identity);
        Player.Inst.curPlayerUnits.Add(unit);
    }

    public void SpawnInvincibleUnit()
    {
        var pos = Player.Inst.finalHeadQuarters.GetRandSpawnPoint();
        var unit = Instantiate(invincibleUnit, pos, Quaternion.identity);

        StartCoroutine(timeOver());
        IEnumerator timeOver()
        {
            yield return new WaitForSeconds(60f);
            Destroy(unit.gameObject);
        }
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
                curTowers.Add(towerObj);
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
