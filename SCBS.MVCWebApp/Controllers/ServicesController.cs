using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SCBS.Repositories.DTO;
using SCBS.Repositories.Models;
using System.Text;

namespace SCBS.MVCWebApp.Controllers
{
    public class ServicesController : Controller
    {
        private string APIEndPoint = "https://localhost:7101/api/";
        public ServicesController() { }
        // GET: ServiceController
        public async Task<IActionResult> Index()
        {
            List<Service> services = new List<Service>(); // Khởi tạo danh sách rỗng mặc định

            try
            {
                using (var httpClient = new HttpClient())
                {
                    #region Add Token to Header of Request
                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                    #endregion

                    using (var response = await httpClient.GetAsync(APIEndPoint + "Service"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var bytes = Encoding.Default.GetBytes(content);
                            var utf8Content = Encoding.UTF8.GetString(bytes);

                            services = JsonConvert.DeserializeObject<List<Service>>(utf8Content) ?? new List<Service>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine("Lỗi khi gọi API Service: " + ex.Message);
                services = new List<Service>(); // Đảm bảo danh sách không bị null
            }

            try
            {
                var users = await GetUsers();
                ViewData["Users"] = new SelectList(users, "Id", "Username");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine("Lỗi khi lấy danh sách Users: " + ex.Message);
                ViewData["Users"] = new SelectList(new List<User>(), "Id", "Username"); // Tránh lỗi null
            }

            return View(services);
        }

        // GET: ServiceController/Details/5
       
        // GET: ServiceController/Create
        //public async Task<IActionResult> Create()
        //{
        //    ViewData["UserId"] = new SelectList(await GetUsers(), "Id", "Username");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Name,Description,Price,Duration,Rating,Status,CreatedAt,UpdatedAt,UserId")] ServiceDTO service)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            #region Add Token to header of Request

        //            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
        //            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

        //            #endregion

        //            using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "Service", service))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var content = await response.Content.ReadAsStringAsync();
        //                    var result = JsonConvert.DeserializeObject<int>(content);
        //                    if (result > 0)
        //                    {
        //                        return RedirectToAction(nameof(Index));
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    ViewData["UserId"] = new SelectList(await GetUsers(), "Id", "Username", service.UserId);
        //    return View(service);
        //}

       

        //private async Task<List<User>> GetUsers()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
        //        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
        //        using (var response = await httpClient.GetAsync(APIEndPoint + "Users"))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var content = await response.Content.ReadAsStringAsync();
        //                return JsonConvert.DeserializeObject<List<User>>(content);
        //            }
        //        }
        //    }
        //    return new List<User>();
        //}
        public  async Task<IActionResult> ServiceCatagoryList()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] ServiceDTO serviceDto)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    #region Add Token to header of Request
                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                    #endregion

                    using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "Service", serviceDto))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return Json(new { success = true });
                        }
                    }
                }
            }

            return Json(new { success = false });
        }

        private async Task<List<ServiceDTO>> GetServices()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(APIEndPoint + "Service");
                return JsonConvert.DeserializeObject<List<ServiceDTO>>(response);
            }
        }

        private async Task<List<User>> GetUsers()
        {
            var users = new List<User>();
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.GetAsync(APIEndPoint + "User"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<List<User>>(content);
                    }
                }
            }
            return users;
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.GetAsync(APIEndPoint + "Service/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Service>(content);
                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }
            return NotFound();
        }

        // GET: Service/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Users = await GetUsers();
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.GetAsync(APIEndPoint + "Service/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Service>(content);
                        if (result != null)
                        {
                            ViewData["UserId"] = new SelectList(Users, "Id", "Username", result.UserId);
                            return View(result);
                        }
                    }
                }
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,Name,Description,Price,Duration,Rating,Status,CreatedAt,UpdatedAt")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    #region Add Token to header of Request

                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                    #endregion

                    using (var response = await httpClient.PutAsJsonAsync(APIEndPoint + "Service", service))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<int>(content);
                            if (result > 0)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
            }
            ViewData["UserId"] = new SelectList(await GetUsers(), "UserId", "Username", service.UserId);
            return View(service);
        }









        // GET: Service/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.GetAsync(APIEndPoint + "Service/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Service>(content);
                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }
            return NotFound();
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion

                using (var response = await httpClient.DeleteAsync(APIEndPoint + "Service/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ServiceExists(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion


                using (var response = await httpClient.GetAsync(APIEndPoint + "Service"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<Service>>(content);

                        if (result != null)
                        {
                            return result.Any(e => e.Id == id);
                        }
                    }
                }
            }
            return false;
        }
    }
}
