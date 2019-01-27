using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
#pragma warning disable 0649
  [SerializeField] private WigglyTree _wigglyTree;
  [SerializeField] private Vector3 _leftSpawnPosition;
  [SerializeField] private Vector3 _rightSpawnPosition;
  
  [Space(12)]
  [SerializeField] private Enemy _level0Enemy;
  [SerializeField] private List<Enemy> _level1Enemies;
  [SerializeField] private List<Enemy> _level2Enemies;
  [SerializeField] private List<Enemy> _level3Enemies;
#pragma warning restore 0649

  private static WigglyTree WigglyTree { get; set; }
  private static Vector3 LeftSpawnPosition { get; set; }
  private static Vector3 RightSpawnPosition { get; set; }

  private static Enemy Level0Enemy { get; set; }
  private static List<Enemy> Level1Enemies { get; set; }
  private static List<Enemy> Level2Enemies { get; set; }
  private static List<Enemy> Level3Enemies { get; set; }

  public enum GameState { InProgress, GameOver }

  public static GameState State { get; private set; }

  public static int Lives { get; private set; }

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
      State = GameState.GameOver;
    }
  }

  public static void NotifyEnemyDied() {
    enemyDiedFlag = true;
  }

  private void Awake() {
    WigglyTree = _wigglyTree;
    LeftSpawnPosition = _leftSpawnPosition;
    RightSpawnPosition = _rightSpawnPosition;

    Level0Enemy = _level0Enemy;
    Level1Enemies = _level1Enemies;
    Level2Enemies = _level2Enemies;
    Level2Enemies = _level3Enemies;
  }

  private void Start() {
    StartCoroutine(PlayGame());
  }

  private static void GameOver() {

  }

  private static IEnumerator PlayGame() {
    State = GameState.InProgress;
    InitLives(5);
    yield return Level0();
    yield return Level1();
    yield return Level2();
    yield return Level3();
  }

  private static void InitLives(int numLives) {
    Lives = numLives;
    for (int i = 1; i <= Lives; i++) {
      //TODO: Replace with random selection from animals.
      Instantiate(Resources.Load("256px"), new Vector3(Random.Range(-4, 4), 2, 0), Quaternion.identity);
    }
  }

  private static bool enemyDiedFlag = false;
  private static IEnumerator SpawnEnemies(List<Enemy> enemyPrefabs) {
    foreach (Enemy enemy in enemyPrefabs) {
      yield return new WaitForSeconds(Random.Range(3, 7));
      GameObject newEnemy = Instantiate(enemy.gameObject);

      if (Random.Range(0, 1) == 0) {
        newEnemy.transform.position = LeftSpawnPosition;
      } else {
        newEnemy.transform.position = RightSpawnPosition;
      }
    }

    for (int i = 0; i < enemyPrefabs.Count; i++) {
      yield return new WaitUntil(() => enemyDiedFlag);
      enemyDiedFlag = false;
    }
  }

  private static IEnumerator Level0() {
    Debug.Log("Starting level 0.  Wiggle the tree enough, and an enemy will spawn.  Then, kill it to proceed.");
    yield return new WaitUntil(() => Mathf.Abs(WigglyTree.Angle) > 45f);
    yield return SpawnEnemies(new List<Enemy>() { Level0Enemy });
  }

  private static IEnumerator Level1() {
    Debug.Log("Starting level 1");
    yield return SpawnEnemies(Level1Enemies);
  }

  private static IEnumerator Level2() {
    Debug.Log("Starting level 2");
    yield return SpawnEnemies(Level2Enemies);
  }

  private static IEnumerator Level3() {
    Debug.Log("Starting level 3");
    yield return SpawnEnemies(Level3Enemies);
  }
}
