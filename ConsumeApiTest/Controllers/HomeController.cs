using ConsumeApiTest.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace ConsumeApiTest.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<StudentModel> studentList = new List<StudentModel>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:7011/api/User/GetStudent"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        studentList = JsonConvert.DeserializeObject<List<StudentModel>>(apiResponse);
                    }
                    return View(studentList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<bool>> AddStudent(StudentModel studentModel)
        {
            StudentModel studentModels = new StudentModel();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(studentModel), Encoding.UTF8, "application/json");

               using (var response = await httpClient.PostAsync("https://localhost:7011/api/User/CreateStudent", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    studentModels = JsonConvert.DeserializeObject<StudentModel>(apiResponse);
                }
                return View(studentModels);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}









