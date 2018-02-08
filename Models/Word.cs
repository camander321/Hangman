using System;
using System.Text;
using System.Collections.Generic;

namespace Hangman.Models
{
  public class Words
  {
    private static Words _instance;

    private string _word;
    private string _shownWord = "";
    private int _tries;
    private string _triedLetters = "";
    private bool _win = false;
    private bool _lose = false;

    public Words(string Word)
    {
      _word = Word;
      _tries = 6;
      for (int i = 0; i < Word.Length; i++)
      {
        _shownWord += "_ ";
      }

      _instance = this;
    }

    public void Guess(string guess)
    {
      if (guess.Length > 1)
      {
        if (_word.Equals(guess))
          _win = true;
        else
          _lose = true;
        return;
      }

      if (_triedLetters.Contains(guess))
      {
        return;
      }

      _triedLetters += guess + " ";

      bool guessCorrect = false;
      for (int i = 0; i < _word.Length; i++)
      {
        if (guess[0] == _word[i])
        {
          StringBuilder sb = new StringBuilder(_shownWord);
          sb[i * 2] = guess[0];
          _shownWord = sb.ToString();
          guessCorrect = true;
        }
      }
      if (!_shownWord.Contains("_"))
      {
        _win = true;
      }
      if (!guessCorrect)
      {
        _tries--;
        if (_tries == 0)
        {
          _lose = true;
        }
      }
    }

    public string GetShownWord()
    {
      return _shownWord;
    }

    public int GetTries()
    {
      return _tries;
    }

    public static Words GetInstance()
    {
      return _instance;
    }

    public bool GetWin()
    {
      return _win;
    }
    public bool GetLose()
    {
      return _lose;
    }

    public string GetTriedLetters()
    {
      return _triedLetters;
    }
  }
}
