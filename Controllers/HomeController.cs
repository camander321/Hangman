using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Hangman.Models;

namespace Hangman.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View("Index");
    }

    [HttpGet("/game")]
    public ActionResult Hangman()
    {
      Words newWord = new Words (Request.Query["newWord"]);
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("word", newWord.GetShownWord());
      model.Add("tries", newWord.GetTries());
      model.Add("triedLetters", newWord.GetTriedLetters());

      return View("Game", model);
    }

    [HttpPost("/game")]
    public ActionResult Guess()
    {
      Words newWord = Words.GetInstance();
      string guess = Request.Form["guess"];
      if (guess.Length > 0)
      {
        newWord.Guess(guess);
      }

      Dictionary<string, object> model = new Dictionary<string, object>();
      if (newWord.GetWin())
      {
        model.Add("msg", "Congratulations you win!");
        model.Add("word", newWord.GetWord());
        return View ("WinLose", model);
      }
      if (newWord.GetLose())
      {
        model.Add("msg", "No congratulations. You lose!");
        model.Add("word", newWord.GetWord());
        return View ("WinLose", model);
      }

      model.Add("word", newWord.GetShownWord());
      model.Add("tries", newWord.GetTries());
      model.Add("triedLetters", newWord.GetTriedLetters());


      return View("Game", model);
    }
  }
}
