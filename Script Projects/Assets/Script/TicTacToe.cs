using UnityEngine;
using System.Collections;

public class TicTacToe : MonoBehaviour {
	
	int[] board = new int[9];
	int[][] wins = new int[][]{new int []{0,1,2},new int[]{3,4,5},new int[]{6,7,8},new int[]{0,3,6},new int[]{1,4,7},new int[]{2,5,8},new int[]{0,4,8},new int[]{2,4,6}};
	public Rect board_but, reset_button, play_button;
	public int Player_Amount;
	public string[] Names, Marks = new string[2];
	public bool Computer;
	int player, flow; //Tracks number of Players;
	Rect BoardSet(float x, float y, float w, float h, int i)
	{
		return new Rect (x + (w * (i/3 % 3)), y + (h * (i % 3)), w, h);
	}
	string Mark(int b)
	{
		if (b != 0) return Marks [b-1];
		else return "";
	}
	string PlayerName(int p,int i)
	{
		return "Player: " + Names[p] + " is playing " + Mark(board[i]);
	}
	bool WinCombo()
	{
		bool foundWin = false;
		for (int i = 0; i < wins.Length; i++) 
		{
			if (board[wins[i][0]] != 0 && board[wins[i][0]] == board[wins[i][1]] && board[wins[i][0]] == board[wins[i][2]])
				foundWin = true;
		}
		return foundWin;
	}
	void getRandomMove () 
	{
		if (flow != 2) 
		{
			int i, movesLeft = 0;
			for (i = 0; i < board.Length; i++) 
			{
				if (board [i] == 0)
					movesLeft++;
			}
			for (i = 0; i < board.Length; i++) 
			{
				if (board [i] == 0) 
					if (movesLeft == 1) SetMark (i, player);
				movesLeft--;
			}
		}
	}
	void OnGUI () {
		if (flow == 0) 
			if (GUI.Button (play_button, "Play")) flow = 1;
		if (flow != 0) 
		{
			if (GUI.Button (reset_button, "Reset")) Reset ();

			for (int i = 0; i < board.Length; i++) // intiates board
				if (GUI.Button (BoardSet(board_but.x,board_but.y,board_but.width,board_but.height,i), Mark(board[i]))) 
				if (board [i] == 0 && flow != 2) 
				{
					SetMark(i,player);
					if (Computer == true) getRandomMove();
				} else print ("Choose an empty slot");
		}
	}
	void FixedUpdate()
	{
		if (flow == 2)
			if (Input.anyKeyDown) 
		{
			Reset();
			flow = 0;
		}
	}
	void Reset()
	{
		for (int i = 0; i< board.Length; i++) board[i] = 0;
		player = 0;
	}
	void SetMark(int i, int p)
	{
		board [i] = 1 + (player % Marks.Length);
		print (PlayerName (p,i));
		player = (p+1) % Player_Amount;
		if (WinCombo() == true)
		{
			print ("Win!");
			flow = 2;
		}
	}
}
