using KrakenNotes.Data.Models;
using KrakenNotes.Web.Models;
using KrakenNotes.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KrakenNotes.Web.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly INotesServices _notesServices;
        private readonly SignInManager<User> _signInManager;

        public NotesController(INotesServices notesServices, SignInManager<User> signInManager)
        {
            _notesServices = notesServices;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            IEnumerable<NoteViewModel> notes = _notesServices.GetNotesByUserId(id).Select(x => new NoteViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Content = x.Content
            });

            return View(notes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new NoteCreateModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteCreateModel model)
        {
            var note = new Note
            {
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };

            await _notesServices.CreateAsync(note);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var note = _notesServices.GetNoteById(id);

            var model = new NoteEditModel
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                Content = note.Content
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NoteEditModel model)
        {
            var note = _notesServices.GetNoteById(model.Id);

            note.Title = model.Title;
            note.Description = model.Description;
            note.Content = model.Content;

            await _notesServices.UpdateAsync(note);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _notesServices.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var note = _notesServices.GetNoteById(id);

            if (note != null)
            {
                var model = new NoteDetailsModel
                {
                    Title = note.Title,
                    Description = note.Description,
                    Content = note.Content
                };

                return View(model);
            }

            return NotFound();
        }

        public IActionResult Search(SearchModel srcModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = _notesServices.Search(srcModel.SearchText, userId);

            var sResult = result.Select(n => new NoteViewModel
            {
                Content = n.Content,
                Description = n.Description,
                Id = n.Id,
                Title = n.Title
            });

            //if(sResult == null)
            //{
            //    sResult = Enumerable.Empty<NoteViewModel>();
            //}

            var model = new SearchModel
            {
                Id = userId,
                SearchText = "",
                SearchResult = sResult
            };

            return View(model);
        }
    }
}