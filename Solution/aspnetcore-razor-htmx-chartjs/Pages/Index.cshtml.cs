using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net;
using AspNetHtmxChartJs.Example.Models;

namespace AspNetHtmxChartJs.Example.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;

    public DateOnly CurrentPeriod { get; set; }
    
    public string CurrentPeriodData { get; set; } = string.Empty;
    
    public DateOnly PreviousPeriod { get; set; }

    public DateOnly NextPeriod { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Set the initial state of the page, including the current period and data.
    /// This is then rendered by the razor page. Any updates to the page are handled
    /// by HTMX and the OnGetUpdate method
    /// </summary>
    public void OnGet()
    {
        DataPoint result = this.GetData().First();

        this.PreviousPeriod = result.Date;
        this.NextPeriod = result.Date.AddMonths(1);
        this.CurrentPeriod = result.Date;
        this.CurrentPeriodData = string.Join(",", result.Data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chartId"></param>
    /// <param name="nextButtonId"></param>
    /// <param name="previousButtonId"></param>
    /// <param name="period"></param>
    /// <returns></returns>
    public IActionResult OnGetUpdate(string chartId, string nextButtonId, string previousButtonId, DateOnly period)
    {
        DataPoint? result = this.GetData().FirstOrDefault(x => x.Date.Equals(period));

        if (result == null)
        {
            return new NotFoundResult();
        }

        DateOnly previousDate = LastDayOfMonth(period.AddMonths(-1));
        DateOnly nextDate = LastDayOfMonth(period.AddMonths(1));

        if (this.GetData().FirstOrDefault(x => x.Date.Equals(previousDate)) == null)
        {
            previousDate = period;
        }

        if (this.GetData().FirstOrDefault(x => x.Date.Equals(nextDate)) == null)
        {
            nextDate = period;
        }

        Uri self = new($"/?chartId={chartId}&nextButtonId={nextButtonId}&previousButtonId={previousButtonId}&period={WebUtility.UrlEncode(period.ToString("o"))}&handler=Update", UriKind.Relative);
        Uri previous = new($"/?chartId={chartId}&nextButtonId={nextButtonId}&previousButtonId={previousButtonId}&period={WebUtility.UrlEncode(previousDate.ToString("o"))}&handler=Update", UriKind.Relative);
        Uri next = new($"/?chartId={chartId}&nextButtonId={nextButtonId}&previousButtonId={previousButtonId}&period={WebUtility.UrlEncode(nextDate.ToString("o"))}&handler=Update", UriKind.Relative);

        // We pass the chart HTML element ID back to the client so it knows which chart to update
        return new JsonResult(new ChartData(chartId, nextButtonId, previousButtonId, period.ToLongDateString(), self, previous, next, result.Data));

        DateOnly LastDayOfMonth(DateOnly date)
        {
            return new DateOnly(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
        }
    }

    private List<DataPoint> GetData()
    {
        List<DataPoint> data = new()
        {
            new DataPoint(new DateOnly(2022,1,31), new[] { 18, 12, 15, 1, 2, 4 }),
            new DataPoint(new DateOnly(2022,2,28), new[] { 0, 15, 9, 18, 1, 12}),
            new DataPoint(new DateOnly(2022,3,31), new[] { 4, 12, 8, 10, 16, 1 }),
            new DataPoint(new DateOnly(2022,4,30), new[] { 6, 4, 2, 7, 0, 5 }),
            new DataPoint(new DateOnly(2022,5,31), new[] { 18, 3, 19, 10, 2, 7 }),
            new DataPoint(new DateOnly(2022,6,30), new[] { 0, 12, 14, 7, 6, 13 }),
            new DataPoint(new DateOnly(2022,7,31), new[] { 6, 15, 12, 8, 17, 13 }),
            new DataPoint(new DateOnly(2022,8,31), new[] { 13, 6, 1, 9, 17, 14 }),
            new DataPoint(new DateOnly(2022,9,30), new[] { 6, 15, 14, 4, 12, 5}),
            new DataPoint(new DateOnly(2022,10,31), new[] { 6, 11, 13, 9, 16, 8 }),
            new DataPoint(new DateOnly(2022,11,30), new[] { 16, 19, 10, 9, 5, 17 }),
            new DataPoint(new DateOnly(2022,12,31), new[] { 10, 0, 11, 16, 18, 9 }),
        };

        return data;
    }
}