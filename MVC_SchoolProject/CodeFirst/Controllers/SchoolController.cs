using CodeFirst.Entities.Concrete;
using CodeFirst.Models;
using CodeFirst.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers
{
    public class SchoolController : Controller
    {
        private readonly IRepository<School> schoolRepository;
        private readonly IRepository<Lesson> lessonRepository;
        private readonly ISchoolRepository hasSchoolrepo;

        public SchoolController(IRepository<School> schoolRepository, IRepository<Lesson> lessonRepository,ISchoolRepository hasSchoolrepo)
        {
            this.schoolRepository = schoolRepository;
            this.lessonRepository = lessonRepository;
            this.hasSchoolrepo = hasSchoolrepo;
        }

        public IActionResult Index()
        {
            var schoolList = hasSchoolrepo.GetAllSchoolsIncludeLessons();
            return View(schoolList);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SchoolCreateVM schoolCreateVM)
        {
            School school = new();
            school.Name = schoolCreateVM.Name;
            try
            {
                bool result = schoolRepository.Add(school);

                if (result)
                {
                    TempData["Bildiri"] = "İşlem Başarılı";
                    return RedirectToAction("Index");
                }
                ViewBag.Bildiri = "İşlem Başarılı olamadı";
                return View(schoolCreateVM);
            }
            catch (Exception)
            {
                return View(schoolCreateVM);
            }
        }

        public IActionResult Update(int id)
        {
            School school = schoolRepository.GetById(id);
            SchoolUpdateVM schoolUpdateVM = new()
            {
                Name = school.Name,
                Id = id,
            };
            return View(schoolUpdateVM);
        }
        [HttpPost]
        public IActionResult Update(SchoolUpdateVM schoolUpdateVM)
        {
            try
            {
                School school = schoolRepository.GetById(schoolUpdateVM.Id);
                school.Name = schoolUpdateVM.Name;

                bool result = schoolRepository.Update(school);

                if (result)
                {
                    TempData["Bildiri"] = "İşlem Başarılı";
                    return RedirectToAction("Index");
                }
                ViewBag.Bildiri = "İşlem Başarılı olamadı";

                return View(schoolUpdateVM);
            }
            catch (Exception)
            {
                return View(schoolUpdateVM);
            }
        }

        public IActionResult Delete(int id)
        {
            School school = schoolRepository.GetById(id);
            bool result = schoolRepository.Delete(school);

            if (result)
            {
                TempData["Bildiri"] = "İşlem Başarılı";
                return RedirectToAction("Index");
            }
            ViewBag.Bildiri = "İşlem Başarılı olamadı";

            return View(id);
        }

        public IActionResult AddLesson(int id)
        {
            AddLessonVM addLessonVM = new();
            addLessonVM.Lessons = lessonRepository.GetAll().ToList();
            addLessonVM.SchoolId = id;
            return View(addLessonVM);
        }
        [HttpPost]
        public IActionResult AddLesson(AddLessonVM addLessonVM)
        {
            Lesson lesson = lessonRepository.GetById(addLessonVM.LessonId);
            School school = schoolRepository.GetById(addLessonVM.SchoolId);
            List<School> list = hasSchoolrepo.GetAllSchoolsIncludeLessons();
            foreach (var item in list)
            {
                if (!item.Lessons.Contains(lesson))
                {
                    school.Lessons.Add(lesson);
                }
            }
            bool result=schoolRepository.Update(school);
            if (result)
            {
                TempData["Bildiri"] = "İşlem Başarılı";
                return RedirectToAction("Index");
            }
            ViewBag.Bildiri = "İşlem Başarılı olamadı";
            return View(addLessonVM);
        }
        public IActionResult DeleteLesson(int id)
        {
            DeleteLessonVM deleteLessonVM = new();
            deleteLessonVM.Lessons = lessonRepository.GetAll().ToList();
            deleteLessonVM.School = hasSchoolrepo.GetSchoolByIdIncludeLessons(id);
            return View(deleteLessonVM);
        }
        [HttpPost]
        public IActionResult DeleteLesson(DeleteLessonVM deleteLessonVM)
        {
            School school = hasSchoolrepo.GetSchoolByIdIncludeLessons(deleteLessonVM.School.Id);
            Lesson lesson = lessonRepository.GetById(deleteLessonVM.LessonId);
            school.Lessons.Remove(lesson);
            bool result = schoolRepository.Update(school);
            if (result)
            {
                TempData["Bildiri"] = "İşlem Başarılı";
                return RedirectToAction("Index");
            }
            ViewBag.Bildiri = "İşlem Başarılı olamadı";
            return View(deleteLessonVM);
        }
    }
}
