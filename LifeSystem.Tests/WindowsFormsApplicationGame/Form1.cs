using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using DataTypes;
using LifeSystem.GameHost;
using LifeSystem.LoadBalancer;
using LifeSystem.Tests;

namespace WindowsFormsApplicationGame
{
    public partial class Form1 : Form
    {
        public int N { get; set; }
        public int M { get; set; }
        public int Hosts { get; set; }
        public int Loops { get; set; }
        public byte[,] Matrix { get; set; }
        public Control[,] Boxes { get; set; }
        public IGameService Service { get; }
        public ILoadBalService BalService { get; }

        private string _loadBalAddress = "http://localhost:8080/LoadBalService.svc/";
        private string _gameAddress = "http://localhost:8081/GameService.svc/";
        private string _ip = "localhost";
        private int _port = 3000; //game host

        public Form1()
        {
            InitializeComponent();
            Service = new GameService();
            BalService = new LoadBalService();
        }

        private void CreateMatrix(object sender, EventArgs e)
        {
            N = int.Parse(textBoxRows.Text);
            M = int.Parse(textBoxCols.Text);
            Loops = int.Parse(textBoxLoops.Text);
            Hosts = int.Parse(textBoxHosts.Text);
            Boxes = new Control[N, M];
            Matrix = new byte[N, M];
            Random r = new Random();
            groupBoxMatr.Controls.Clear();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    var rndBit = r.Next(0, 2);
                    Boxes[i, j] = new TextBox()
                    {
                        Text = rndBit.ToString(),
                        Location = new Point(10 + j * 20, 15 + i * 20),
                        Size = new Size(20, 20)
                    };
                    Matrix[i, j] = (byte)rndBit;
                    groupBoxMatr.Controls.Add(Boxes[i, j]);
                }
            }
        }

        private void UpdateMatrix(object sender, EventArgs e)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Matrix[i, j] = byte.Parse(Boxes[i, j].Text);
                }
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            Request request = new Request(_gameAddress);

            var zones = Split2DArrayIntoParts(N, Hosts);
            var processed = 0;

            for (int i = 0; i < Hosts; i++)
            {
                int rows = zones[i].GetUpperBound(0) - zones[i].GetLowerBound(0) + 1;
                int cols = M;
                bool isFirst = i == 0;
                bool isLast = i == Hosts - 1;

                var host = new Host(i + 1, _ip, _port + i + 1, false);
                BalService.RegisterHost(host);

                var lifeData = new LifeData(Convert2DTo1DArray(zones[i], rows, cols), isFirst, isLast, cols,
                    isFirst || isLast ? rows - 1 : rows - 2);

                var task = new Task(i + 1, host.Id, lifeData, Loops);

                JavaScriptSerializer js = new JavaScriptSerializer();
                string postData = js.Serialize(task);
                string data = request.CreateRequest(_gameAddress, postData, "task");

                List<TaskResult> taskResults = js.Deserialize<List<TaskResult>>(data);

                var taskResult = taskResults[0];
                var matrix = Convert1DTo2DArray(taskResult.Data.Array, rows, cols, taskResult.Data.IsFirst, taskResult.Data.IsLast);

                for (int j = processed; j < processed + taskResult.Data.Height; j++) //taskResult.Data.Height; j++)
                {
                    int g = 0;
                    for (int k = 0; k < cols; k++)
                    {
                        Matrix[j, k] = matrix[g, k];
                        Boxes[j, k].Text = Matrix[j, k].ToString();
                    }
                    g++;
                }
                processed += taskResult.Data.Height;

                //State state = request.GetState(data);                
            }
        }

        public byte[,] Convert1DTo2DArray(byte[] array, int n, int m, bool isFirst, bool isLast)
        {
            byte[,] arr2D = new byte[n, m];

            int counter = 0;
            //n = (isFirst || isLast) ? n + 1 : n + 2;

            for (int i = 0; i < n; i++)//isFirst ? 0 : 1; j < (isLast ? n : n - 1); j++)
            {
                for (int j = 0; j < m; j++)
                {
                    arr2D[i, j] = array[counter];
                    counter++;
                }
            }
            return arr2D;
        }

        public byte[] Convert2DTo1DArray(byte[,] arr, int n, int m)
        {
            int k = 0;
            byte[] b = new byte[n * m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    b[k++] = arr[i, j];
                }
            }
            return b;
        }

        private List<byte[,]> Split2DArrayIntoParts(int whole, int parts)
        {
            int[] arr = new int[parts];
            for (int i = 0; i < arr.Length; i++)
            {
                whole -= arr[i] = (whole + parts - i - 1) / (parts - i);
            }

            List<byte[,]> matricies = new List<byte[,]>();

            for (int i = 0; i < arr.Length; i++)
            {
                byte[,] matrix = new byte[arr[i] + (i > 0 && i < arr.Length - 1 ? 2 : 1), M];

                int g = 0;
                for (int j = i > 0 ? arr[i - 1] - 1 : 0;
                    j < (i == 0 ? arr[i] + 1 : i < arr.Length - 1 ? arr[i - 1] + arr[i] + 1 : arr[i - 1] + arr[i]);
                    j++)
                {                    
                    for (int k = 0; k < M; k++)
                    {
                        matrix[g, k] = Matrix[j, k];
                    }
                    g++;
                }
                matricies.Add(matrix);
            }
            return matricies;
        }
    }
}
