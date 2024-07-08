using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Project.Controllers;

namespace Project.Repository
{
    public class ExpenseRepository
    {
        private readonly string _filePath;

        public ExpenseRepository(string filePath)
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

        public ExpenseEntryModel GetEntry(string id)
        {
            var entries = GetAllEntries();
            return entries.Find(e => e.Id == id);
        }

        public void AddEntry(ExpenseEntryModel entry)
        {
            List<ExpenseEntryModel> entries = GetAllEntries();
            entry.Id = Guid.NewGuid().ToString();
            entries.Add(entry);
            SaveData(entries);
        }

        public void UpdateEntry(ExpenseEntryModel updatedEntry)
        {
            List<ExpenseEntryModel> entries = GetAllEntries();
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

        public void DeleteExpense(string id)
        {
            List<ExpenseEntryModel> entries = GetAllEntries();
            entries.RemoveAll(e => e.Id == id);
            SaveData(entries);
        }

        private void SaveData(List<ExpenseEntryModel> entries)
        {
            string json = JsonConvert.SerializeObject(entries, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}