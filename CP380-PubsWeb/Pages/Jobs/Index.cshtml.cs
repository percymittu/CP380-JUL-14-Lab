using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = CP380_PubsLab.Models;

namespace CP380_PubsWeb.Pages.Jobs
{
    public class IndexModel : PageModel
    {
        private readonly Models.PubsDbContext webDb = new Models.PubsDbContext();

        [BindProperty]
        public List<Models.Jobs> NoOfJobs { get; set; }
        public async Task<IActionResult> OnGet()
        {
            NoOfJobs = await webDb.Jobs.ToListAsync();
            return Page();
        }
    }
}
