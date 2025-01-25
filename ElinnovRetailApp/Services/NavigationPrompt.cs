using ElinnovRetail.Models.Common;
using ElinnovRetail.Models.DTOs;
using ElinnovRetail.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElinnovRetail.App.Services
{
    public static class NavigationPrompt
    {
        public static int MenuOptionsPromptInput()
        {
            Console.Write(" Enter the number corresponding to your choice: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                return choice;
            }
            else
            {
                return 0;
            }
        }

        public static bool ConfirmSavePromptInput()
        {
            int option;
            do
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine(" Do you want to continue?");
                Console.WriteLine("");
                Console.WriteLine(" 1. Continue");
                Console.WriteLine(" 2. Cancel (Go back to menu)");
                Console.WriteLine("");

                option = MenuOptionsPromptInput();

            } while (option != 1 && option != 2);

            return option == 1;
        }

        public static bool BackToMenuPromptInput()
        {
            Console.Write(" Enter 1 to go back to menu: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                return choice == 1;
            }
            else
            {
                return false;
            }
        }
    }
}
