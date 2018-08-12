using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
	{

	public GameObject btnNext;
	public GameObject txtKanji;
	public GameObject txtOnyomi;
	public GameObject txtKunyomi;
	public int KanjiPtr = 0;

	// Use this for initialization
	void Start () {
		KanjiList();
		prevKanji = new Queue<Kanji>();

		Next();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateKanji(int direction){
		if(direction == 1){
			Next();
		}
		else if(direction == -1){
			Prev();
		}
	}

	public void Prev(){
		if(prevKanji.Count > 0){
			var prvKanji = prevKanji.Dequeue();
			txtKanji.GetComponent<Text>().text = prvKanji.Value + " ";
			txtOnyomi.GetComponent<Text>().text = prvKanji.Onyomi;
			txtKunyomi.GetComponent<Text>().text = prvKanji.Kunyomi;
		}
	}

	public void Next(){
		var rndkanji = RandomKanji();
		txtKanji.GetComponent<Text>().text = rndkanji.Value + " ";
		txtOnyomi.GetComponent<Text>().text = rndkanji.Onyomi;
		txtKunyomi.GetComponent<Text>().text = rndkanji.Kunyomi;
		prevKanji.Enqueue(rndkanji);
	}
	
	private static readonly System.Random rnd = new System.Random();

	private Kanji RandomKanji(){
		lock(rnd){
			return kanji[rnd.Next(kanji.Length)];	
		}
	}

	private static void KanjiList(){
		var kanjiList = new List<Kanji>();
		kanjiList.Add(new Kanji(){Value = "一", Onyomi = "イチ", Kunyomi = "ひと-"});
		kanjiList.Add(new Kanji(){Value = "二", Onyomi = "ニ", Kunyomi = "ふた-"});
		kanjiList.Add(new Kanji(){Value = "三", Onyomi = "サン", Kunyomi = "み-"});

		kanji = kanjiList.ToArray();
	}

	private static Kanji[] kanji;
	private Queue<Kanji> prevKanji;

	private class Kanji{
		public string Value {get;set;}
		public string Onyomi {get;set;}
		public string Kunyomi {get;set;}

		public List<string> CommonWords{get;set;}
	}
}
