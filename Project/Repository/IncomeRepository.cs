using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Project.Controllers;

namespace Project.Repository
{
    public class IncomeRepository
    {
        private readonly string _filePath;

        public IncomeRepository(string filePath)
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

        public IncomeEntryModel GetEntry(string id)
        {
            var entries = GetAllEntries();
            return entries.Find(e => e.Id == id);
        }

        public void AddEntry(IncomeEntryModel entry)
        {
            List<IncomeEntryModel> entries = GetAllEntries();
            entry.Id = Guid.NewGuid().ToString(); 
            entries.Add(entry);
            SaveData(entries);
        }

        public void UpdateEntry(IncomeEntryModel updatedEntry)
        {
            List<IncomeEntryModel> entries = GetAllEntries();
            int index = entries.FindIndex(e => e.Id == updatedEntry.Id);
            if (index != -1)
            {
                entries[index] = updatedEntry;
                SaveData(entries);
            }
            else
            {
                Console.WriteLine($"Entry with ID {updatedEntry.Id} not found.");
            }
        }

        public void DeleteEntry(string id)
        {
            List<IncomeEntryModel> entries = GetAllEntries();
            entries.RemoveAll(e => e.Id == id);
            SaveData(entries);
        }

        private void SaveData(List<IncomeEntryModel> entries)
        {
            string json = JsonConvert.SerializeObject(entries, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}