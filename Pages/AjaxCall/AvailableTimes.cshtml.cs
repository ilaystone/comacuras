using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComaCuras.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComaCuras.web.Pages.AjaxCall
{
    public class AvailableTimesModel : PageModel
    {
        private readonly ComaCuras.web.Data.ComaCuraswebContext _context;

        public AvailableTimesModel(ComaCuras.web.Data.ComaCuraswebContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointments { get; set; }
        public Schedule Schedule { get; set; }
        public Service Service { get; set; }
        public SelectList PossibleDates { get; set; }
        public async Task OnGet(string day, int srvId, int agent)
        {
            DateTime day_q = DateTime.ParseExact(day, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            Appointments = await _context.Appointment
                                    .AsNoTracking()
                                    .Where(a => a.Date.Day == day_q.Day)
                                    .Where(a => a.AgentNumber == agent).ToListAsync();
            Service = await _context.Service
                                 .AsNoTracking()
                                 .Where(s => s.Id == srvId).FirstOrDefaultAsync();
            Schedule = await _context.Schedule
                                .AsNoTracking()
                                .Where(s => s.ShopId == Service.ShopId)
                                .Where(s => s.Day == Utility.returnDay(day_q.DayOfWeek.ToString())).FirstOrDefaultAsync();
            PossibleDates = new SelectList(GeneratePossibleDates(Service.Duration));
        }

        private IList<string> GeneratePossibleDates(int step)
        {
            List<string> res = new List<string>();
            DateTime start = GetHoursMinutesFromString(Schedule.Start);
            DateTime end = GetHoursMinutesFromString(Schedule.End);
            while (DateTime.Compare(end, start) > 0)
            {
                if (!IntervalsIntersec(start, start.AddMinutes(step)))
                    res.Add($"{start.Hour:00}:{start.Minute:00} - {start.AddMinutes(step).Hour:00}:{start.AddMinutes(step).Minute:00}");
                start = start.AddMinutes(step);
            }
            return res;
        }

        private DateTime GetHoursMinutesFromString(string str)
        {
            List<string> temp = str.Split(":").ToList();
            DateTime res = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                    int.Parse(temp[0]), int.Parse(temp[1]), 0);
            return res;
        }

        private bool IntervalsIntersec(DateTime start, DateTime end)
        {
            float a = Math.Min(float.Parse($"{end.Hour},{end.Minute}"), float.Parse(Schedule.Pend.Replace(':', ',')));
            float b = Math.Max(float.Parse($"{start.Hour},{start.Minute}"), float.Parse(Schedule.Pstart.Replace(':', ',')));
            if (a > b)
                return true;
            foreach (var item in Appointments)
            {
                a = Math.Min(float.Parse($"{end.Hour},{end.Minute}"), float.Parse(item.End.Replace(':', ',')));
                b = Math.Max(float.Parse($"{start.Hour},{start.Minute}"), float.Parse(item.Start.Replace(':', ',')));
                if (a > b)
                    return true;
            }
            return false;
        }
    }
}
