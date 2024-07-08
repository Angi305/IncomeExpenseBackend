using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Project.Controllers;

namespace Project.Data
{
    public class Income
    {
        private readonly string _filePath;

        public Income(string filePath)
        {
            _filePath = filePath;
        }

        public List<IncomeEntryModel> GetAllEntries()
        {
            if (!File.Exists(_filePath))
            {
                return new List<IncomeEntryModel>();
            }

            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<IncomeEntryModel>>(json);
        }

        public void AddEntry(IncomeEntryModel entry)
        {
            List<IncomeEntryModel> entries = GetAllEntries();
            entry.Id = Guid.NewGuid().ToString(); 
            entries.Add(entry);
            SaveData(entries);
        }

        public void SaveData(List<IncomeEntryModel> entries)
        {
            string json = JsonConvert.SerializeObject(entries, Formatting.Indented);
            
            
            File.WriteAllText(_filePath, json);
        }
    }
}