using dotnettodolist.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using dotnettodolist.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace dotnettodolist.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly TodosContext _context;

		public HomeController(ILogger<HomeController> logger, TodosContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var todos = await GetTodos();
			return View(todos);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpGet]
		public async Task<IEnumerable<Todos>> GetTodos ()
		{
            return await _context.Todos.ToListAsync();
        }

		[HttpPost]
		public async Task<ActionResult<Todos>> CreateTodo(string todo)
		{
			if (string.IsNullOrEmpty(todo))
			{
				return BadRequest("Todo can't be empty");
			}

			var newTodo = new Todos { Todo  = todo };
			_context.Todos.Add(newTodo);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTodos", newTodo);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTodo(int id)
		{
			var todo = await _context.Todos.FindAsync(id);

			if (todo == null)
			{
				return NotFound();
			}

			todo.IsFinished = !todo.IsFinished;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpPost]
		public async Task<string> DeleteTodo(int id)
		{
			var student = await _context.Todos.FindAsync(id);

			if (student is not null)
			{
				_context.Todos.Remove(student);
				await _context.SaveChangesAsync();
			}

			return "";
		}
	}
}
