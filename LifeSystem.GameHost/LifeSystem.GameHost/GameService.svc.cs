using System;
using System.Collections.Generic;
using DataTypes;

namespace LifeSystem.GameHost
{
    // Simple async result implementation.
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HostService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HostService.svc or HostService.svc.cs at the Solution Explorer and start debugging.
    public class GameService : IGameService
    {
        public bool Ping(string hostId)
        {
            return true;
        }

        //public IAsyncResult BeginServiceAsyncMethod(string msg, AsyncCallback callback, object asyncState)
        //{
        //    Console.WriteLine("BeginServiceAsyncMethod called with: \"{0}\"", msg);
        //    return new CompletedAsyncResult<string>(msg);
        //}

        public List<TaskResult> EndCalculateTask(IAsyncResult r)
        {
            CompletedAsyncResult<List<TaskResult>> result = r as CompletedAsyncResult<List<TaskResult>>;
            return result.Data;
        }

        public IAsyncResult CalculateTask(Task task, AsyncCallback callback, object asyncState)
        {
            int fieldWidth = task.Data.Width;
            int fieldHeight = task.Data.Height;

            byte[,] field = new byte[fieldHeight, fieldWidth];       
            byte[,] prevField = new byte[fieldHeight, fieldWidth];
            byte[] resArray = new byte[task.Data.Array.Length];

            int steps = task.StepsCount;
            
            List<TaskResult> results = new List<TaskResult>();

            GameField gameField = new GameField(fieldWidth, fieldHeight, task.Data.IsFirst, task.Data.IsLast);
            
            gameField.Convert1DTo2DArray(task.Data.Array, field);

            //int livePoints = gameField.GetLiveCount(field);

            for (int i = 0; i < steps; i++)
            {
                gameField.CopyField(field, prevField);
                gameField.NextGeneration(field, prevField);

                Convert2DTo1DArray(field, resArray, fieldWidth, fieldHeight);

                results.Add(new TaskResult
                {
                    HostId = task.HostId,
                    TaskId = task.TaskId,
                    PartId = i + 1,
                    Data = new LifeData
                    {
                        Array = resArray,
                        Height = fieldHeight,
                        Width = fieldWidth,
                        IsFirst = task.Data.IsFirst,
                        IsLast = task.Data.IsLast
                    },
                    StepFrom = i,
                    StepTo = i
                });
            }

            return new CompletedAsyncResult<List<TaskResult>>(results);
        }

        public void Convert2DTo1DArray(byte[,] field, byte[] resArray, int fieldWidth, int fieldHeight)
        {
            int counter = 0;

            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    resArray[counter] = field[i, j];
                    counter++;
                }
            }
        }
    }
}
