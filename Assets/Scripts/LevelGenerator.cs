using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class LevelGenerator : MonoBehaviour {
#pragma warning disable 0649
  
  [SerializeField] private Text _levelText;
  [SerializeField] private Text _instructionalText;

  [SerializeField] private GameObject _startButton;
  [SerializeField] private GameObject _logo;
  [SerializeField] private GameObject _gameover;
  [SerializeField] private GameObject _youwin;

  [SerializeField] private WigglyTree _wigglyTree;
  [SerializeField] private Vector3 _leftSpawnPosition;
  [SerializeField] private Vector3 _rightSpawnPosition;

  [Space(12)]
  [SerializeField] private Enemy _level0Enemy;
  [SerializeField] private List<Enemy> _level1Enemies;
  [SerializeField] private List<Enemy> _level2Enemies;
  [SerializeField] private List<Enemy> _level3Enemies;
  [SerializeField] private float _animationSpeed;
#pragma warning restore 0649

  private static Text LevelText { get; set; }
  private static Text InstructionalText { get; set; }
  private static string instruction1 = "Press and hold the spacebar to wiggle!";
  private static string instruction2 = "The Bunyans are coming! Protect you and your furry friends!";

  private static GameObject startButton { get; set; }
  private static GameObject logo { get; set; }
  private static GameObject gameover { get; set; }
  private static GameObject youwin { get; set; }

  private static WigglyTree WigglyTree { get; set; }
  private static Vector3 LeftSpawnPosition { get; set; }
  private static Vector3 RightSpawnPosition { get; set; }

  private static Enemy Level0Enemy { get; set; }
  private static List<Enemy> Level1Enemies { get; set; }
  private static List<Enemy> Level2Enemies { get; set; }
  private static List<Enemy> Level3Enemies { get; set; }

  public enum GameState { Loading, InProgress, GameOver }

  public static GameState State { get; private set; }

  public static int Lives { get; private set; }
  private static float animationSpeed = .01f;

  public static void RemoveLife() {
    Lives -= 1;

    GameObject[] Animals;
    Animals = GameObject.FindGameObjectsWithTag("Animal");
    if (Animals.Length == 0) {
      Debug.LogError("All the animals have already left. :(");
    } else {
      int index = Random.Range(0, Animals.Length);
      GameObject animal = Animals[index];
      Destroy(animal);
    }

    if (Lives == 0) {
      GameOver(false);
    }
  }

  public static void NotifyEnemyDied() {
    if (State == GameState.InProgress) {
      enemyDiedFlag += 1;
    }
  }

  public void LoadOnClick() {
    if (State == GameState.Loading) {
      State = GameState.InProgress;
      StartCoroutine(PlayAnimation());
      StartCoroutine(PlayGame());
    }
  }

  public void Retry() {
    StartCoroutine(PlayGame(skipTutorial: true));

    Debug.Log("Retry isn't implemented");
  }

  private void Awake() {
    WigglyTree = _wigglyTree;
    LeftSpawnPosition = _leftSpawnPosition;
    RightSpawnPosition = _rightSpawnPosition;

    Level0Enemy = _level0Enemy;
    Level1Enemies = _level1Enemies;
    Level2Enemies = _level2Enemies;
    Level3Enemies = _level3Enemies;

    LevelText = _levelText;
    InstructionalText = _instructionalText;
    animationSpeed = _animationSpeed;

    startButton = _startButton;
    logo = _logo;

    gameover = _gameover;
    youwin = _youwin;

    LevelText = GameObject.Find("LevelText").GetComponent<Text>();
  }

  private static IEnumerator PlayAnimation() {
    for (int i = 0; i < 500; i++) {
      startButton.transform.Translate(Vector3.up);
      logo.transform.Translate(Vector3.up);
      yield return new WaitForSeconds(animationSpeed);

    }
  }

  private static void GameOver(bool isWin) {
    State = GameState.GameOver;
    Debug.Log("Game Over");
    if (isWin) {
      youwin.SetActive(true);
    } else {
      gameover.SetActive(true);
    }
  }

  private static IEnumerator PlayGame(bool skipTutorial = false) {
    gameover.SetActive(false);
    youwin.SetActive(false);

    //Remove all squirrels
    GameObject[] Animals;
    Animals = GameObject.FindGameObjectsWithTag("Animal");
    foreach (GameObject animal in Animals) {
      Destroy(animal);
    }

    //Reset enemy tracking
    enemyDiedFlag = 0;

    State = GameState.InProgress;
    InitLives();
    if (!skipTutorial) yield return Level0();
    yield return Level1();
    yield return Level2();
    yield return Level3();
  }

  private static void InitLives() {
    Lives = 5;
    for (int i = 1; i <= Lives; i++) {
      //TODO: Replace with random selection from animals.
      Instantiate(Resources.Load("256px"), new Vector3(Random.Range(-4f, 4f), 2, 0), Quaternion.identity);
    }
  }

  private static int enemyDiedFlag = 0;
  private static IEnumerator SpawnEnemies(List<Enemy> enemyPrefabs) {
    foreach (Enemy enemy in enemyPrefabs) {
      yield return new WaitForSeconds(Random.Range(3, 7));
      GameObject newEnemy = Instantiate(enemy.gameObject);

      if (Random.Range(0, 2) == 0) {
        newEnemy.transform.position = LeftSpawnPosition;
      } else {
        newEnemy.transform.position = RightSpawnPosition;
      }
    }

    for (int i = 0; i < enemyPrefabs.Count; i++) {
      yield return new WaitUntil(() => enemyDiedFlag > 0);
      enemyDiedFlag -= 1;
    }
  }

  private static IEnumerator Level0() {
    LevelText.text = "Day 0";
    InstructionalText.text = instruction1;

    yield return new WaitUntil(() => Mathf.Abs(WigglyTree.Angle) > 45f);

    InstructionalText.text = instruction2;
    yield return SpawnEnemies(new List<Enemy>() { Level0Enemy });
  }

  private static IEnumerator Level1() {
    InstructionalText.text = "";

    LevelText.text = "Day 1";
    yield return SpawnEnemies(Level1Enemies);
  }

  private static IEnumerator Level2() {
    LevelText.text = "Day 2";
    yield return SpawnEnemies(Level2Enemies);
  }

  private static IEnumerator Level3() {
    LevelText.text = "Day 3";
    yield return SpawnEnemies(Level3Enemies);
    GameOver(true);
  }
}