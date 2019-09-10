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
// Create Time         :    2019/8/1 16:28:40
// Update Time         :    2019/8/1 16:28:40
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using Amib.Threading;
using BrightIdeasSoftware;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
// ===============================================================================
namespace Zaku
{
    internal class TaskGroupManager
    {
        public Dictionary<string, ITaskGroup> taskGroupMap { get; private set; }
        private ITaskGroup curTaskGroup;
        private SmartThreadPool smartThreadPool;
        private Thread tasksProducerThread;
        private TaskGroupPerformance performance;
        private FastObjectListView showListView;

        private bool isProducerThreadRunning;
        private bool isWorkerThreadRunning;
        private bool isNoticeStop;
        public bool IsNoticeStop {
            get { return isNoticeStop; }
            set { isNoticeStop = value; }
        }

        public TaskGroupManager()
        {
            taskGroupMap = new Dictionary<string, ITaskGroup>();
            curTaskGroup = null;
            isProducerThreadRunning = false;
            isWorkerThreadRunning = false;
            isNoticeStop = false;
            performance = new TaskGroupPerformance();
        }

        public bool Init(Type t)
        {
            try
            {
                System.Reflection.Assembly dataAccess = System.Reflection.Assembly.GetAssembly(t);

                foreach (var type in dataAccess.GetTypes())
                {
                    if (!type.IsAbstract &&
                        typeof(ITaskGroup).IsAssignableFrom(type))
                    {
                        ITaskGroup taskGroup = Activator.CreateInstance(type) as ITaskGroup;
                        if (taskGroup.IsShow())
                        {
                            taskGroupMap.Add(taskGroup.Name, taskGroup);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                GlobalVar.Instance.logger.Error("初始化任务管理器出现异常 ", e);
            }
            return true;
        }
        public int TaskGroupTypeCount()
        {
            return taskGroupMap.Count;
        }
        public bool IsAnyTaskActiving()
        {
            if(this.curTaskGroup == null)
            {
                return false;
            }
            return true;
        }
        public List<string> TaskGroupNameList()
        {
            List<string> keyList = new List<string>(this.taskGroupMap.Keys);
            return keyList;
        }
        public ITaskGroup CreateTaskGroupByName(string name, Control container)
        {
            if(curTaskGroup != null)
            {
                return null;
            }
            if (!taskGroupMap.ContainsKey(name))
            {
                return null;
            }
           
            if (!taskGroupMap[name].Init(container))
            {
                return null;
            }
            curTaskGroup = taskGroupMap[name];
            return curTaskGroup;
        }
        public int ImportData(string file)
        {
            if (this.curTaskGroup == null)
            {
                return 0;
            }

            string strExt = System.IO.Path.GetExtension(file);
            ResourceType type = ResourceType.eResourceType_Txt;
            switch (strExt)
            {
                case ".txt":
                    type = ResourceType.eResourceType_Txt;
                    break;
                case ".csv":
                    type = ResourceType.eResourceType_Csv;
                    break;
                case ".sql":
                    type = ResourceType.eResourceType_MySQL;
                    break;

            }

            int nCnt = this.curTaskGroup.ImportData(file, type);
            this.performance.tasksTotal = nCnt;
            return nCnt;
        }

        public string ExportData(ResourceType type)
        {
            if (this.curTaskGroup == null)
            {
                return string.Empty;
            }
            if(this.showListView == null)
            {
                return string.Empty;
            }
            ArrayList data = (ArrayList)this.showListView.Objects;
            return curTaskGroup.ExportData(data, type);
        }
        public void InitUIControls(FastObjectListView showListView,
            UsageControl.UsageControl usageThreadsInPool,
            UsageControl.UsageHistoryControl usageHistorySTP,
            Chart statisticsChart,
            Chart timeEfficiencyChart)
        {
            this.showListView = showListView;
            if(performance != null)
            {
                performance.usageHistorySTP = usageHistorySTP;
                performance.usageThreadsInPool = usageThreadsInPool;
                performance.statisticsChart = statisticsChart;
                performance.timeEfficiencyChart = timeEfficiencyChart;
            }
        }

        public void Clear()
        {
            this.curTaskGroup = null;
            if (performance != null)
            {
                performance.Clear();
            }
            isProducerThreadRunning = false;
            isWorkerThreadRunning = false;
            isNoticeStop = false;
        }
        public bool Stop()
        {
            if (this.curTaskGroup == null)
            {
                return false;
            }
            GlobalVar.Instance.logger.Debug("开始尝试停止任务.");
            StopPerformance();

            isProducerThreadRunning = false;
            isWorkerThreadRunning = false;
            isNoticeStop = false;

            if (performance != null)
            {
                GlobalVar.Instance.logger.Debug("最后统计.");
                performance.OnTick();
            }

            try
            {
                if (tasksProducerThread != null)
                {
                    tasksProducerThread.Join();
                }
                if (smartThreadPool != null)
                {
                    smartThreadPool.Shutdown(true, 1000);
                    smartThreadPool.Dispose();
                    smartThreadPool = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception e)
            {
                GlobalVar.Instance.logger.Error("释放回收出现错误.", e);
                return false;
            }
            finally
            {
                GlobalVar.Instance.logger.Info("任务已停止.");
                this.curTaskGroup.Stop();
            }

            if (tasksProducerThread != null)
            {
                tasksProducerThread.Abort();
                tasksProducerThread = null;
            }

            curTaskGroup.Clear();
            return true;
        }
        public void Do(TaskGroupSetting  s)
        {
            if (this.curTaskGroup == null)
            {
                return;
            }
            
            if(!this.curTaskGroup.PrepareDo(this.showListView, s))
            {
                GlobalVar.Instance.logger.Error("准备执行任务失败.");
                return;
            }

            GlobalVar.Instance.logger.Info("开始执行任务.");
            StartPerformance();

            isProducerThreadRunning = true;
            isWorkerThreadRunning = true;
            isNoticeStop = false;

            STPStartInfo stpStartInfo = new STPStartInfo();
            stpStartInfo.ThreadPoolName = "扎古量产池";
            stpStartInfo.IdleTimeout = 5000;
            stpStartInfo.MaxWorkerThreads = s.ThreadNum;
            stpStartInfo.MinWorkerThreads = 0;
            stpStartInfo.EnableLocalPerformanceCounters = true;
            smartThreadPool = new SmartThreadPool(stpStartInfo);
            performance.Init(smartThreadPool);

            tasksProducerThread = new Thread(new ThreadStart(this.TasksProducerThreadMain));
            tasksProducerThread.IsBackground = true;
            tasksProducerThread.Start();
        }
        private void StopPerformance()
        {
            performance.stopTime = System.DateTime.Now;
        }
        private void StartPerformance()
        {
            performance.startTime = DateTime.Now;
            performance.tasksCompleted = 0;
            performance.tasksGenerated = 0;
            performance.tasksCancelled = 0;
            performance.tasksErrored = 0;
            performance.getActiveThreads = () => smartThreadPool.PerformanceCountersReader.ActiveThreads;
            performance.getInUseThreads = () => smartThreadPool.PerformanceCountersReader.InUseThreads;
            performance.getQueuedWorkItems = () => smartThreadPool.PerformanceCountersReader.WorkItemsQueued;
            performance.getCompletedWorkItems = () => smartThreadPool.PerformanceCountersReader.WorkItemsProcessed;
        }
        private object SmartThreadMain(object obj)
        {
            object r = null;
            if(obj != null && this.curTaskGroup != null)
            {
                r = this.curTaskGroup.OneTask_Do(obj);
            }
            return r;
        }
        private void OneThreadDoneCallback(IWorkItemResult r)
        {
            if (r == null || this.curTaskGroup == null)
            {
                Interlocked.Increment(ref performance.tasksErrored);
            }
            else
            {
                if (r.Exception != null)
                {
                    Interlocked.Increment(ref performance.tasksErrored);
                    this.curTaskGroup.OneTask_Finished(this.showListView, r.Result, TaskStatus.eTaskStatus_Finish_Error);
                }
                if (r.IsCanceled)
                {
                    Interlocked.Increment(ref performance.tasksCancelled);
                    this.curTaskGroup.OneTask_Finished(this.showListView, r.Result, TaskStatus.eTaskStatus_Finish_Cancelled);
                }
                if (r.IsCompleted)
                {
                    if(((ITaskGroupResult)(r.Result)).Status == TaskStatus.eTaskStatus_Finish_Error)
                    {
                        Interlocked.Increment(ref performance.tasksErrored);
                    }
                    else if (((ITaskGroupResult)(r.Result)).Status == TaskStatus.eTaskStatus_Finish_Cancelled)
                    {
                        Interlocked.Increment(ref performance.tasksCancelled);
                    }
                    else {
                        Interlocked.Increment(ref performance.tasksCompleted);
                    }
                    this.curTaskGroup.OneTask_Finished(this.showListView, r.Result, TaskStatus.eTaskStatus_Finish_Suceessed);
                }
            }

            if (performance.tasksErrored + performance.tasksCancelled + performance.tasksCompleted >= performance.tasksGenerated)
            {
                GlobalVar.Instance.logger.Info("全部任务消费完毕.");
                isWorkerThreadRunning = false;
                NoticeWorkerThreadStop();
                return;
            }
        }
        private void NoticeWorkerThreadStop()
        {
            isNoticeStop = true;
        }
        private void TasksProducerThreadMain()
        {
            WorkItemCallback workItemCallback = new WorkItemCallback(this.SmartThreadMain);
            PostExecuteWorkItemCallback postExecuteWorkItemCallback = new PostExecuteWorkItemCallback(this.OneThreadDoneCallback);

            while (isProducerThreadRunning)
            {
                IWorkItemsGroup workItemsGroup = smartThreadPool;
                if (null == workItemsGroup)
                {
                    return;
                }

                try
                {
                    object data = this.curTaskGroup.OneTask_Produce();
                    if (data != null)
                    {
                        workItemCallback = new WorkItemCallback(this.SmartThreadMain);
                        workItemsGroup.QueueWorkItem(workItemCallback, data, postExecuteWorkItemCallback, CallToPostExecute.Always);
                        performance.tasksGenerated++;
                    }
                    else
                    {
                        GlobalVar.Instance.logger.Info("全部任务生产完毕.");
                        isProducerThreadRunning = false;
                    }
                }
                catch (ObjectDisposedException e)
                {
                    e.GetHashCode();
                    break;
                }
                Thread.Sleep(10);
            }
        }
        public void OnTick()
        {
            if(performance == null)
            {
                return;
            }
            if (isWorkerThreadRunning)
            {
                performance.OnTick();
            }
        }
    }
}