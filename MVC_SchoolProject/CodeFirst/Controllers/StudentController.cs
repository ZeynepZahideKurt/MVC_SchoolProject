using CodeFirst.Entities.Concrete;
using CodeFirst.Models;
using CodeFirst.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeFirst.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IRepository<Lesson> lessonRepository;
        private readonly IRepository<School> schoolRepository;

        public StudentController(IStudentRepository studentRepository, IRepository<Lesson> lessonRepository, ISchoolRepository schoolRepository)
        {
            this.studentRepository = studentRepository;
            this.lessonRepository = lessonRepository;
            this.schoolRepository = schoolRepository;
        }
        public IActionResult Index()
        {
            var studentList = studentRepository.GetAllStudentsIncludeLessonAndSchool();
            return View(studentList);
        }
        public IActionResult AddStudent()
        {
            List<School> schoolList = schoolRepository.GetAll().ToList();
            List<Lesson> lessonList = lessonRepository.GetAll().ToList();
            AddStudentVM addStudentVM = new();
            addStudentVM.Schools = schoolList;
            addStudentVM.Lessons = lessonList;
            return View(addStudentVM);
        }
        [HttpPost]
        public IActionResult AddStudent(AddStudentVM addStudentVM)
        {
            try
            {
                Student student = new();
                student.Name = addStudentVM.Name;
                student.Class = addStudentVM.Class;
                School school = schoolRepository.GetById(addStudentVM.SchoolId);
                Lesson lesson = lessonRepository.GetById(addStudentVM.LessonId);
                student.School = school;
                student.Lessons = lesson;
                bool result = studentRepository.Add(student);

                if (result)
                {
                    TempData["Bildiri"] = "İşlem Başarılı";
                    return RedirectToAction("Index");
                }
                ViewBag.Bildiri = "İşlem Başarılı olamadı";
                return View(addStudentVM);
            }
            catch (Exception)
            {
                return View(addStudentVM);
            }            
        }

        public IActionResult Delete(int id)
        {
            Student student = studentRepository.GetById(id);
            var result = studentRepository.Delete(student);
            if (result)
            {
                TempData["BildiriSilme"] = "Silme işlemi başarılı bir şekilde gerçekleşti";
                return RedirectToAction("Index");
            }
            TempData["BildiriSilme"] = "Silme yaparken bir yerde hata oluştu. tekrar deneyiniz";
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Student student = studentRepository.GetStudentByIdIncludeLessonAndSchool(id);
            AddStudentVM addStudentVM = new();
            addStudentVM.Name = student.Name;
            addStudentVM.Class = student.Class;
            List<School> schoolList = schoolRepository.GetAll().ToList();
            List<Lesson> lessonList = lessonRepository.GetAll().ToList();
            addStudentVM.Schools = schoolList;
            addStudentVM.Lessons = lessonList;
            addStudentVM.Id = id;
            addStudentVM.SchoolId = student.School.Id;
            addStudentVM.LessonId = student.Lessons.Id;
            return View(addStudentVM);
        }
        [HttpPost]
        public IActionResult Update(AddStudentVM addStudentVM)
        {
            try
            {
                Student student = studentRepository.GetStudentByIdIncludeLessonAndSchool(addStudentVM.Id);
                student.Name = addStudentVM.Name;
                student.Class = addStudentVM.Class;
                School school = schoolRepository.GetById(addStudentVM.SchoolId);
                Lesson lesson = lessonRepository.GetById(addStudentVM.LessonId);
                student.School = school;
                student.Lessons = lesson;
                bool result = studentRepository.Update(student);

                if (result)
                {
                    TempData["Bildiri"] = "İşlem Başarılı";
                    return RedirectToAction("Index");
                }
                ViewBag.Bildiri = "İşlem Başarılı olamadı";
                return View(addStudentVM);
            }
            catch (Exception)
            {
                return View(addStudentVM);
            }
        }
    }
}
