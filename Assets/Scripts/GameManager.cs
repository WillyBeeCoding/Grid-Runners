using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Enemy[] enemies;
    public Transform panels;

    public Material panelDimMat;

    public int score { get; private set; }
    public int lives { get; private set; }

    private void SetScore(int score) => this.score = score;
    private void SetLives(int lives) => this.lives = lives;

    private void Start() {
        NewGame();
    }

    private void Update() {
        if (this.lives <= 0 && Input.GetKeyDown(KeyCode.Space)) {
            NewGame();
        }
    }

    private void NewGame() {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound() {
        foreach(Transform panel in this.panels) { panel.GetComponent<MeshRenderer>().material = panelDimMat; }
        ToggleCharacters(true);
    }

    private void ToggleCharacters(bool active) {
        foreach(Enemy enemy in this.enemies) { enemy.gameObject.SetActive(active); }
        player.gameObject.SetActive(active);
    }

    private void GameOver() {
        ToggleCharacters(false);
    }

    public void EnemyKilled(Enemy enemy) {
        SetScore(this.score + enemy.points);
    }

    public void PlayerKilled() {
        player.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if (lives > 0) {ToggleCharacters(true); }
        else { GameOver(); }
    }
}
