using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public class StoryNode
    {
        public string history;
        public string[] answers;
        public StoryNode[] nextNode;
        public bool isFinal = false;
        public delegate void NodeVisited();
        public NodeVisited nodeVisited;
    }

    private StoryNode current;
    public Text historyText;
    public Transform answersParent;
    public GameObject buttonAnswer;
    public GameObject pauseMenu;

    void Start()
    {
        current = StoryFiller.FillStory();
        historyText.text = "";
        FillUI();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            FinishGameMenu();
    }

    private void FillUI()
    {
       
        historyText.text = current.history;
        if (current.nodeVisited != null) { current.nodeVisited(); }
        foreach (Transform child in answersParent.transform){
            Destroy(child.gameObject);
        }
        bool isLeft = true;
        float height = 50;
        int index = 0;
        foreach(string answer in current.answers){
            GameObject buttonAnswerCopy = Instantiate(buttonAnswer);
            buttonAnswerCopy.transform.parent = answersParent;
            float x = buttonAnswerCopy.GetComponent<RectTransform>().rect.x * 1.1f;
            buttonAnswerCopy.GetComponent<RectTransform>().localPosition =
                                new Vector3(isLeft? x : -x, height, 0);
            if (!isLeft) height += buttonAnswerCopy.GetComponent<RectTransform>().rect.y * 2.5f;

            isLeft = !isLeft;
            FillListener(buttonAnswerCopy.GetComponent<Button>(), index);
            buttonAnswerCopy.GetComponentInChildren<Text>().text = answer;
            index++;
        }
    }

    private void FillListener(Button button, int index)
    {
        button.onClick.AddListener(()=>{
            AnswerSelected(index);
        });
    }

    private void AnswerSelected(int index)
    {
        print(index);
        //historyText.text += "\n" + current.answers[index];
        if(!current.isFinal){
            current = current.nextNode[index];
            FillUI();
        }
        else{
            //Final de partida
            FinishGameMenu();
        }
    }

    private void FinishGameMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
