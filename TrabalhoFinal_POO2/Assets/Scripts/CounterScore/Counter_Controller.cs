﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



namespace DefaultNamespace
{
    public class Counter_Controller : MonoBehaviour, IObs
    {
        private int currentScore;
        private int points;

        public int CurrentScore { get => currentScore; set => currentScore = value; }
        public int Points { get => points; set => points = value; }
        public TextMeshProUGUI currentScoreText;

        void Start()
        {

            currentScoreText = GameObject.Find("CurrentScore").GetComponent<TextMeshProUGUI>();
            CurrentScore = 0;// zera a pontuação;
            SetCurrentScoreText(Points.ToString());
        }
        
        public void updateObs(ISubj subj, EnemyState state)
        {
            print("Estado recebido: " + state);
            var enemy = (Enemy_Controller)subj;
            print("Estado do inimigo: " + enemy.currentState);
            if (state == EnemyState.Born) // se o inimigo foi criado
            {
                print("OK. Enemy is born!");
                enemy.register(this);
            }
            // senão, se for destruido
            else if (state == EnemyState.Die)
            {
                Points = enemy.getDamage() * 10; // Cada inimigo soma pontos de acordo com seu dano (dificil de matar gera mais pontos);
                CurrentScore += Points; // soma no total;
                Points = 0; // para garantir que não vai somar ponto sem termatado o inimigo;
                SetCurrentScoreText(CurrentScore.ToString()); // mostra na tela;
                //enemy.unregister(this); // inimigo desregistra o contador;
            }
        }

        private void SetCurrentScoreText(string score)
        {
            currentScoreText.text = score;
        }

    }
}
