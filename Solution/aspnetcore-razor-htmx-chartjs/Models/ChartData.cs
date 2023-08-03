namespace AspNetHtmxChartJs.Example.Models;

// Use a record type to represent the data for a chart
public record ChartData(string ChartId, string NextButtonId, string PreviousButtonId, string Title, Uri Self, Uri Previous, Uri Next, int[] Data);