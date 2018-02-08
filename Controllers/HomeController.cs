using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Hangman.Models;

namespace Hangman.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Hangman()
    {
      Words newWord = new Words ("cheese");
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("word", newWord.GetShownWord());
      model.Add("tries", newWord.GetTries());
      model.Add("triedLetters", newWord.GetTriedLetters());

      return View("Index", model);
    }

    [HttpPost("/")]
    public ActionResult Guess()
    {
      Words newWord = Words.GetInstance();
      string guess = Request.Form["guess"];
      if (guess.Length > 0)
      {
        newWord.Guess(guess);
      }

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("word", newWord.GetShownWord());
      model.Add("tries", newWord.GetTries());
      model.Add("triedLetters", newWord.GetTriedLetters());
      if (newWord.GetWin())
      {
        return View ("WinLose", "Congratulations you win!");
      }
      if (newWord.GetLose())
      {
        return View ("WinLose", "No congratulations. You lose!");
      }

      return View("Index", model);
    }
  }
}
