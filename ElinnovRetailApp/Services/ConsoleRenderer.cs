using ElinnovRetail.App.Extensions;
using ElinnovRetail.Models.Common;
using ElinnovRetail.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ElinnovRetail.Models.Common.Enums;

namespace ElinnovRetail.App.Services
{
    public static class ConsoleRenderer
    {
        /// <summary>
        /// Displays a header containing the app name.
        /// </summary>
        public static void RenderHeader()
        {
            string appName = Constants.AppName;
            int consoleWidth = Console.WindowWidth;
            string horizontalBorder = new string('_', consoleWidth);
            int leftPaddingLength = (consoleWidth - appName.Length) / 2;
            string leftPadding = new string(' ', Math.Max(0, leftPaddingLength));
            Console.Clear();
            Console.WriteLine(horizontalBorder);
            Console.WriteLine("");
            Console.WriteLine($"{leftPadding}{appName}");
            Console.WriteLine(horizontalBorder);
        }

        /// <summary>
        /// Displays a warning or success message with a border.
        /// </summary>
        /// <param name="message">Message to display.</param>
        public static void RenderMessage(string message = "")
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                int consoleWidth = Console.WindowWidth;
                int innerPadding = 3;
                int padding = Math.Max(0, (consoleWidth - message.Length - innerPadding * 2 - 2));
                string border = new string('*', message.Length + innerPadding * 2 + 2);

                Console.WriteLine("");
                Console.WriteLine(new string(' ', padding / 2) + border);
                Console.WriteLine(new string(' ', padding / 2) + "*" + new string(' ', innerPadding) + message + new string(' ', innerPadding) + "*");
                Console.WriteLine(new string(' ', padding / 2) + border);
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Displays <see cref="ManageProductOptionsEnum"/> as options.
        /// </summary>
        public static void RenderManageProductOptions()
        {
            string leftPadding = new string(' ', Constants.LeftPadding);
            Console.WriteLine("");
            Console.WriteLine(" PLEASE CHOOSE AN OPTION: \n");
            foreach (var operation in Enum.GetValues(typeof(ManageProductOptionsEnum)))
            {
                Console.WriteLine($"{leftPadding}{(int)operation}. {operation.ToString().Replace("_", " ")}");
            }
            Console.WriteLine("");
        }

        #region TABLE RENDER

        public static void RenderTable<T>(string tableTitle, List<string> headers, IEnumerable<T> items)
        {
            int totalWidth = 0;
            var rows = new List<List<string>>();

            if (items.Any())
            {
                // Get properties of the type
                var properties = typeof(T).GetProperties();

                // Extract rows by getting property values
                rows = items.Select(item =>
                {
                    var row = properties.Select(p =>
                    {
                        var value = p.GetValue(item)?.ToString() ?? "";
                        // Format the Price as currency
                        if (p.Name == "Price" && decimal.TryParse(value, out decimal price))
                        {
                            value = price.ConvertDecimalToCurrency();
                        }
                        return value;
                    }).ToList();
                    return row;
                }).ToList();
            }

            // Determine the maximum width for each column
            var columnWidths = headers.Select((header, index) =>
                Math.Max(header.Length, rows.Any() ? rows.Max(row => row[index].Length) : 0)
            ).ToList();

            // Calculate the total width of the table (including borders and padding)
            totalWidth = columnWidths.Sum() + columnWidths.Count * 3 + 1; // Adding padding and borders

            // Render the title with borders
            RenderSeparator(totalWidth); // Top border
            Console.WriteLine("|" + tableTitle.PadLeft((totalWidth + tableTitle.Length) / 2).PadRight(totalWidth - 1) + "|");
            RenderSeparator(totalWidth); // Border below title

            // Render the table
            RenderSeparator(columnWidths); // Top border for table
            Console.WriteLine(FormatRow(headers, columnWidths)); // Header row
            RenderSeparator(columnWidths); // Border below header

            if (!items.Any())
            {
                // Display "No records found" in the center of the table
                var noRecordsMessage = "No records found...";
                int messagePadding = (totalWidth - noRecordsMessage.Length - 2) / 2; // Calculate padding for centering
                Console.WriteLine("|" + new string(' ', messagePadding) + noRecordsMessage + new string(' ', messagePadding) + "|");
                RenderSeparator(columnWidths); // Border below message
            }
            else
            {
                foreach (var row in rows)
                {
                    Console.WriteLine(FormatRow(row, columnWidths)); // Table rows
                }
                RenderSeparator(columnWidths); // Bottom border for table
            }
        }

        static void RenderSeparator(int totalWidth)
        {
            Console.WriteLine(new string('-', totalWidth));
        }

        static void RenderSeparator(List<int> columnWidths)
        {
            Console.WriteLine("+" + string.Join("+", columnWidths.Select(width => new string('-', width + 2))) + "+");
        }

        static string FormatRow(List<string> row, List<int> columnWidths)
        {
            var formattedColumns = row.Select((cell, index) =>
                " " + cell.PadRight(columnWidths[index]) + " "
            ).ToList();

            return "|" + string.Join("|", formattedColumns) + "|";
        }

        #endregion
    }
}
