/* 
 * WRANING: These codes below is far away from bugs with the god and his animal protecting
 *                  _oo0oo_                   ┏┓　　　┏┓
 *                 o8888888o                ┏┛┻━━━┛┻┓
 *                 88" . "88                ┃　　　　　　　┃ 　
 *                 (| -_- |)                ┃　　　━　　　┃
 *                 0\  =  /0                ┃　┳┛　┗┳　┃
 *               ___/`---'\___              ┃　　　　　　　┃
 *             .' \\|     |# '.             ┃　　　┻　　　┃
 *            / \\|||  :  |||# \            ┃　　　　　　　┃
 *           / _||||| -:- |||||- \          ┗━┓　　　┏━┛
 *          |   | \\\  -  #/ |   |          　　┃　　　┃神兽保佑
 *          | \_|  ''\---/''  |_/ |         　　┃　　　┃永无BUG
 *          \  .-\__  '-'  ___/-. /         　　┃　　　┗━━━┓
 *        ___'. .'  /--.--\  `. .'___       　　┃　　　　　　　┣┓
 *     ."" '<  `.___\_<|>_/___.' >' "".     　　┃　　　　　　　┏┛
 *    | | :  `- \`.;`\ _ /`;.`/ - ` : | |   　　┗┓┓┏━┳┓┏┛
 *    \  \ `_.   \_ __\ /__ _/   .-` /  /   　　　┃┫┫　┃┫┫
 *=====`-.____`.___ \_____/___.-`___.-'=====　　　┗┻┛　┗┻┛ 
 *                  `=---='　　　
 *          佛祖保佑       永无BUG
 */
// =============================================================================== 
// Author              :    Frankie.W
// Create Time         :    2019/8/9 11:34:23
// Update Time         :    2019/8/9 11:34:23
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Amib.Threading;
using System;
using System.Windows.Forms.DataVisualization.Charting;
// ===============================================================================
namespace Zaku
{
    class TaskGroupPerformance
    {
        public DateTime startTime;
        public DateTime stopTime;
        public long tasksTotal;
        public long tasksGenerated;
        public long tasksCompleted;
        public long tasksCancelled;
        public long tasksErrored;
        public Amib.Threading.Func<long> getActiveThreads;
        public Amib.Threading.Func<long> getInUseThreads;
        public Amib.Threading.Func<long> getQueuedWorkItems;
        public Amib.Threading.Func<long> getCompletedWorkItems;

        public UsageControl.UsageControl usageThreadsInPool;
        public UsageControl.UsageHistoryControl usageHistorySTP;
        public Chart statisticsChart;
        public Chart timeEfficiencyChart;

