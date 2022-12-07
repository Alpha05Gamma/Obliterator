using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileObcerver
{
    internal class UI
    {
        private int index = 2; //Номер положения указателя
        List<int> positions = new List<int>(); //Список элементов
        


        keyboard keyboard = new keyboard(); //Создается экземпляр модели клавиатуры
        
        public string[] description= new string[3]{"F1 - создать процесс", "Enter - информация о процессе", "Space - остановить"};
        //И описание фунционала меню
        public void drawGrid() //Отрисовка сетки интерфейса
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 1);
            for(int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write("-");
            }
            for (int i = 2; i < 17 ; i++)
            {
                Console.SetCursorPosition(Console.BufferWidth / 5 * 3, i);
                Console.Write("|");
            }
            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 5);
            for (int i = Console.BufferWidth / 5 * 3 + 1; i < Console.BufferWidth; i++)
            {
                Console.Write("-");
            }
            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 2);
            for(int i =0; i<description.Length; i++)
            {
                Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 2+i);
                Console.WriteLine(description[i]);
            }
            
        }

        public void menu()
        {
            keyboard.OnChange += Detector; //Подписываемся на событие с нажатием клавиши
            new Thread(keyboard.keyControl).Start(); //Запускаем уловитель события в поток
            drawGrid(); //Вывод интерфейса
            Console.SetBufferSize(Console.BufferWidth, 20000);
            positions = Obcerver.Obcerve();
            Console.SetCursorPosition(0, 1);

        }

        public void Detector(ConsoleKey consoleKey) //Обработчик нажатий
        {

            switch (consoleKey)
            {
                case ConsoleKey.UpArrow://Перемещение указателя на 1 вверх
                    Arrow(-1);
                    break;
                case ConsoleKey.DownArrow://Перемещение указателя на 1 вниз
                    Arrow(1);
                    break;
                case ConsoleKey.Enter: //информация 
                    Obcerver.ObcerveInfo(positions[index - 2]);
                    break;
                case ConsoleKey.Spacebar: //остановить
                    Console.SetCursorPosition(0, 0);
                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(0 + i, 0);
                    }
                    Obcerver.Obliterator(positions[index - 2]);
                    positions = Obcerver.Obcerve();
                    break;
                case ConsoleKey.F1:
                    Console.CursorVisible = true;
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 6);
                    string name = Console.ReadLine();//Считываем название файла
                    Obcerver.Run(name);
                    Console.CursorVisible = false;
                    break;

            }

        }

        private void Arrow(int direction)
        {
            Console.SetCursorPosition(1, index);
            Console.Write(" "); //удаляем старый индикатор
            index = index + direction;
            if(index<positions.Count && index >= 1) //Проверяем новый на выход за предел значений
            {
                Console.SetCursorPosition(1, index);
                Console.Write(">"); //рисуем
            }
            else
            {
                index = 1; //индикатор в положение по умолчанию
                
            }
        }

    }
}
