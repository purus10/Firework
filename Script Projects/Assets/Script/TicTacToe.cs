using UnityEngine;
using System.Collections;

public class TicTacToe : MonoBehaviour {
	
	int[,] board = new int[3,3];
	//int[,] win = new int[,]{{0,1,2},{3,4,5},{8,7,9},{0,3,8},{1,4,7},{2,5,9},{0,4,9},{2,4,8}};
	public Rect board_but, reset_button, play_button;
	public int Player_Amount;
	public string[] Names, Marks = new string[2];
	public bool Computer;
	bool set,win;
	int player; //Tracks number of Players;
	
	Rect BoardSet(float x, float y, float w, float h, int j, int i)
	{
		return new Rect (x + (w * j), y + (h * i), w, h);
	}
	string Mark(int b)
	{
		if (b != 0) return Marks [b-1];
		else return "";
	}
	string PlayerName(int p,int i, int j)
	{
		return "Player: " + Names[p] + " is playing " + Mark(board[i,j]);
	}
	void getRandomMove () 
	{
		if (win == false) 
		{
			int i, j, movesLeft = 0;
			for (i = 0; i < board.GetLength(0); i++) {
				for (j = 0; j < board.GetLength(1); j++) {
					if (board [i, j] == 0)
						movesLeft++;
				}
			}
			for (i = 0; i < board.GetLength(0); i++) {
				for (j = 0; j < board.GetLength(1); j++) {
					if (board [i, j] == 0) 
					{
						if (movesLeft == 1) SetMark (i, j, player);
						else movesLeft--;
					}
				}
			}
		}
	}
	void OnGUI () {
		if (set == false) 
			if (GUI.Button (play_button, "Play")) set = true;
		if (set == true) 
			if (GUI.Button (reset_button, "Reset")) Reset ();
		if (set == true) //Initialized and displays Board based on board array collum and rows;
			for (int i = 0; i < board.GetLength(0); i++) 
				for (int j = 0; j < board.GetLength(1); j++) 
					if (GUI.Button (new Rect (BoardSet(board_but.x,board_but.y,board_but.width,board_but.height,i,j)), Mark(board[i,j]))) 
					{
					SetMark(i,j,player);
					if (Computer == true) getRandomMove();
					CheckCombo ();
					}
	}
	void Update()
	{
		if (win)
		if (Input.anyKeyDown) {
			Reset();
			set = false;
		}
	}
	void Reset()
	{
		for (int i = 0; i< board.GetLength(0); i++) 
			for (int j = 0; j < board.GetLength(1); j++) board[i,j] = 0;
	}
	void SetMark(int i,int j, int p)
	{
		if (board [i,j] == 0) {
			board [i,j] = 1 + (player % Marks.Length);
			print (PlayerName (p,i,j));
			player = (p+1) % Player_Amount;
		} else print ("Choose an empty slot");
	}
	void CheckCombo()
	{
		for (int j = 0; j < board.GetLength(0); j++) 
		{
			if (board[j,0] == board[j,1] && board[j,1] == board[j,2] && board[j,0] != 0) win = true;
			else if (board[0,j] == board[1,j] && board[1,j] == board[2,j] && board[0,j] != 0) win = true;
			else if (board[0,0] == board[1,1] && board[1,1] == board[2,2] && board[0,0] != 0) win = true;
			else if (board[0,2] == board[1,1] && board[1,1] == board[2,0] && board[0,2] != 0) win = true;
			if (win == true) print ("win!");
		}
	}
}
