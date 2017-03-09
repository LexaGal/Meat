namespace LifeSystem.GameHost
{
    public class GameField
    {
        private readonly int _fieldWidth;
        private readonly int _fieldHeight;
        private readonly bool _isFirst;
        private readonly bool _isLast;

        public GameField(int fieldWidth, int fieldHeight)
        {
            _fieldHeight = fieldHeight;
            _fieldWidth = fieldWidth;
        }

        public GameField(int fieldWidth, int fieldHeight, bool isFirst, bool isLast) : this(fieldWidth, fieldHeight)
        {
            _isFirst = isFirst;
            _isLast = isLast;
        }

        public void Convert1DTo2DArray(byte[] array, byte[,] field)
        {
            int counter = 0;
            //int n = (_isFirst || _isLast) ? _fieldHeight + 1 : _fieldHeight + 2;

            for (int i = 0; i < _fieldHeight; i++)//_isFirst ? 0 : 1; i < (_isLast ? n : n - 1); i++)
            {
                for (int j = 0; j < _fieldWidth; j++)
                {
                    field[i, j] = array[counter];
                    counter++;
                }
            }
        }

        public int GetLiveCount(byte[,] field)
        {
            int counter = 0;

            for (int i = 0; i < _fieldHeight; i++)
            {
                for (int j = 0; j < _fieldWidth; j++)
                {
                    if (field[i, j] == 1)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public int[,] GetPointNeighbours(int[,] nb, int x, int y)
        {
            int k = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i == x && j == y)
                    {
                        continue;
                    }
                    nb[k, 0] = i;
                    nb[k, 1] = j;
                    k++;
                }
            }

            return nb;
        }

        public int CountLiveNeighbours(byte[,] field, int x, int y)
        {
            int count = 0;
            int i;
            int[,] nb = new int[8, 2];

            GetPointNeighbours(nb, x, y);

            for (i = 0; i < 8; i++)
            {
                var nbX = nb[i, 0];
                var nbY = nb[i, 1];

                if (nbX < 0 || nbY < 0)
                {
                    continue;
                }

                if (nbX >= _fieldHeight || nbY >= _fieldWidth)
                {
                    continue;
                }

                if (field[nbX, nbY] == 1)
                {
                    count++;
                }
            }
            return count;
        }

        public void NextGeneration(byte[,] field, byte[,] preField)
        {
            for (int i = 0; i < _fieldHeight; i++)
            {
                for (int j = 0; j < _fieldWidth; j++)
                {
                    var point = preField[i, j];
                    var liveNb = CountLiveNeighbours(preField, i, j);

                    if (point == 0)
                    {
                        if (liveNb == 3)
                        {
                            field[i, j] = 1;
                        }
                    }
                    else if (liveNb < 2 || liveNb > 3)
                    {
                        field[i, j] = 0;
                    }
                }
            }
        }

        public void CopyField(byte[,] src, byte[,] dest)
        {
            for (int i = 0; i < _fieldHeight; i++)
            {
                for (int j = 0; j < _fieldWidth; j++)
                {
                    dest[i, j] = src[i, j];
                }
            }
        }
    }
}