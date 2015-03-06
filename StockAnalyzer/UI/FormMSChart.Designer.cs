namespace FinanceAnalyzer.UI
{
    partial class FormMSChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMSChart));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonOk = new System.Windows.Forms.Button();
            this.checkBoxMa = new System.Windows.Forms.CheckBox();
            this.checkBoxVertex = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea1.AxisX.ScaleView.MinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.AxisX.ScaleView.SmallScrollMinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.AxisX.ScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            chartArea1.Name = "Price";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.DockedToChartArea = "Price";
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(10, 11);
            this.chart1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chart1.Name = "chart1";
            series1.ChartArea = "Price";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceUpColor=Red, PriceDownColor=Green";
            series1.Legend = "Legend1";
            series1.Name = "Price";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValuesPerPoint = 4;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(682, 466);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chartStock";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(615, 482);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(76, 27);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // checkBoxMa
            // 
            this.checkBoxMa.AutoSize = true;
            this.checkBoxMa.Location = new System.Drawing.Point(9, 489);
            this.checkBoxMa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxMa.Name = "checkBoxMa";
            this.checkBoxMa.Size = new System.Drawing.Size(36, 16);
            this.checkBoxMa.TabIndex = 2;
            this.checkBoxMa.Text = "MA";
            this.checkBoxMa.UseVisualStyleBackColor = true;
            this.checkBoxMa.CheckedChanged += new System.EventHandler(this.checkBoxMa_CheckedChanged);
            // 
            // checkBoxVertex
            // 
            this.checkBoxVertex.AutoSize = true;
            this.checkBoxVertex.Location = new System.Drawing.Point(49, 489);
            this.checkBoxVertex.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxVertex.Name = "checkBoxVertex";
            this.checkBoxVertex.Size = new System.Drawing.Size(60, 16);
            this.checkBoxVertex.TabIndex = 3;
            this.checkBoxVertex.Text = "Vertex";
            this.checkBoxVertex.UseVisualStyleBackColor = true;
            this.checkBoxVertex.CheckedChanged += new System.EventHandler(this.checkBoxVertex_CheckedChanged);
            // 
            // FormMSChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(701, 513);
            this.Controls.Add(this.checkBoxVertex);
            this.Controls.Add(this.checkBoxMa);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.chart1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormMSChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stocks";
            this.Load += new System.EventHandler(this.FormMSChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.CheckBox checkBoxMa;
        private System.Windows.Forms.CheckBox checkBoxVertex;
    }
}