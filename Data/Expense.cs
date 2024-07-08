using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Project.Controllers;

namespace Project.Data
{
    public class Expense
    {
        private readonly string _filePath;

        public Expense(string filePath)
        {
            _filePath = filePath;
        }

        public List<ExpenseEntryModel> GetAllEntries()
        {
            if (!File.Exists(_filePath))
            {
                return new List<ExpenseEntryModel>();
            }

            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<ExpenseEntryModel>>(json);
        }

        public void AddEntry(ExpenseEntryModel entry)
        {
            List<ExpenseEntryModel> entries = GetAllEntries();
            entry.Id = Guid.NewGuid().ToString(); 
            entries.Add(entry);
            SaveData(entries);
        }

        public void SaveData(List<ExpenseEntryModel> entries)
        {
            string json = JsonConvert.SerializeObject(entries, Formatting.Indented);
            
            File.WriteAllText(_filePath, json);
        }
    }
}