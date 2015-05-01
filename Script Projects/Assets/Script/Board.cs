using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {
	
	int[] board = new int[64];
	// 1 = Player 1 piece, 2 = Player 1 king, 3 = Player 4 piece, 4 = Player 2 king;
	int saved, enemy, white_pieces = 12, red_pieces = 12, player = 0;
	string[] piece = new string[7]{"","O","K","O","K","",""};
	Rect BoardSet(float x, float y, float w, float h, int i)
	{
		return new Rect (x + (w * (i % 8)), y + (h * (i/8 % 8)), w, h);
	}
	string MarkPlace(string piece, int board_spot)
	{
		if (board_spot == 0 || board_spot > 4)
		{
			return"";
		}else return piece;
	}
	int MoveCheckRight(int i, int player)
	{
		int move = 0;
		if (board[i] == 2 || board[i] == 3) move = i-7;
		else  if (board[i] == 1 || board[i] == 4) move = i+7;
		return move;
	}
	int MoveCheckLeft(int i, int player)
	{
		int move = 0;
		if (board[i] == 2 || board[i] == 3) move = i-9;
		else if (board[i] == 1 || board[i] == 4) move = i+9;
		return move;
	}
	int BoarderCheck(int i, int player)
	{
		int move = 0;
		if (i % 8 == 0) 
		{
			if (board[i] == 2 || board[i] == 3) move = i-7;
			else if (board[i] == 1 || board[i] == 4) move = i+9;
		} else if (i == 7 || i == 23 || i == 39 || i == 55)
		{
			if (board[i] == 2 || board[i] == 3) move = i-9;
			else if (board[i] == 1 || board[i] == 4) move = i+7;
		} else move = 0;
		return move;
	}
	//FIX THIS FOR THE KING!!!!
	/**/
	int LeftEnemyCheck(int i, int player)
	{
		if (player == 1)
		{
			if (i-18 > 0 && board[i-9] > 2 && BoarderCheck(board[i-9],player) == 0) return i-18;
			else return 0;
		} else{
			if (i+18 < 63 && board[i+9] == 3 || board[i+9] == 4 && BoarderCheck(board[i+18],player) == 0) return i+18;
			else return 0;
		}
	}
	int RightEnemyCheck(int i, int player)
	{
		if (player == 1)
		{
			if (i-14 > 0 && board[i-7] > 1 && BoarderCheck(board[i-7],player) == 0) return i-14;
			return 0;
		} else {
			if (i+14 < 63 && board[i+7] == 3 || board[i+7] == 4 && BoarderCheck(board[i+14],player) == 0) return i+14;
			return 0;
		}
	}
	/**/
	int KingCheck(int i, int player)
	{
		int change = 0;
		if (player == 1 && i < 7) change = 4;
		else if (player == 0 && i > 55) change = 2; 
		return change;
	}
	void Start () 
	{
		for (int i = 0;i<board.Length;i++)
		{
			if ( i > 0 && i < 8 && i % 2 == 1|| i > 7 && i < 15 && i % 2 == 0 || i > 16 && i < 24 && i % 2 == 1) board[i] = 1;
			if (i > 39 && i < 48 && i % 2 == 0 /*|| i > 48 && i < 56 && i % 2 == 1 || i > 55 && i < 63 && i % 2 == 0*/) board[i] = 3;
		}
	}
	void Update()
	{
		if (player == 0 && red_pieces == 0 || 
		    player == 1 && white_pieces == 0) print ("GAME OVER");
	}
	void OnGUI () {
		for (int i = 0;i<board.Length;i++)
		{
			if (saved == 0 && board[i] > 3) board[i] = 0;
			if (i > 0 && i < 8 && i % 2 == 1|| i > 7 && i < 15 && i % 2 == 0 ||
			    i > 16 && i < 24 && i % 2 == 1 ||  i > 23 && i < 31 && i % 2 == 0 ||
			    i > 32 && i < 40 && i % 2 == 1 || i > 39 && i < 48 && i % 2 == 0 || 
			    i > 48 && i < 56 && i % 2 == 1 || i > 55 && i < 63 && i % 2 == 0) 
				GUI.backgroundColor = Color.grey;
			else GUI.backgroundColor = Color.red;
			if (board[i] != 0 )
			{
				if (board[i] < 3) GUI.contentColor = Color.white;
				else if (board[i] < 5) GUI.contentColor = Color.red;
				else GUI.backgroundColor = Color.green;
			}
			if (GUI.Button(BoardSet(0,0,50,50,i),MarkPlace(piece[board[i]],board[i])))
			{
				if (board[i] != 0)
				{
					if (board[i] < 5)
					{
						saved = i;
						if (BoarderCheck(i,player) == 0)
						{
							if (board[MoveCheckLeft(i,player)] != 0 && board[LeftEnemyCheck(i,player)] == 0) board[LeftEnemyCheck(i,player)] = 6;
							else if (board[MoveCheckLeft(i,player)] == 0) board[MoveCheckLeft(i,player)] = 5;
							if (board[MoveCheckRight(i,player)] != 0 && board[RightEnemyCheck(i,player)] == 0) board[RightEnemyCheck(i,player)] = 6;
							else if (board[MoveCheckRight(i,player)] == 0) board[MoveCheckRight(i,player)] = 5;
						}else{
							if (board[BoarderCheck(i,player)] != 0 && board[LeftEnemyCheck(i,player)] == 0) board[LeftEnemyCheck(i,player)] = 6;
							if (board[BoarderCheck(i,player)] != 0 && board[RightEnemyCheck(i,player)] == 0) board[LeftEnemyCheck(i,player)] = 6;
							else if (board[BoarderCheck(i,player)] == 0) board[BoarderCheck(i,player)] = 5;
						}
					}
					if (board[i] == 6)
					{
						if (player == 0)
							if (board[i-18] == 1 || board[i-18] == 2) enemy = i-9;
						else if (board[i-14] == 1 || board[i-14] == 2) enemy = i-7;
					}
					if (board[i] > 3) 
					{
						board[i] = board[saved];
						board[saved] = 0;
						saved = 0;
						if (enemy != 0) 
						{
							board[enemy] = 0;
							if (player == 0) red_pieces--;
							else white_pieces--;
							print ("RED REMAINING = "+red_pieces+" WHITE REMAINING = "+white_pieces);
						}
					}
					if (KingCheck(i,player) != 0) board[i] = KingCheck(i,player);
				}
			}
		}
	}
}
