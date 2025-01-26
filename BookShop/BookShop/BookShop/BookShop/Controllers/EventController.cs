using System.Text;
using MusicEvents.Common;
using MusicEvents.Data;
using MusicEvents.Models;
using MusicEvents.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MusicEvents.Controllers;

[Route("[controller]/[action]")]
public class EventController : Controller
{
    private readonly UserRepository _userRepository;
    private readonly EventRepository _eventRepository;

    public EventController(UserRepository userRepository, EventRepository eventRepository)
    {
        _userRepository = userRepository;
        _eventRepository = eventRepository;
    }
    
    [HttpGet]
    public IActionResult CreateEvent()
    {
        if (CurrentUser.CurrentUserId == null)
        {
            return RedirectToAction("Index", "Home");
        }
        
        var model = new CreateEventViewModel();
        return View(model);
    }

    [HttpPost]
    public IActionResult CreateEvent(CreateEventViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.Image != null &&
            model.Image.ContentType != "image/png" &&
            model.Image.ContentType != "image/jpeg")
        {
            ModelState.AddModelError(nameof(model.Image), "File must be in .png or in .jpeg format.");
            return View(model);
        }
        
        if (CurrentUser.CurrentUserId == null)
        {
            return NotFound();
        }

        var user = _userRepository.GetById((Guid)CurrentUser.CurrentUserId);

        if (user == null)
        {
            return NotFound();
        }

        var imageBase64 = string.Empty;
        
        if (model.Image != null)
        {
            using var memoryStream = new MemoryStream();
            model.Image.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();
            imageBase64 = Convert.ToBase64String(bytes);
        }
        
        var _event = new Music
        {
            Name = model.Name,
            Content = model.Content,
            IsPublic = model.IsPublic,
            User = user,
            ImageBase64 = imageBase64
        };

        _eventRepository.Create(_event);
        
        return RedirectToAction("MyEvents");
    }

    [HttpGet]
    public IActionResult MyEvents()
    {
        if (CurrentUser.CurrentUserId == null)
        {
            return RedirectToAction("Index", "Home");
        }
        
        var events = _eventRepository.GetAllFromUser((Guid)CurrentUser.CurrentUserId);

        var model = events.Select(selector: Music => new MyEventViewModel
        {
            Id = Music.Id,
            Name = Music.Name,
            IsPublic = Music.IsPublic,
            Content = Music.Content,
            ImageBase64 = Music.ImageBase64
                // .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                // .Aggregate(new StringBuilder(), (sb, word) =>
                // {
                //     if (sb.Length + word.Length > 100)
                //     {
                //         sb.AppendLine();
                //     }
                //
                //     sb.Append(word).Append(' ');
                //     return sb;
                // })
                // .ToString()
                // .Trim()
        }).ToList();

        return View(model);
    }

    [HttpGet]
    public IActionResult PublicEvents()
    {
        if (CurrentUser.CurrentUserId == null)
        {
            return RedirectToAction("Index", "Home");
        }

        var _events = _eventRepository.GetPublic();

        var model = _events.Select(b => new PublicEventViewModel
        {
            Name = b.Name,
            Content = b.Content,
            Author = $"{b.User.FirstName} {b.User.LastName}",
            ImageBase64 = b.ImageBase64
        }).ToList();

        return View(model);
    }

    [HttpGet("{id:guid}")]
    public IActionResult UpdateEvent([FromRoute] Guid id)
    {
        if (CurrentUser.CurrentUserId == null)
        {
            return RedirectToAction("Index", "Home");
        }

        var _event = _eventRepository.GetById(id);

        if (_event == null)
        {
            return NotFound();
        }

        var model = new UpdateEventViewModel
        {
            Name = _event.Name,
            Content = _event.Content,
            IsPublic = _event.IsPublic
        };

        return View(model);
    }

    [HttpPost("{id:guid}")]
    public IActionResult UpdateEvent([FromRoute] Guid id, UpdateEventViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (CurrentUser.CurrentUserId == null)
        {
            return NotFound();
        }

        var _event = _eventRepository.GetById(id);

        if (_event == null)
        {
            return NotFound();
        }
        
        _event.Name = model.Name;
        _event.Content = model.Content;
        _event.IsPublic = model.IsPublic;

        _eventRepository.Update(_event);

        return RedirectToAction("MyEvents");
    }

    [HttpPost("{id:guid}")]
    public IActionResult DeleteEvent([FromRoute] Guid id)
    {
        if (CurrentUser.CurrentUserId == null)
        {
            return NotFound();
        }

        var _event = _eventRepository.GetById(id);

        if (_event == null)
        {
            return NotFound();
        }
        
        _eventRepository.Delete(_event);

        return RedirectToAction("MyEvents");
    }
}