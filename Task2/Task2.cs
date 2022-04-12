Random rnd = new Random();
bool play=true;
bool current;
string manletter;
string pcletter;
string[,] matrix = new string [3, 3]; //Заполнение матрицы
for (int i = 0; i <= 2; i++)
{
    for (int j = 0; j <= 2; j++)
    {
        matrix[i, j] = "_";
    }
}

int[] empty = new int[9]; //Заполнение массива проверок
for (int i = 0; i < 9; i++)
{
    empty [i] = 1;
}


int ToLenIndex(int a, int b) => a * 3 + b;
(int, int) ToMatrixIndex(int a) => (a / 3, a % 3);

void draw(string[,] matrix)
{
    Console.WriteLine("  " + "1 " + "2 " + "3 " + "\n" + "a" + "|" + matrix[0, 0] + "|" + matrix[0, 1] + "|" +
                      matrix[0, 2] + "|" + "\n" + "b" + "|" + matrix[1, 0] + "|" + matrix[1, 1] + "|" +
                      matrix[1, 2] +
                      "|" + "\n" + "b" + "|" + matrix[2, 0] + "|" + matrix[2, 1] + "|" + matrix[2, 2] + "|");
}  //Отрисовка матрицы

void core(string[,] matrix, string side, int turn)
{
    var (a, b) = ToMatrixIndex(turn - 1);
    matrix[a, b] = side;
} //Метод хода

bool check(string[,] matrix, string side)
{
    if (matrix[0, 0] == matrix[0, 1] && matrix[0, 0] == matrix[0, 2] && matrix[0, 0] != "_" ||
        matrix[1, 0] == matrix[1, 1] && matrix[1, 0] == matrix[1, 2] && matrix[1, 0] != "_" ||
        matrix[2, 0] == matrix[2, 1] && matrix[2, 0] == matrix[2, 2] && matrix[2, 0] != "_" ||
        matrix[0, 0] == matrix[1, 0] && matrix[0, 0] == matrix[2, 0] && matrix[0, 0] != "_" ||
        matrix[0, 1] == matrix[1, 1] && matrix[0, 1] == matrix[2, 1] && matrix[0, 1] != "_" ||
        matrix[0, 2] == matrix[1, 2] && matrix[0, 2] == matrix[2, 2] && matrix[0, 2] != "_" ||
        matrix[0, 0] == matrix[1, 1] && matrix[0, 0] == matrix[2, 2] && matrix[0, 0] != "_" ||
        matrix[0, 2] == matrix[1, 1] && matrix[0, 2] == matrix[2, 0] && matrix[0, 2] != "_")
    {
        draw(matrix);
        Console.WriteLine("GAME OVER" + "\n" + side + " WINS");
        return true;
    }
    return false;
}  //Метод проверки
while (true)
{
    Console.WriteLine("Выбери сторону (O или X)");
    manletter= Console.ReadLine();

    if (manletter!= "O" && manletter != "X")
    {
        Console.WriteLine("Давай по новой, всё хуйня");
    }
    else
    {
        if (manletter == "O")
        {
            current = true;
            pcletter = "X";
            break;
        }
        current = false;
        pcletter = "O";
            break;
    }
}

draw(matrix);
while (true)
{
    if (current)
    {
        Console.WriteLine("Твой ход ");
        int player;
        while (true)
        {
            if(int.TryParse(Console.ReadLine(), out player))
            {
                if (player >= 1 && player <= 9)
                {
                    if (empty[player-1] == 1)
                    {
                        empty[player-1] = 0;
                        break;
                    }
                }  
            }
            Console.WriteLine("Некорректный ход, попробуй ещё.");
        } //Проверка игрока
        
        core(matrix, manletter, player);
        if (check(matrix, manletter))
        {
            break;
        }
        draw(matrix);
        current = false;
    }
    else
    {
        int bot;
        while (true)
        {
            bot = rnd.Next(1, 10);
            if (empty[bot - 1] != 0)
            {
                empty[bot - 1] = 0;
                break;
            }
        } //Проверка Бота

        Console.WriteLine("Ход бота " + bot);

        core(matrix, pcletter, bot);
        if (check(matrix, pcletter))
        {
            break;
        }
        draw(matrix);
        current = true;
    }
}
