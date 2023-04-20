using CodeFirst.Entities.Concrete;
using CodeFirst.Models;
using CodeFirst.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers
{
    public class LessonController : Controller
    {
        private readonly IRepository<Lesson> lessonRepository;

        public LessonController(IRepository<Lesson> lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }
        public IActionResult Index()
        {
            var lessonList = lessonRepository.GetAll().ToList();
            return View(lessonList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(LessonCreateVM lessonCreateVM)
        {
            try
            {
                Lesson lesson = new() { Name = lessonCreateVM.Name };
                var result = lessonRepository.Add(lesson);
                if (result)
                {
                    TempData["Bildiri"] = "Kayıt işlemi başarılır bir şekilde gerçekleşti";
                    return RedirectToAction("Index");
                }
                ViewBag.Bildiri = "Kayıt yaparken bir yerde hata oluştu. tekrar deneyiniz";
                return View();
            }
            catch (Exception)
            {
                return View();
            }
            
        }

        public IActionResult Update(int id)
        {
            Lesson lesson = lessonRepository.GetById(id);
            return View(lesson);
        }
        [HttpPost]
        public IActionResult Update(Lesson lesson)
        {
            try
            {
                Lesson _lesson = lessonRepository.GetById(lesson.Id);
                _lesson.Name = lesson.Name;
                var result = lessonRepository.Update(_lesson);
                if (result)
                {
                    TempData["BildiriGuncelleme"] = "Güncelleme işlemi başarılı bir şekilde gerçekleşti";
                    return RedirectToAction("Index");
                }
                ViewBag.BildiriGuncelleme = "Güncelleme yaparken bir yerde hata oluştu. tekrar deneyiniz";
                return View(lesson);
            }
            catch (Exception)
            {
                return View(lesson);
            }
            
        }

        public IActionResult Delete(int id)
        {
            Lesson lesson = lessonRepository.GetById(id);
            var result = lessonRepository.Delete(lesson);
            if (result)
            {
                TempData["BildiriSilme"] = "Silme işlemi başarılı bir şekilde gerçekleşti";
                return RedirectToAction("Index");
            }
            TempData["BildiriSilme"] = "Silme yaparken bir yerde hata oluştu. tekrar deneyiniz";
            return RedirectToAction("Index");
        }
        
    }
}
