using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace ExperimentToolApi.Controllers
{
    public class CompressionResultController : ControllerBase
    {
        [HttpPost("/tool/compression-results"), DisableRequestSizeLimit]
        public IActionResult AddNewResults()
        {
            string[] lines = System.IO.File.ReadAllLines(@"Secure\SCISKANIE_WC_Walcowka_AL_30-03-2018_Xcf0521.TXT");

            for (int i = 0; i < lines.Length; i++)
            {
                if (i % 2 == 0 && i >= 12)
                {
                    lines[i] = lines[i].Replace("\t", " ");
                    string[] values = lines[i].Split(" ");
                    decimal newNumber = Decimal.Parse(values[0], NumberStyles.Float);
                    Console.WriteLine(newNumber);
                }
            }
            return Ok("Added succesfully!");
        }
    }
}