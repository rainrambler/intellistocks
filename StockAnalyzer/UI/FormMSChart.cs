using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FinanceAnalyzer.Display;
using FinanceAnalyzer.Utility;
using System.Collections.Generic;

namespace FinanceAnalyzer.UI
{
    /// <summary>
    /// Show the chart of one stock in specified period
    /// </summary>
    public partial class FormMSChart : Form
    {
        public FormMSChart()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMSChart_Load(object sender, EventArgs e)
        {
            InitChart();

            FillData();
        }

        private void InitChart()
        {
            // Set series chart type
            chart1.Series["Price"].ChartType = SeriesChartType.Candlestick;

            // Set the style of the open-close marks
            chart1.Series["Price"]["OpenCloseStyle"] = "Candlestick";

            // Show both open and close marks
            chart1.Series["Price"]["ShowOpenClose"] = "Both";

            // Set point width
            chart1.Series["Price"]["PointWidth"] = "0.8";

            chart1.ChartAreas["Price"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["Price"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["Price"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["Price"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["Price"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["Price"].CursorY.IsUserSelectionEnabled = true;

            chart1.Series.Add("PriceMA5");
            chart1.Series["PriceMA5"].ChartArea = "Price";
            chart1.Series["PriceMA5"].ChartType = SeriesChartType.Line;

            chart1.Series.Add("PriceMA10");
            chart1.Series["PriceMA10"].ChartArea = "Price";
            chart1.Series["PriceMA10"].ChartType = SeriesChartType.Line;

            chart1.Series.Add("PriceMA20");
            chart1.Series["PriceMA20"].ChartArea = "Price";
            chart1.Series["PriceMA20"].ChartType = SeriesChartType.Line;

            chart1.ChartAreas.Add("Volume");
            chart1.ChartAreas["Volume"].AlignWithChartArea = "Price";
            chart1.ChartAreas["Volume"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["Volume"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["Volume"].CursorX.IsUserSelectionEnabled = true;

            chart1.Series.Add("Volume");
            chart1.Series["Volume"].ChartArea = "Volume";
            chart1.Series["Volume"].ChartType = SeriesChartType.Column;
            chart1.Series["Volume"].XValueType = ChartValueType.DateTime;            
        }

        private void FillData()
        {
            DateTime startDate = _StockDrawer.MinDate;

            double minYValue = Double.MaxValue;
            while (startDate < _StockDrawer.MaxDate)
            {
                StockPoint pt = _StockDrawer.GetAt(startDate);

                if (pt != null)
                {
                    int curIdx = chart1.Series["Price"].Points.AddXY(startDate, pt.High);
                    DateToXPos_.Add(startDate, curIdx);

                    chart1.Series["Volume"].Points.AddXY(startDate, pt.Volume);

                    chart1.Series["Price"].Points[curIdx].YValues[1] = pt.Low;

                    chart1.Series["Price"].Points[curIdx].YValues[2] = pt.Open;
                    chart1.Series["Price"].Points[curIdx].YValues[3] = pt.End;

                    minYValue = (minYValue > pt.Low) ? pt.Low : minYValue;
                }

                startDate = startDate.AddDays(1);
                while (Holidays.IsWeekend(startDate))
                {
                    startDate = startDate.AddDays(1);
                }
            }

            if (minYValue != Double.MaxValue)
            {
                chart1.ChartAreas["Price"].AxisY.Minimum = (int)minYValue - 1; // 最小值设置
            }

            chart1.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, 
                "5", chart1.Series["Price"], chart1.Series["PriceMA5"]);
            chart1.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, 
                "10", chart1.Series["Price"], chart1.Series["PriceMA10"]);
            chart1.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, 
                "20", chart1.Series["Price"], chart1.Series["PriceMA20"]);

            ShowVertex();
        }

        private void ShowVertex()
        {
            if (Vertexes == null)
            {
                return;
            }

            foreach (var item in Vertexes)
            {
                if (!DateToXPos_.ContainsKey(item.VertexDate))
                {
                    continue;
                }

                int curPos = DateToXPos_[item.VertexDate];
                if (item.VertType == Statistics.Vertex.VertexType.Max)
                {
                    chart1.Series["Price"].Points[curPos].MarkerImage 
                        = ApplicationHelper.GetAppPath() + "\\image\\up.png";
                }
                else if (item.VertType == Statistics.Vertex.VertexType.Min)
                {
                    chart1.Series["Price"].Points[curPos].MarkerImage 
                        = ApplicationHelper.GetAppPath() + "\\image\\down.png";
                }
            }
        }

        public void SetStockDrawer(IStockDrawer stockDrawer)
        {
            _StockDrawer = stockDrawer;
        }

        IStockDrawer _StockDrawer;

        private void checkBoxMa_CheckedChanged(object sender, EventArgs e)
        {
            DisplayMaLines(checkBoxMa.Checked);
        }

        private void DisplayMaLines(bool show)
        {
            chart1.Series["PriceMA5"].Enabled = show;
            chart1.Series["PriceMA10"].Enabled = show;
            chart1.Series["PriceMA20"].Enabled = show;
        }

        private void checkBoxVertex_CheckedChanged(object sender, EventArgs e)
        {

        }

        public ICollection<VertexPoint> Vertexes
        {
            get;
            set;
        }

        Dictionary<DateTime, int> DateToXPos_ = new Dictionary<DateTime, int>();
    }
}