        public TaskGroupPerformance()
        {
            Clear();
        }
        public void Init(SmartThreadPool _smartThreadPool)
        {
            getActiveThreads = () => _smartThreadPool.PerformanceCountersReader.ActiveThreads;
            getInUseThreads = () => _smartThreadPool.PerformanceCountersReader.InUseThreads;
            getQueuedWorkItems = () => _smartThreadPool.PerformanceCountersReader.WorkItemsQueued;
            getCompletedWorkItems = () => _smartThreadPool.PerformanceCountersReader.WorkItemsProcessed;
            if (statisticsChart != null)
            {
                statisticsChart.Legends.Clear();
                statisticsChart.Series.Clear();

                Legend legend = new Legend();
                legend.Docking = Docking.Right;
                statisticsChart.Legends.Add(legend);

                Series series = new Series("Data");
                series.ChartType = SeriesChartType.Pie;
                series["PieLabelStyle"] = "Inside";
                series["PieLineColor"] = "Black";
                series.ChartType = SeriesChartType.Pie;
                series.LegendText = "#PERCENT";
                statisticsChart.Series.Add(series);
            }
            if(timeEfficiencyChart != null)
            {
                timeEfficiencyChart.Legends.Clear();
                timeEfficiencyChart.Series.Clear();

                Legend legend = new Legend();
                legend.Docking = Docking.Top;
                timeEfficiencyChart.Legends.Add(legend);

                Series series1 = new Series();
                series1.ChartArea = "LineChart_ChartArea";
                series1.Name = "待处理";
                series1.ChartType = SeriesChartType.Line;
                timeEfficiencyChart.Series.Add(series1);

                Series series2 = new Series();
                series2.ChartArea = "LineChart_ChartArea";
                series2.Name = "队列中";
                series2.ChartType = SeriesChartType.Line;
                timeEfficiencyChart.Series.Add(series2);

                Series series3 = new Series();
                series3.ChartArea = "LineChart_ChartArea";
                series3.Name = "成功";
                series3.ChartType = SeriesChartType.Line;
                timeEfficiencyChart.Series.Add(series3);

                Series series4 = new Series();
                series4.ChartArea = "LineChart_ChartArea";
                series4.Name = "取消";
                series4.ChartType = SeriesChartType.Line;
                timeEfficiencyChart.Series.Add(series4);

                Series series5 = new Series();
                series5.ChartArea = "LineChart_ChartArea";
                series5.Name = "失败";
                series5.ChartType = SeriesChartType.Line;
                timeEfficiencyChart.Series.Add(series5);
            }
            if(usageThreadsInPool != null)
            {
                usageThreadsInPool.Maximum = Environment.ProcessorCount * 5;
                usageHistorySTP.Maximum = Environment.ProcessorCount * 5;
            }
        }
        public void Clear()
        {
            startTime = stopTime = DateTime.MaxValue;
            tasksTotal = tasksGenerated = tasksCancelled = tasksCompleted = tasksErrored = 0;
        }
        public void OnTick()
        {
            if(getInUseThreads != null && usageThreadsInPool != null)
            {
                usageThreadsInPool.Value1 = (int)getInUseThreads();
            }
            if (getActiveThreads != null && usageThreadsInPool != null)
            {
                usageThreadsInPool.Value2 = (int)getActiveThreads();
            }

            if (usageHistorySTP != null && getInUseThreads != null && getActiveThreads != null)
            {
                usageHistorySTP.AddValues((int)getInUseThreads(), (int)getActiveThreads());
            }
            if(statisticsChart != null && statisticsChart.Series.Count > 0 && 
                (tasksGenerated != 0 || tasksCancelled != 0 || tasksCompleted != 0 || tasksErrored != 0))
            {
                statisticsChart.Series[0].Points.Clear();

                long tasksTodo = tasksTotal - tasksGenerated;
                string strCurTime = string.Format("{0:HH:mm:ss}", DateTime.Now);
                if (tasksTodo > 0)
                {
                    timeEfficiencyChart.Series[0].Points.AddXY(strCurTime, tasksTodo);
                    statisticsChart.Series[0].Points.AddXY($"待处理 [{tasksTodo}]", tasksTodo);
                }
                long taskWaiting = tasksGenerated - tasksCancelled - tasksCompleted - tasksErrored;
                if (taskWaiting > 0)
                {
                    timeEfficiencyChart.Series[1].Points.AddXY(strCurTime, taskWaiting);
                    statisticsChart.Series[0].Points.AddXY($"队列中 [{taskWaiting}]", taskWaiting);
                }
                if (tasksCompleted > 0)
                {
                    timeEfficiencyChart.Series[2].Points.AddXY(strCurTime, tasksCompleted);
                    statisticsChart.Series[0].Points.AddXY($"成功 [{tasksCompleted}]", tasksCompleted);
                }
                if (tasksCancelled > 0)
                {
                    timeEfficiencyChart.Series[3].Points.AddXY(strCurTime, tasksCancelled);
                    statisticsChart.Series[0].Points.AddXY($"取消 [{tasksCancelled}]", tasksCancelled);
                }
                if (tasksErrored > 0)
                {
                    timeEfficiencyChart.Series[4].Points.AddXY(strCurTime, tasksErrored);
                    statisticsChart.Series[0].Points.AddXY($"失败 [{tasksErrored}]", tasksErrored);
                }
            }
        }
    }
}