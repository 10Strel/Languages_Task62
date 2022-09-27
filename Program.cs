/*
Напишите программу, которая заполнит спирально массив 4 на 4.
Например, на выходе получается вот такой массив:
01 02 03 04
12 13 14 05
11 16 15 06
10 09 08 07
*/

int rows = 0, columns = 0;
Random random = new Random();

if (!InputControl("Задайте размерность двумерного массива (m n)", ref rows, ref columns))
    return;

string[,] array = InitArray(rows, columns);

DoWorkArray(array);

PrintArray(array);

# region methods

bool InputControl(string caption, ref int m, ref int n)
{
    int tryCount = 3;
    string inputStr = string.Empty;
    bool resultInputCheck = false;

    while (!resultInputCheck)
    {
        Console.WriteLine($"\r\n{caption} (количество попыток: {tryCount}):");
        inputStr = Console.ReadLine() ?? string.Empty;

        string[] inputStringArray = inputStr.Split(new char[] { ' ', ';' });

        resultInputCheck =
            inputStringArray.Length == 2 &&
            int.TryParse(inputStringArray[0], out m) &&
            m > 0 &&
            int.TryParse(inputStringArray[1], out n) &&
            n > 0;

        if (!resultInputCheck)
        {
            tryCount--;

            if (tryCount == 0)
            {
                Console.WriteLine("\r\nВы исчерпали все попытки.\r\nВыполнение программы будет остановлено.");
                return false;
            }
        }
    }

    return true;
}

string[,] InitArray(int m, int n)
{
    string[,] array = new string[m, n];

    return array;
}

void DoWorkArray(string[,] array)
{
    int curRow = 0, currColumn = 0, currentElement = 0, nextRow, nextColumn;
    
    bool isExistNearestEmptyElement = true;
    while (isExistNearestEmptyElement)
    {
        array[curRow, currColumn] = GetElementValue(++currentElement);

        isExistNearestEmptyElement = GetNearestEmptyElementPosition(curRow, currColumn, out nextRow, out nextColumn);

        curRow = nextRow; currColumn = nextColumn;
    }
}

void PrintArray(string[,] array)
{
    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j < array.GetLength(1); j++)
        {
            Console.Write($"{array[i, j]}\t");
        }

        Console.WriteLine();
    }
}

string GetElementValue(int currElem)
{
    return $"{currElem}".PadLeft(2, '0');
}

bool GetNearestEmptyElementPosition(int curRow, int curColumn, out int nextRow, out int nextColumn)
{
    int tmp;
    nextRow = -1; nextColumn = -1;

    tmp = curColumn + 1;
    if (tmp < columns && string.IsNullOrEmpty(array[curRow, tmp]))
    {
        nextRow = curRow; nextColumn = tmp;
        return true;
    }
        
    tmp = curRow + 1;
    if (tmp < rows && string.IsNullOrEmpty(array[tmp, curColumn]))
    {
        nextRow = tmp; nextColumn = curColumn;
        return true;
    }

    tmp = curColumn - 1;
    if (tmp >= 0 && string.IsNullOrEmpty(array[curRow, tmp]))
    {
        nextRow = curRow; nextColumn = tmp;
        return true;
    }

    tmp = curRow - 1;
    if (tmp >= 0 && string.IsNullOrEmpty(array[tmp, curColumn]))
    {
        nextRow = tmp; nextColumn = curColumn;
        return true;
    }

    return false;        
}

# endregion
