using System;
using System.Collections.Generic;
using System.IO;

namespace expertsystem.services
{
    /// <summary>
    /// Input service for reading data from files
    /// </summary>
    public class InputService
    {
        /// <summary>
        /// Get rows of file
        /// </summary>
        /// <param name="filePath">path to file</param>
        /// <returns>List of rows</returns>
        public List<string> GetRows(string filePath)
        {
            var rows = new List<string>();

            try
            {
                var file = new StreamReader(filePath);
                string row;

                // go through file row by row as long as a next row exists
                while ((row = file.ReadLine()) != null)
                {
                    // add row to list
                    rows.Add(row);
                }

                // close connection to file
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file " + filePath + " could not be read: " + e.Message);
            }

            return rows;
        }
    }
}