using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client
{
    public class HomeController : Controller
    {
	    private readonly ITokenService _tokenService;
		private readonly HttpClient _httpClient = new HttpClient();

	    public HomeController(ITokenService tokenService)
	    {
		    _tokenService = tokenService;
	    }

        //Entrypoint Sitio

        public async Task<IActionResult> Index()
        {
            //var logs = null; //await GetJournalLogs();
            return RedirectToAction("Index", "Usuarios");   //move into XYGO Maintainer
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Controller example
        /*
        private async Task<List<JournalLog>> GetJournalLogs()
        {
	        var token = await _tokenService.GetToken();
	        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
	        var res = await _httpClient.GetAsync("https://localhost:9001/api/values");
	        if (res.IsSuccessStatusCode)
	        {
		        var content = await res.Content.ReadAsStringAsync();
		        var journalLogs = JsonConvert.DeserializeObject<List<JournalLog>>(content);
		        return journalLogs;
	        }

	        return null;
        }
        */


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
