﻿// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Text;
using static System.Console;

WriteLine("Encodings");
WriteLine("[1] ASCII");
WriteLine("[2] UTF-7");
WriteLine("[3] UTF-8");
WriteLine("[4] UTF-16 (Unicode)");
WriteLine("[5] UTF-32");
WriteLine("[any other key] Default");

// выбираем кодировку
Write("Press a number to choose an encoding: ");
ConsoleKey number = ReadKey(intercept: false).Key;
WriteLine();
WriteLine();

Encoding encoder = number switch
{
    ConsoleKey.D1 or ConsoleKey.NumPad1 => Encoding.ASCII,
    ConsoleKey.D2 or ConsoleKey.NumPad2 => Encoding.UTF7,
    ConsoleKey.D3 or ConsoleKey.NumPad3 => Encoding.UTF8,
    ConsoleKey.D4 or ConsoleKey.NumPad4 => Encoding.Unicode,
    ConsoleKey.D5 or ConsoleKey.NumPad5 => Encoding.UTF32,
    _ => Encoding.Default
};

// определяем строку для кодировки
string message = "Café cost: £4.39";

// кодируем строку в массив байтов
byte[] encoded = encoder.GetBytes(message);

// проверяем, сколько байтов требуется для кодировки
WriteLine("{0} uses {1:N0} bytes.",
 encoder.GetType().Name, encoded.Length);
WriteLine();

// перечисляем каждый байт
WriteLine($"BYTE HEX CHAR");
foreach (byte b in encoded)
{
    WriteLine($"{b,4} {b.ToString("X"),4} {(char)b,5}");
}
// декодируем массив байтов обратно в строку и отображаем его
string decoded = encoder.GetString(encoded);
WriteLine(decoded);

//Чтобы указать кодировку, передайте ее в качестве второго параметра конструктору вспомогательного типа:
//StreamReader reader = new(stream, Encoding.UTF8);
//StreamWriter writer = new(stream, Encoding.UTF8);