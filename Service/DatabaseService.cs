using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    // Constructor: Initialize database connection
    public DatabaseService(string dbName, string password)
    {
        // Define the database path
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbName);

        // Create the SQLite connection with encryption
        var options = new SQLiteConnectionString(dbPath, true, key: password);

        _database = new SQLiteAsyncConnection(options);
    }

    // Create the table (only once for the given type)
    public async Task CreateTableAsync<T>() where T : new()
    {
        await _database.CreateTableAsync<T>();
    }

    // Insert a record
    public async Task<int> InsertAsync<T>(T item) where T : new()
    {
        return await _database.InsertAsync(item);
    }

    // Update a record
    public async Task<int> UpdateAsync<T>(T item) where T : new()
    {
        return await _database.UpdateAsync(item);
    }

    // Delete a record
    public async Task<int> DeleteAsync<T>(T item) where T : new()
    {
        return await _database.DeleteAsync(item);
    }

    // Get all records
    public async Task<List<T>> GetAllAsync<T>() where T : new()
    {
        return await _database.Table<T>().ToListAsync();
    }

    // Query with a condition
    public async Task<List<T>> QueryAsync<T>(string query, params object[] args) where T : new()
    {
        return await _database.QueryAsync<T>(query, args);
    }
}
