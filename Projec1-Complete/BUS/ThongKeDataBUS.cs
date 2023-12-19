using LiveCharts.Wpf;
using LiveCharts;
using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.BUS
{
    public class ThongKeDataBUS
    {
        private ThongKeDataDAL _DAL;
        public ThongKeDataBUS()
        {
            _DAL = new ThongKeDataDAL();
        }
        public List<ThongKeData> GetRevenueBy(string groupBy)
        {
            return _DAL.GetRevenueBy(groupBy);
        }
        public SeriesCollection GetChartData(out string[] labels, string groupBy)
        {
            var thongkeDataList = _DAL.GetRevenueBy(groupBy);

            var seriesCollection = new SeriesCollection();
            var labelsList = new List<string>();
            var values = new ChartValues<decimal>();

            foreach (var item in thongkeDataList)
            {
                if (groupBy == "Date")
                {
                    labelsList.Add(item.Date.ToString("dd/MM/yyyy"));
                }
                else if (groupBy == "Month")
                {
                    labelsList.Add(item.Date.ToString("MM/yyyy"));
                }
                else if (groupBy == "Product")
                {
                    labelsList.Add(item.ProductName.ToString());
                }

                values.Add(item.TotalRevenue);
            }

            var columnSeries = new ColumnSeries
            {
                Values = values
            };

            seriesCollection.Add(columnSeries);

            labels = labelsList.ToArray();
            return seriesCollection;
        }
    }
}
