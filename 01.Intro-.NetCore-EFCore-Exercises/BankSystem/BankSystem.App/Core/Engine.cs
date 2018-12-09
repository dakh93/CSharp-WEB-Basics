using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.App.Core
{
    public class Engine
    {
        

        public Engine()
        {
            
        }

        public void Run()
        {
            var cmdInterpreter = new CommandInterpreter();

            while (true)
            {
                var input = Console.ReadLine();

                cmdInterpreter.GetInputInfo(input);

            }

        }
    }
}
